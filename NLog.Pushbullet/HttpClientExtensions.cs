using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NLog.Pushbullet {

	internal static class HttpClientExtensions {

		public static Task<HttpResponseMessage> PostJsonAsync(this HttpClient httpClient, string requestUri, object json) {
			return httpClient.PostAsync(requestUri, new StringContent(json.ToString(), Encoding.UTF8, "application/json"));
		}

	}

}
