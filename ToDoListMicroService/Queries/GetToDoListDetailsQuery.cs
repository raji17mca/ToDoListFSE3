using MediatR;
using ToDoListMicroService.Models;

namespace ToDoListMicroService.Queries
{
    public class GetToDoListDetailsQuery: IRequest<ToDoListResponseModel>
    {
        public string TaskName { get; set; }

        public string UserId { get; set; }

    }
}
