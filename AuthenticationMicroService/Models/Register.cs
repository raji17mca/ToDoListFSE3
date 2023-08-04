using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AuthenticationMicroService.Models
{
   
    public class Register
    {

        [JsonProperty("userName")]
        [Required]
        public string UserName { get; set; }

        [JsonProperty("password")]
        [Required]
        public string Password { get; set; }

    }
}
