using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
	public class Event
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public Guid GUID { get; set; }
		[Required] public uint Id { get; set; }
		[Required] public string Name { get; set; }
		[Required] public string Description { get; set; }
		[Required] public string Slug { get; set; }
		[Required] public string Image { get; set; }
		[Required] public string StartsAt { get; set; }
		[Required] public string EndsAt { get; set; }
		[Required] public string Timezone { get; set; }
		[Required] public int LocationId { get; set; }
		[ForeignKey("LocationId")]
		public virtual Location Location { get; set; }
		[Required] public string Url { get; set; }
		[Required] public string Info { get; set; }
	}
}
