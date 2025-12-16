using System.Text.Json.Serialization;

namespace Opx.WebApi.Common
{
	[Serializable]
	public class ApiErrorValue
	{
		[JsonPropertyName("code")]
		public string Message { get; set; } = "";
		[JsonPropertyName("id")]
		public string Id { get; set; } = "";
		[JsonPropertyName("objectName")]
		public string ObjectName { get; set; } = "";
	}
}
