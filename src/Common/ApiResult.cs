using System.Text.Json.Serialization;

namespace Opx.WebApi.Common
{
	[Serializable]
	public class ApiResult
	{
		[JsonPropertyName("result")]
		public bool Result { get; set; }
		[JsonPropertyName("data")]
		public dynamic? Data { get; set; }
		[JsonPropertyName("statusCode")]
		public string StatusCode { get; set; }
	}
}
