using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EventsController : ControllerBase
	{
		private readonly IEventRepository _eventRepository;

		public EventsController(IEventRepository eventRepository)
		{
			_eventRepository = eventRepository;
		}

		[HttpGet(Name = "GetEvents")] // get endpoint to get all events from local DB
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Event>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult<List<Event>>> GetEventsAsync()
		{
			{
				List<Event> result = (List<Event>)await _eventRepository.GetEventsAsync();
				if (result.IsNullOrEmpty())
				{
					return BadRequest();
				}
				return Ok(result);
			}
		}

		[HttpPost(Name = "SaveEventsFromURL")] // endpoint to retrieve and store data from sourceURL into DB
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Event>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult> SaveEventsFromURLAsync()
		{
			bool eventsCreated = await _eventRepository.SaveEventsAsync();
			if (eventsCreated)
			{
				return Ok(eventsCreated);
			}
			return BadRequest();
		}

		[HttpDelete] // delete endpoint to delete all db records
		public async Task<ActionResult> Delete()
		{
			await _eventRepository.DeleteDataAsync();
			return Ok();
		}
	}
}
