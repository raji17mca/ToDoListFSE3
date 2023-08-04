using MediatR;
using ToDoListMicroService.Models;

namespace ToDoListMicroService.Commands
{
    public class UpdateToDoListRequest : IRequest<string>
    {
        public string Id { get; set; }
        public string Status { get; set; }
    }
}
