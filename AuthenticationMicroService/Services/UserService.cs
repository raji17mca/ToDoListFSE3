using AuthenticationMicroService.Entities;
using AuthenticationMicroService.Models;
using DnsClient;
using MongoDB.Driver;
using ToDoListMicroService.DataBaseConfig;

namespace AuthenticationMicroService.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _userService;
        public UserService(IUserDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _userService = database.GetCollection<User>(settings.CollectionName);
        }

        public bool IsUerExist(string email)
        {
           var response =  _userService.Find(x => x.UserName == email).FirstOrDefault();

            return response != null;
        }

        public Register Add(Register register)
        {
            var user = new User()
            {
                UserName = register.UserName,
                Password= register.Password
            };

            _userService.InsertOne(user);

            return register;
        }

        public bool IsValidUser(Login login)
        {
            var response =  _userService.Find( x => x.UserName.ToLower() == login.UserName.ToLower() && x.Password.ToLower() == login.Password.ToLower()).FirstOrDefault();

            return response != null;
        }
       
    }
}
