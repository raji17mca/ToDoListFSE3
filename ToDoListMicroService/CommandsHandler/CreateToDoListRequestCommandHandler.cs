using MediatR;
using ToDoListMicroService.Commands;
using ToDoListMicroService.Models;
using ToDoListMicroService.Queries;
using ToDoListMicroService.Repository;
using ToDoListMicroService.Services;

namespace ToDoListMicroService.CommandsHandler
{
    public class CreateToDoListRequestCommandHandler : IRequestHandler<CreateToDoListRequest, ToDoListRequestModel>
        {
         
        private readonly IToDoListService _service;

        public CreateToDoListRequestCommandHandler(IToDoListService toDoListService )
            {
            _service = toDoListService;
            }

        public async Task<ToDoListRequestModel> Handle(CreateToDoListRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.Create(request, request.UserId);
            return result;
        }
    }
}
