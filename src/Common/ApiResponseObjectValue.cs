using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace Opx.WebApi.Common
{
	internal class ApiResponseObjectValue : IDisposable
	{
		private bool disposedValue;

		public async Task ShowErrorResponseAsync(HttpContext context, int? statusCode, dynamic errorValue,
	double? elapsedTime = null)
		{
			var originStatusCode = statusCode != null ? statusCode : context.Response.StatusCode;
			var result = new ApiResult()
			{
				Result = false,
				Data = errorValue,
				StatusCode = originStatusCode.ToString()
			};
			var response = JsonSerializer.Serialize(result);
			var executionTime = elapsedTime != null ? (double)elapsedTime : GetExecutionTime(context);

			context.Response.ContentType = "application/json";
			context.Response.StatusCode = 200;
			context.Response.ContentLength = response.Length;
			context.Response.Headers.Add("Execution-Time", executionTime.ToString());

			await context.Response.WriteAsync(response);
			await context.Response.CompleteAsync();

			return;
		}

		public async Task ShowResponseAsync(HttpContext context, dynamic data, double? elapsedTime = null)
		{
			var result = new ApiResult()
			{
				Result = true,
				Data = data,
				StatusCode = "200"
			};
			var response = JsonSerializer.Serialize(result);
			var executionTime = elapsedTime != null ? (double)elapsedTime : GetExecutionTime(context);

			context.Response.Headers.ContentType = "application/json";
			context.Response.StatusCode = 200;
			context.Response.ContentLength = response.Length;
			context.Response.Headers.Add("Execution-Time", executionTime.ToString());

			await context.Response.WriteAsync(response);

			return;
		}

		public string GetRouteNameFromContext(FilterContext context)
		{
			var routeName = context.RouteData.Values["Controller"].ToString();
			return !string.IsNullOrWhiteSpace(routeName) ? routeName : string.Empty;
		}

		public double GetExecutionTime(HttpContext context, bool inSec = true)
		{
			double result = 0;

			var startTimeObj = context.Items["StartTime"];

			if (startTimeObj != null)
			{
				var startTime = (DateTime)startTimeObj;
				result = (DateTime.UtcNow - startTime).TotalMilliseconds;
			}

			if (inSec)
				result = TimeSpan.FromMilliseconds(result).TotalSeconds;

			return Math.Round(result, 4);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~ApiResponseObjectValue()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
