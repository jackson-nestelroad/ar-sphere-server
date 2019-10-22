using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ARSphere.Middleware.Validation
{
    public interface IValidationService
    {
        public bool IsValid();

        public void AddError(string prop, string message);
        public bool AddError(string prop, string key, string message);
        public bool RemoveError(string prop, string key);
        public string GetError(string prop, string key);
        public bool SetError(string prop, string key, string message);

        public int GetErrorCount(string prop);

        public string ToString();
        public object ToObject();
        public void Throw();

        public string this[string prop, string key] { get; set; }

        public void Validate(object obj);
        public void Validate<T>(T obj, Func<T, IEnumerable<ValidationResult>> validator);

        public bool TryValidate(object obj);
        public bool TryValidate<T>(T obj, Func<T, IEnumerable<ValidationResult>> validator);
    }
}
