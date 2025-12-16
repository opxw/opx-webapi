using Microsoft.AspNetCore.Mvc;
using Opx.WebApi.Common;
using Opx.WebApi.Handlers;
using WebApiTemplate.Models;

namespace WebApiTemplate.Controllers
{
	[Route("test")]
	[ApiController]
	[OpxApiExceptionHandler]
	[OpxApiFilterHandler]
	public class TestController : OpxApiController
	{
		[HttpGet, Route("exception")]
		public async Task ShowExceptionAsync()
		{
			throw new Exception("This is exception");
		}

		[HttpPost, Route("invalid-model")]
		public async Task TestInvalidModel([FromBody] TestModel model)
		{
			await OkAsync("oke");
		}
	}
}
