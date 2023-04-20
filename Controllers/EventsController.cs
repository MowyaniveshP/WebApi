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

		[HttpGet(Name = "GetEvents")]
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

		[HttpGet("{id}", Name = "GetEvent")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Event>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult<Event>> GetEventAsync(uint id)
		{
			Event result = await _eventRepository.GetEventAsync(id);
			if (result != null)
			{
				return Ok(result);
			}
			return BadRequest();
		}

		[HttpPost(Name = "SaveEventsFromURL")]
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

		[HttpDelete]
		public async Task<ActionResult> Delete()
		{
			await _eventRepository.DeleteDataAsync();
			return Ok();
		}
	}
}
