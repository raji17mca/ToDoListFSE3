using MediatR;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ToDoListMicroService.Models
{
    public class ToDoListRequestModel : IValidatableObject
    {
        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }

        [JsonProperty("description")]
        [Required]
        public string Description { get; set; }

        [JsonProperty("startDatee")]
        [DataType(DataType.Date)]
        [Required] public DateTime StartDate { get; set; }

        [JsonProperty("endDate")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime EndDate { get; set; }

        [JsonProperty("status")]
        [Required]
        public string Status { get; set; }

        [JsonProperty("totalEffort")]
        [Required]
        [Range(0.1, int.MaxValue, ErrorMessage = "Total effort shold be greater than 0")]
        public decimal TotalEffort { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate.Date <= StartDate.Date)
            {
                yield return new ValidationResult("End date must be greater than the start date.", new[] { "EndDate" });
            }

            if (Status != StatusConstant.ToDo)
            {
                yield return new ValidationResult("Invalid status.", new[] { "Status" });
            }
        }

    }
}
