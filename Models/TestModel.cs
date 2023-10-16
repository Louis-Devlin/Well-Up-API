using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Well_Up_API.Models
{
	public class TestModel
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? Name { get; set; }
    }
}

