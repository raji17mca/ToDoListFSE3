using MediatR;
using ToDoListMicroService.Models;
using ToDoListMicroService.Repository;
using System.Linq;
using ToDoListMicroService.Queries;

namespace ToDoListMicroService.QueriesHandler
{
    public class GetAllToDoListQueryHandler : IRequestHandler<GetAllToDoListQuery, PagedToDoListResponse>
    {
        private readonly IToDoListQueryRepository _toDoListQueryRepository;

        public GetAllToDoListQueryHandler(IToDoListQueryRepository toDoListQueryRepository)
        {
            _toDoListQueryRepository = toDoListQueryRepository;
        }

        public async Task<PagedToDoListResponse> Handle(GetAllToDoListQuery request, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
             {
                 var result = new PagedToDoListResponse();

                 var skipIndex = (request.PageNumber - 1) * request.PageSize;
                 var pageSize = request.PageSize;
                 var totalPage = 0;

                 // pagenation
                 var response = _toDoListQueryRepository.Get(request.UserId).Skip(skipIndex).Take(pageSize);

                 // Filter
                 if (request?.SearchTerm != null)
                 {
                     response = response?.Where(x => x.Name.ToLower().Contains(request.SearchTerm));
                 }

                 // Sorting
                 response = response?.OrderByDescending(x => x.TotalEffort);

                 var totalCount = response?.Count() ?? 0;
                

                 if (request != null)
                 {
                     totalPage = (int)Math.Ceiling((double)(totalCount / pageSize));
                 }

                 var toDoListResponseModelList = new List<ToDoListResponseModel>();

                 if (response != null && response.Count() > 0)
                 {
                     foreach (var item in response)
                     {
                         var toDoListResponseModel = new ToDoListResponseModel()
                         {
                             Id = item.Id,
                             Name = item.Name,
                             Description = item.Description,
                             TotalEffort = item.TotalEffort,
                             Status = item.Status,
                             UserId = item.UserId,
                             EndDate = item.EndDate,
                             StartDate = item.StartDate
                         };
                         toDoListResponseModelList.Add(toDoListResponseModel);
                     }
                 }

                 result.toDoListResponseModel = toDoListResponseModelList;
                 result.TotalCount = totalCount;
                 result.PageNumber = request?.PageNumber;
                 result.TotalPage = totalPage;
                 result.PageSize = pageSize;

                 return result;

             });
        }
    }
}
