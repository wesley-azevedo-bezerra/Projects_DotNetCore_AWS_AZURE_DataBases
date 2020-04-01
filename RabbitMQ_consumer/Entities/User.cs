using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ_consumer.Entities
{
    public class User
    {

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public bool Active { get; set; }

        public int NumberOfChildren { get; set; }

        public List<string> Interests { get; set; }

        public string FullName
        {
            get { return $"{ FirstName} {LastName}"; }
        }
    }
}
