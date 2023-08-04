using ToDoListMicroService.Entities;
using ToDoListMicroService.Models;

namespace ToDoListMicroService.Services
{
    public interface IToDoListService
    {
        Task<ToDoListRequestModel> Create(ToDoListRequestModel toDoListRequestModel, string userId);
       
        Task<string> Update(string id, string status);
    }
}
