using Microsoft.AspNetCore.Mvc;

namespace Opx.WebApi.Common
{
	public class OpxApiController : ControllerBase
	{
		protected async Task OkAsync(dynamic data, double? elapsed = null)
		{
			using (var responseObj = new ApiResponseObjectValue())
			{
				await responseObj.ShowResponseAsync(HttpContext, data, elapsed);
			}
			;
		}

		protected async Task FailAsync(dynamic data, int statusCode = 200, double? elapsed = null)
		{
			using (var responseObj = new ApiResponseObjectValue())
			{
				await responseObj.ShowErrorResponseAsync(HttpContext, statusCode, data, elapsed);
			}
			;
		}
	}
}