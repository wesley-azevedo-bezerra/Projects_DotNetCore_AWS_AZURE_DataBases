using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB_Api.Models
{
    public class StudentDatabaseSettings : IStudentDatabaseSettings
    {
        public string StudentCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DataBaseName { get; set; }
    }

    public interface IStudentDatabaseSettings
    {
        string StudentCollectionName { get; set; }

        string ConnectionString { get; set; }


        string DataBaseName { get; set; }
    }


}
