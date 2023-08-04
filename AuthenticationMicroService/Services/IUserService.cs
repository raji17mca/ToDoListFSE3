using AuthenticationMicroService.Models;

namespace AuthenticationMicroService.Services
{
    public interface IUserService
    {
        public Register Add(Register register);

        public bool IsValidUser(Login login);

        public bool IsUerExist(string email);
       
    }
}
