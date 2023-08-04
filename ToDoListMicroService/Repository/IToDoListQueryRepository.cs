using ToDoListMicroService.Entities;

namespace ToDoListMicroService.Repository
{
    public interface IToDoListQueryRepository
    {
        IQueryable<ToDoList> Get(string userId);
        Task<ToDoList> GetByName(string taskName, string userId);
    }
}
