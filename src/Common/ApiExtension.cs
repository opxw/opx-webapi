using System.Net;

namespace Opx.WebApi.Common
{
	public static class ApiExtension
	{
		public static async Task HandleUncatchedStatusCodeAsync(this WebApplication app, HttpContext context, string sender = "")
		{
			var statusCode = context.Response.StatusCode;

			if (statusCode == (int)HttpStatusCode.OK || statusCode == (int)HttpStatusCode.RedirectKeepVerb ||
				statusCode == (int)HttpStatusCode.Redirect)
				return;

			using (var responseObj = new ApiResponseObjectValue())
			{
				var controller = context.Request.RouteValues["controller"];
				var action = context.Request.RouteValues["action"];

				var error = new ApiErrorValue()
				{
					Id = action != null ? action.ToString() : ((HttpStatusCode)context.Response.StatusCode).ToString(),
					ObjectName = controller != null ? controller.ToString() : context.Request.Path,
				};

				string message = statusCode switch
				{
					(int)HttpStatusCode.Unauthorized => "Unauthorized",
					(int)HttpStatusCode.BadRequest => "Bad request",
					(int)HttpStatusCode.Forbidden => "Forbidden",
					(int)HttpStatusCode.NotFound => "Not found",
					(int)HttpStatusCode.MethodNotAllowed => "HTTP Method not allowed",
					(int)HttpStatusCode.ServiceUnavailable => "Unvailable",
					(int)HttpStatusCode.NoContent => "No content",
					(int)HttpStatusCode.InternalServerError => "Internal server error",
					(int)HttpStatusCode.UnsupportedMediaType => "Unsupported Media Type"
				};

				error.Message = message;

				await responseObj.ShowErrorResponseAsync(context, statusCode, error);
			}
		}
	}
}
