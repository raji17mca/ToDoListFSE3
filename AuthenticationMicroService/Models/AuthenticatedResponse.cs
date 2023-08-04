using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationMicroService.Models
{
     public class AuthenticatedResponse
        {
            [JsonProperty("token")]
            public string Token { get; set; }

            [JsonProperty("statusCode")]
            public int StatusCode { get; set; }
        }
    }
