using MediatR;
using ToDoListMicroService.Commands;
using ToDoListMicroService.Models;
using ToDoListMicroService.Queries;
using ToDoListMicroService.Repository;
using ToDoListMicroService.Services;

namespace ToDoListMicroService.CommandsHandler
{
    public class UpdateToDoListRequestCommandHandler : IRequestHandler<UpdateToDoListRequest, string?>
        {
         
        private readonly IToDoListService _service;

        public UpdateToDoListRequestCommandHandler(IToDoListService toDoListService )
            {
            _service = toDoListService;
            }

        public async Task<string?> Handle(UpdateToDoListRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.Update(request.Id, request.Status);
            return result;
        }
    }
}
