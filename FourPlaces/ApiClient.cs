using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common.Api.Dtos;
using Newtonsoft.Json;
using TD.Api.Dtos;

namespace FourPlaces
{
	public class ApiClient
	{
		public const string API = "https://td-api.julienmialon.com/";
		public const string LOGIN = "https://td-api.julienmialon.com/auth/login";
		public const string REFRESH = "https://td-api.julienmialon.com/auth/refresh";
		public const string REGISTER = "https://td-api.julienmialon.com/auth/register";
		public const string ME = "https://td-api.julienmialon.com/me";
		public const string ME_PASSWORD = "https://td-api.julienmialon.com/me/password";
		public const string LIST_PLACES = "https://td-api.julienmialon.com/places";
		public const string GET_PLACE = "https://td-api.julienmialon.com/places/";
		public const string CREATE_PLACE = "https://td-api.julienmialon.com/places";
		public const string CREATE_COMMENT = "https://td-api.julienmialon.com/places/";
		public const string CREATE_IMAGE = "https://td-api.julienmialon.com/images"; 
		public const string GET_IMAGE = "https://td-api.julienmialon.com/images/"; 
		
		private readonly HttpClient _client = new HttpClient();
		public LoginResult Token { get; set; }

		public async Task<HttpResponseMessage> Execute(HttpMethod method, string url, object data = null, string token = null)
		{
			HttpRequestMessage request = new HttpRequestMessage(method, url);

			if (data != null)
			{
				request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
			}

			if (token != null)
			{
				request.Headers.Add("Authorization", $"Bearer {token}");
			}

			return await _client.SendAsync(request);
		}

		public async Task<T> ReadFromResponse<T>(HttpResponseMessage response)
		{
			string result = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<T>(result);
		}
	}
}