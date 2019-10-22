using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ARSphere.Middleware.Validation
{
	using ErrorBucket = Dictionary<string, string>;
	/// <summary>
	/// <para>Transient service to handle model validation.</para>
	/// <para>Previously, we used a <code>ValidateModel</code> attribute attached to API methods to handle
	/// automatic valiadtion. However, this practice would not work with SignalR methods. Thus, all
	/// validation is handled manually through this service, allowing the process to be identical between
	/// API controllers and SignalR hubs.</para>
	/// </summary>
	public class ValidationService : IValidationService
	{
		private Dictionary<string, ErrorBucket> Errors = new Dictionary<string, ErrorBucket>();
		private IServiceProvider _serviceProvider;

		public ValidationService(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public bool IsValid()
		{
			return Errors.Count == 0;
		}

		public void AddError(string prop, string message)
		{
			AddError(prop, GetErrorCount(prop).ToString(), message);
		}

		public bool AddError(string prop, string key, string message)
		{
			return AssureBucket(prop).TryAdd(key, message);
		}

		public bool RemoveError(string prop, string key)
		{
			return GetBucket(prop)?.Remove(key) ?? false;
		}

		public string GetError(string prop, string key)
		{
			var bucket = GetBucket(prop);
			if(bucket != null)
			{
				string error;
				bool errorExists = bucket.TryGetValue(key, out error);
				if(errorExists)
				{
					return error;
				}
			}

			return null;
		}

		public bool SetError(string prop, string key, string message)
		{
			var bucket = GetBucket(prop);
			if(!bucket?.ContainsKey(key) ?? true)
			{
				return false;
			}

			bucket[key] = message;
			return true;
		}

		public int GetErrorCount(string prop)
		{
			var bucket = GetBucket(prop);
			return bucket?.Count ?? 0;
		}

		public override string ToString()
		{
			return JsonSerializer.Serialize(Errors);
		}

		public object ToObject()
		{
			return Errors;
		}

		public void Throw()
		{
			throw this;
		}

		public string this[string prop, string key] {
			get
			{
				return GetError(prop, key);
			}
			set
			{
				SetError(prop, key, value);
			}
		}

		public static implicit operator Exception(ValidationService s)
		{
			return new InvalidModelException(s);
		}

		public void Validate(object obj)
		{
			var context = new ValidationContext(obj, serviceProvider: _serviceProvider, items: null);
			var results = new List<ValidationResult>();
			bool isValid = Validator.TryValidateObject(obj, context, results, true);
			ResultsToDictionary(results);

			if(!isValid)
			{
				Throw();
			}
		}

		public void Validate<T>(T obj, Func<T, IEnumerable<ValidationResult>> validator)
		{
			var results = validator.Invoke(obj);
			ResultsToDictionary(results);

			if(results.Any())
			{
				Throw();
			}
		}

		public bool TryValidate(object obj)
		{
			var context = new ValidationContext(obj, serviceProvider: _serviceProvider, items: null);
			var results = new List<ValidationResult>();
			bool isValid = Validator.TryValidateObject(obj, context, results, true);
			ResultsToDictionary(results);

			return isValid;
		}

		public bool TryValidate<T>(T obj, Func<T, IEnumerable<ValidationResult>> validator)
		{
			var results = validator.Invoke(obj);
			ResultsToDictionary(results);

			return !results.Any();
		}

		private void ResultsToDictionary(IEnumerable<ValidationResult> results)
		{
			foreach(var res in results)
			{
				if(!res.MemberNames.Any())
				{
					AddError("Model", res.ErrorMessage);
				}
				else
				{
					foreach (var prop in res.MemberNames)
					{
						AddError(prop, res.ErrorMessage);
					}
				}
			}
		}

		private ErrorBucket AssureBucket(string prop)
		{
			ErrorBucket bucket;
			if (!Errors.TryGetValue(prop, out bucket))
			{
				bucket = new ErrorBucket();
				Errors.Add(prop, bucket);
			}
			return bucket;
		}

		private ErrorBucket GetBucket(string prop)
		{
			ErrorBucket bucket;
			return Errors.TryGetValue(prop, out bucket) ? bucket : null;
		}
	}
}
