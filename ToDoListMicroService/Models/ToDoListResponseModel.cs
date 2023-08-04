using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ToDoListMicroService.Models
{
    public class ToDoListResponseModel
    {
        [JsonProperty("id")]
        [Required]
        public string Id { get; set; }


        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }

        [JsonProperty("description")]
        [Required]
        public string Description { get; set; }

        [JsonProperty("startDateDate")]
        [Required]
        public DateTime StartDate { get; set; }

        [JsonProperty("endDate")]
        [Required]
        public DateTime EndDate { get; set; }

        [JsonProperty("status")]
        [Required]
        public string Status { get; set; }

        [JsonProperty("userId")]
        [Required]
        public string UserId { get; set; }

        [JsonProperty("totalEffort")]
        [Required]
        public decimal TotalEffort { get; set; }
    }
}
