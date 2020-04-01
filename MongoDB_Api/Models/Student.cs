using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


namespace MongoDB_Api.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [BsonElement("School")]
        public string Department { get; set; }

    }
}
