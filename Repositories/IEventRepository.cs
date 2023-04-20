using WebApi.Models;

namespace WebApi.Repositories
{
	public interface IEventRepository
	{
		Task<IList<Event>> GetEventsAsync(); //get all rec from DB
		Task<bool> SaveEventsAsync(); //to save to DB
		Task<bool> SaveAsync(); //save DB changes
		Task<bool> DeleteDataAsync(); //delete DB data
	}
}
