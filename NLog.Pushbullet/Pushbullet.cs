using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NLog.Pushbullet {

	public class Pushbullet {

		public const string BaseApiUrl = "https://api.pushbullet.com/v2/";
		public const string AuthorizationKeyHeader = "Authorization";
		public const string AuthorizationKeyValueFormat = "Bearer {0}";

		public const string PushTypeNote = "note";
		public const string PushTypeFile = "file";
		public const string PushTypeLink = "link";
		
		public static Task<dynamic> PushNote(string accessToken, string title, string body) {
			return Push(accessToken, JsonConvert.SerializeObject(new {
				type = PushTypeNote,
				title = title,
				body = body
			}));
		}

		public static Task<dynamic> PushFile(string accessToken, string title, string body, string fileName, string fileType, string fileUrl) {
			return Push(accessToken, JsonConvert.SerializeObject(new {
				type = PushTypeFile,
				title = title,
				body = body,
				file_name = fileName,
				file_type = fileType,
				file_url = fileUrl
			}));
		}

		public static Task<dynamic> PushLink(string accessToken, string title, string body, string link) {
			return Push(accessToken, JsonConvert.SerializeObject(new {
				type = PushTypeLink,
				title = title,
				body = body,
				url = link
			}));
		}

		public static Task<dynamic> Push(string accessToken, object json) {
			return Task.Run(async () => {
				if (json == null) {
					throw new ArgumentNullException("json");
				}

				using (HttpClient httpClient = new HttpClient()) {
					httpClient.DefaultRequestHeaders.Add(AuthorizationKeyHeader, String.Format(AuthorizationKeyValueFormat, accessToken));

					HttpResponseMessage response = await httpClient.PostJsonAsync(String.Concat(BaseApiUrl, "pushes"), json);

					if (!response.IsSuccessStatusCode) {
						throw new HttpRequestException(String.Format("{0} {1} - Invalid status code received from Pushbullet", (int)response.StatusCode, response.StatusCode));
					}

					return JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
				}
			});
		}

	}

}
