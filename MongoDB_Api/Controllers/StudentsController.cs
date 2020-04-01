using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB_Api.Models;
using MongoDB_Api.Services;

namespace MongoDB_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private readonly StudentService _StudentService;

        public StudentsController(StudentService Studentservice)
        {
            _StudentService = Studentservice;
        }

        [HttpGet]
        public ActionResult<List<Student>> Get() =>
            _StudentService.Get();


        [HttpGet("{id:length(24)}", Name = "GetStudent")]
        public ActionResult<Student> Get(string id)
        {
            var student = _StudentService.Get(id);

            if (student == null)
                return NotFound();
            return student;


        }

        [HttpPost]
        public ActionResult<Student> Create(Student student)
        {
            _StudentService.Create(student);
            return CreatedAtAction("GetStudent", new { id = student.StudentId.ToString() });
        }



        [HttpPut("{id:length(24)}")]
        public ActionResult Update(string id, Student studentIn)
        {

            var student = _StudentService.Get(id);

            if (student == null)
                return NotFound();
            _StudentService.Update(id, studentIn);
            return CreatedAtAction("GetStudent", new { id = student.StudentId.ToString() });
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult Delete(string id)
        {

            var student = _StudentService.Get(id);

            if (student == null)
                return NotFound();
            _StudentService.Remove(student.StudentId);
            return NoContent();
        }




    }
}