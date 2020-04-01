using MongoDB.Driver;
using MongoDB_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB_Api.Services
{
    public class StudentService
    {
        private readonly IMongoCollection<Student> _Students;

        public StudentService(IStudentDatabaseSettings settings)
        {

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DataBaseName);
            _Students = database.GetCollection<Student>(settings.StudentCollectionName);

        }


        public List<Student> Get() =>
            _Students.Find(student => true).ToList();

        public Student Get(string id) =>
            _Students.Find<Student>(student => student.StudentId == id).FirstOrDefault();

        public Student Create(Student student)
        {
            _Students.InsertOne(student);
            return student;
        }

        public void Update(string id, Student studentIn) =>
            _Students.ReplaceOne(student => student.StudentId == id, studentIn);


        public void Remove(Student studentIn) =>
            _Students.DeleteOne(student => student.StudentId == studentIn.StudentId);


        public void Remove(string id) =>
       _Students.DeleteOne(student => student.StudentId == id);




    }
}
