using LinqToDB.Repository;
using Microsoft.AspNetCore.Mvc;
using Opx.WebApi.Common;
using Opx.WebApi.Handlers;
using WebApiTemplate.Domain;
using WebApiTemplate.Models;

namespace WebApiTemplate.Controllers
{
	[Route("person")]
	[ApiController]
	[OpxApiExceptionHandler]
	[OpxApiFilterHandler]
	public class PersonController : OpxApiController
	{
		IDbRepository<Person> _personRepo;

		public PersonController(IDbRepository<Person> personRepo)
		{
			_personRepo = personRepo;
		}

		[HttpGet, Route("/")]
		public async Task GetAllPersonAsync()
		{
			var persons = await _personRepo.FindAsync();

			await OkAsync(persons);
		}
	}
}
