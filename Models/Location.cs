using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
	public class Location
	{
		[Key] public int Id { get; set; }
		[Required] public string Name { get; set; }
		[Required] public string Address { get; set; }
		[Required] public float Lat { get; set; }
		[Required] public float Long { get; set; }
	}
}
