using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DynamoDB_DataBase.Entities
{
    [DynamoDBTable("Users")]
    public class User
    {

        [DynamoDBProperty("Id")]
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public bool Active { get; set; }

        public int NumberOfChildren { get; set; }

        public List<string> Interests { get; set; }

        [DynamoDBIgnore]
        public string FullName
        {
            get { return $"{ FirstName} {LastName}"; }
        }
    }
}
