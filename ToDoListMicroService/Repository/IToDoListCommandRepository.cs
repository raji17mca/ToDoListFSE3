using ToDoListMicroService.Entities;

namespace ToDoListMicroService.Repository
{
    public interface IToDoListCommandRepository
    {
        Task<ToDoList> Create(ToDoList toDoList);
        Task<string> Update(string id, string status);
    }
}
