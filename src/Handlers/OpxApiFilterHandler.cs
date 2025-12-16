using Microsoft.AspNetCore.Mvc.Filters;
using Opx.WebApi.Common;

namespace Opx.WebApi.Handlers
{
	public class OpxApiFilterHandler : ActionFilterAttribute
	{
		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			context.HttpContext.Items["StartTime"] = DateTime.UtcNow;

			if (!context.ModelState.IsValid)
			{
				var errorNames = string.Join(",", context.ModelState.Keys.ToList());

				using (var responseObj = new ApiResponseObjectValue())
				{
					var errorValue = new ApiErrorValue()
					{
						Message = "No data sent : " + errorNames,
						ObjectName = responseObj.GetRouteNameFromContext(context),
						Id = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName,
					};

					await responseObj.ShowErrorResponseAsync(context.HttpContext, 400, errorValue);
				}
			}
			else
			{
				await base.OnActionExecutionAsync(context, next);
			}

			return;
		}
	}
}
