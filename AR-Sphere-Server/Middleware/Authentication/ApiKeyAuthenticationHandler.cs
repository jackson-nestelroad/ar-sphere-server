//using ARSphere.Services;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Caching.Memory;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http.Headers;
//using System.Security.Claims;
//using System.Text.Encodings.Web;
//using System.Threading.Tasks;

//namespace ARSphere.Middleware.Authentication
//{
//	public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationScheme>
//	{
//		private const ulong REQUEST_MAX_AGE_IN_SECONDS = 300;
//		private const string AUTHENTICATION_SCHEME = "amx";
//		private static readonly DateTime _1970 = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);

//		private readonly IMemoryCache _cache;

//		public ApiKeyAuthenticationHandler(
//			IMemoryCache memoryCache,
//			IOptionsMonitor<AuthenticationScheme> options,
//			ILoggerFactory logger,
//			UrlEncoder encoder,
//			ISystemClock clock)
//			: base(options, logger, encoder, clock)
//		{
//			_cache = memoryCache;
//		}

//		protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
//		{
//			if(!Context.Request.Headers.ContainsKey("Authorization"))
//			{
//				return AuthenticateResult.Fail("Missing Authorization Header.");
//			}

//			try
//			{
//				var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
//				var authorization = authHeader.ToString();
//				if(authorization.StartsWith(AUTHENTICATION_SCHEME))
//				{
//					var authArray = ParseAuthorizationHeaderValues(authorization);
//					if(authArray != null)
//					{
//						var apiKey = authArray[0];
//						var signatureHmac = authArray[1];
//						var nonce = authArray[2];
//						var timeStamp = authArray[3];

//						var isValid = await IsValidRequest(Context.Request, apiKey, signatureHmac, nonce, timeStamp);

//						if(isValid)
//						{
//							var identity = new ClaimsIdentity("API");
//							var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), null, "API");
//							return AuthenticateResult.Success(ticket);
//						}
//					}
//				}

//				return AuthenticateResult.Fail("Invalid Authorization Header.");
//			}
//			catch
//			{
//				return AuthenticateResult.Fail("Invalid Authorization Header.");
//			}
//		}

//		private async bool IsValidRequest(HttpRequest request, string appId, string signatureHmac, string nonce, string timeStamp)
//		{
			
//		}

//		private bool IsReplayRequest(string nonce, string timeStamp)
//		{
//			if(_cache.TryGetValue(nonce, out object _))
//			{
//				return true;
//			}

//			TimeSpan currentSpan = DateTime.UtcNow - _1970;
//			var serverTotalSeconds = Convert.ToUInt64(currentSpan.TotalSeconds);
//			var requestTotalSeconds = Convert.ToUInt64(timeStamp);

//			if(serverTotalSeconds - requestTotalSeconds > REQUEST_MAX_AGE_IN_SECONDS)
//			{
//				return true;
//			}

//			_cache.Set(nonce, timeStamp, DateTimeOffset.UtcNow.AddSeconds(REQUEST_MAX_AGE_IN_SECONDS));

//			return false;
//		}

//		public static string[] ParseAuthorizationHeaderValues(string rawAuthHeader)
//		{
//			var credArray = rawAuthHeader.Split(' ')[1].Split(':');
//			return credArray.Length == 4 ? credArray : null;
//		}
//	}
//}
