using Microsoft.AspNetCore.Authorization;
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
	[Authorize]

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

		[HttpPost, Route("oke")]
		public async Task TestOke()
		{
			await OkAsync("oke");
		}
	}
}
