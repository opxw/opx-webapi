using System.ComponentModel.DataAnnotations;

namespace WebApiTemplate.Models
{
	public class TestModel
	{
		[Required]
		public string Id { get; set; }
	}
}
