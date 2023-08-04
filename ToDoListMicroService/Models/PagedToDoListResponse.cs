namespace ToDoListMicroService.Models
{
    public class PagedToDoListResponse
    {
        public List<ToDoListResponseModel>? toDoListResponseModel { get; set; }

        public int ? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public int? TotalPage { get; set;}
        public int? TotalCount { get; set;
        }
    
    }
}
