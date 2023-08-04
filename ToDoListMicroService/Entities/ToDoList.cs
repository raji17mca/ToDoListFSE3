using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace ToDoListMicroService.Entities
{
    [BsonIgnoreExtraElements]
    public class ToDoList
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        [Required]
        public string Description { get; set; }

        [BsonElement("startDateDate")]
        [Required]
        public DateTime StartDate { get; set; }

        [BsonElement("endDate")]
        [Required]
        public DateTime EndDate { get; set; }

        [BsonElement("status")]
        [Required]
        public string Status { get; set; }

        [BsonElement("userId")]
        [Required]
        public string UserId { get; set; }


        [BsonElement("totalEffort")]
        [Required]
        public decimal TotalEffort { get; set; }
    }
}
