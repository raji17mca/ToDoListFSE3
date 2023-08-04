using MediatR;
using ToDoListMicroService.Filter;
using ToDoListMicroService.Models;

namespace ToDoListMicroService.Queries
{
    public class GetAllToDoListQuery : PaginationFilter, IRequest<PagedToDoListResponse>
    {
        public string UserId { get; set; }
    }
}
