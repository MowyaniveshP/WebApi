using WebApi.Models;

namespace WebApi.Repositories
{
	public interface IEventRepository
	{
		Task<IList<Event>> GetEventsAsync(); //get all rec from DB
		Task<Event> GetEventAsync(uint id); //get individual rec from DB

		//Task<bool> EventsExistsAsync(uint id); //to check existing rec
		Task<bool> SaveEventsAsync(); //to save to DB
		Task<bool> SaveAsync(); //save DB changes
		Task<bool> DeleteDataAsync(); //delete DB data
	}
}
