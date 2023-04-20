using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using WebApi.Models;

namespace WebApi.Repositories
{
	public class EventRepository : IEventRepository
	{
		private readonly IConfiguration _config;
		private readonly Context _db;
		public EventRepository(Context db, IConfiguration config)
		{
			_config = config;
			_db = db;
		}
		public async Task<bool> SaveEventsAsync()
		{
			//string url = "https://opencollective.com/sustainoss/events.json";
			string URL = _config["URL"].ToString();

			HttpClient httpClient = new();

			var httpResponse = await httpClient.GetAsync(URL);
			string jsonResponse = await httpResponse.Content.ReadAsStringAsync();
			var myEvents = JsonConvert.DeserializeObject<IList<Event>>(jsonResponse);

			if (myEvents != null && GetEventsAsync().Result.IsNullOrEmpty())
			{
				foreach (var item in myEvents)
				{
					await _db.Events.AddAsync(item);
				}
			}
			return await SaveAsync();
		}

		//public async Task<bool> EventsExistsAsync(uint id)
		//{
		//	bool eventExists = await _db.Events.AnyAsync(o => o.Id == id);
		//	var locationId = 0;
		//	if (eventExists == true)
		//	{
		//		Event eve = _db.Events.FirstOrDefault(o => o.Id == id);
		//		if (eve != null)
		//		{
		//			locationId = eve.LocationId;
		//		}
		//	}
		//	bool locationExists = await _db.Locations.AnyAsync(o => o.Id == locationId);

		//	return (eventExists);

		//}

		public async Task<IList<Event>> GetEventsAsync()
		{
			List<Event> events = await _db.Events.Include(o => o.Location).ToListAsync();
			return events;
		}

		public async Task<Event> GetEventAsync(uint id)
		{
			Event eventbyId = await _db.Events.Include(o => o.Location).FirstOrDefaultAsync(o => o.Id == id);
			return eventbyId;
		}

		public async Task<bool> SaveAsync()
		{
			bool value = await _db.SaveChangesAsync() >= 0;
			return value;
		}

		public async Task<bool> DeleteDataAsync()
		{
			List<Event> events = await _db.Events.Include(o => o.Location).ToListAsync();
			foreach (var item in events)
			{
				_db.Events.Remove(item);
				_db.Locations.Remove(item.Location);
			}
			return await SaveAsync();
		}
	}
}
