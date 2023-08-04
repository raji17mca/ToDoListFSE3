using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationMicroService.Models
{
    public class Login
    {
        [JsonProperty("userName")]
        [Required]
        public string UserName { get; set; }

        [JsonProperty("password")]
        [Required]
        public string Password { get; set; }

    }
}
