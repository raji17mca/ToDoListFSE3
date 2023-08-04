using MediatR;
using ToDoListMicroService.Models;
using ToDoListMicroService.Queries;
using ToDoListMicroService.Repository;

namespace ToDoListMicroService.QueriesHandler
{
    public class GetToDoListDetailsHandler: IRequestHandler<GetToDoListDetailsQuery, ToDoListResponseModel>
    {
        private readonly IToDoListQueryRepository _toDoListQueryRepository;

        public GetToDoListDetailsHandler(IToDoListQueryRepository toDoListQueryRepository)
        {
            _toDoListQueryRepository = toDoListQueryRepository;
        }

        public async Task<ToDoListResponseModel> Handle(GetToDoListDetailsQuery request, CancellationToken cancellationToken)
        {
           var response =  await _toDoListQueryRepository.GetByName(request.TaskName, request.UserId);

            if (response != null)
            {
                var toDoListResponseModel = new ToDoListResponseModel()
                {
                    Id = response.Id,
                    Name = response.Name,
                    Description = response.Description,
                    TotalEffort = response.TotalEffort,
                    Status = response.Status,
                    UserId = response.UserId,
                    EndDate = response.EndDate,
                    StartDate = response.StartDate
                };

                return toDoListResponseModel;
            }

            return null;
        }
    }
}
