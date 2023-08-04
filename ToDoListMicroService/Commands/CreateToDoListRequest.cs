using MediatR;
using ToDoListMicroService.Models;

namespace ToDoListMicroService.Commands
{
    public class CreateToDoListRequest: ToDoListRequestModel, IRequest<ToDoListRequestModel>
    {
        public string UserId { get; set; }
    }
}
