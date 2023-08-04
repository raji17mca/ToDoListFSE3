namespace AuthenticationMicroService.Services
{
    public interface ITokenService
    {
        public string GenerateToken(string userName);
     
    }
}
