using AuthenticationMicroService.Models;
using MassTransit;

namespace ToDoListMicroService.Consumer
{
    public class UserConsumer: IConsumer<UserMessageQueue>
    {
        public async Task Consume(ConsumeContext<UserMessageQueue> context)
        {
            var data = context.Message;
           /// await _service.Delete(data.CompanyCode);
            Console.Write(data.UserId);
        }
    }
}
