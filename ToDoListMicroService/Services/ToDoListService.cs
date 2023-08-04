using ToDoListMicroService.Entities;
using ToDoListMicroService.Filter;
using ToDoListMicroService.Models;
using ToDoListMicroService.Repository;

namespace ToDoListMicroService.Services
{
    public class ToDoListService : IToDoListService
    {
        private IToDoListCommandRepository _toDoListRepository;

        public ToDoListService(IToDoListCommandRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
        }

        public async Task<ToDoListRequestModel> Create(ToDoListRequestModel toDoListRequestModel, string userId)
        {
            var request = new ToDoList()
            {
                Name = toDoListRequestModel.Name,
                Description = toDoListRequestModel.Description,
                TotalEffort = toDoListRequestModel.TotalEffort,
                UserId = userId,
                Status = toDoListRequestModel.Status.ToString(),
                StartDate = toDoListRequestModel.StartDate,
                EndDate = toDoListRequestModel.EndDate,
            };

           await _toDoListRepository.Create(request);

            return toDoListRequestModel;
            
        }

        public async Task<string> Update(string id, string status)
        {
           var response =  await _toDoListRepository.Update(id, status);
            return response;
        }
    }
}
