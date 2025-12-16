using Microsoft.AspNetCore.Mvc.Filters;
using Opx.WebApi.Common;

namespace Opx.WebApi.Handlers
{
	public class OpxApiExceptionHandler : ExceptionFilterAttribute
	{
		public override async Task OnExceptionAsync(ExceptionContext context)
		{
			context.HttpContext.Items["StartTime"] = DateTime.UtcNow;
			context.ExceptionHandled = true;
			using (var responseObj = new ApiResponseObjectValue())
			{
				var errorValue = new ApiErrorValue()
				{
					Id = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName,
					Message = context.Exception.Message,
					ObjectName = responseObj.GetRouteNameFromContext(context)
				};

				await responseObj.ShowErrorResponseAsync(context.HttpContext, 500, errorValue);
			}

			return;
		}
	}
}