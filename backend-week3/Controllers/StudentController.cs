using backend_week3.Data;
using backend_week3.DTO;
using backend_week3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_week3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly Context _context;

        public StudentController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            var students = from student in _context.Student
                       join student_description in _context.Student_description on student.id equals student_description.id
                       select new StudentDTO
                       {
                           id = student.id,
                           grade = student.grade,
                           students_id = student_description.students_id,
                           age = student_description.age,
                           first_name = student_description.first_name,
                           last_name = student_description.last_name,
                           address = student_description.address,
                           country = student_description.country,

                       };

            return await students.ToListAsync();
        }

        [HttpGet("{id}")]
        public ActionResult<StudentDTO> GetStudents_byId(int id)
        {
            var students = from student in _context.Student
                           join student_description in _context.Student_description on student.id equals student_description.id
                           select new StudentDTO
                       {
                               id = student.id,
                               grade = student.grade,
                               students_id = student_description.students_id,
                               age = student_description.age,
                               first_name = student_description.first_name,
                               last_name = student_description.last_name,
                               address = student_description.address,
                               country = student_description.country,
                           };

            var students_by_id = students.ToList().Find(x => x.students_id == id);

            if (students_by_id == null)
            {
                return NotFound();
            }
            return students_by_id;
        }

        [HttpPost]
        public async Task<ActionResult<StudentDTO>> Add_Students(AddStudent studentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = new Student()
            {
                grade = studentDTO.grade

            };
            await _context.Student.AddAsync(student);
            await _context.SaveChangesAsync();

            var student_description = new Student_description()
            {
                students_id = studentDTO.students_id,
                age = studentDTO.age,
                first_name = studentDTO.first_name,
                last_name = studentDTO.last_name,
                address = studentDTO.address,
                country = studentDTO.country,
            };
            await _context.AddAsync(student_description);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudents", new { id = student.id }, studentDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> Delete_Student(int id)
        {
            var student = _context.Student.Find(id);
            var student_description = _context.Student_description.SingleOrDefault(x => x.id == id);

            if (student == null)
            {
                return NotFound();
            }
            else
            {
                _context.Remove(student);
                _context.Remove(student_description);
                await _context.SaveChangesAsync();
                return student;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update_Students(int id, StudentDTO student)
        {
            if (id != student.students_id || !StudentExists(id))
            {
                return BadRequest();
            }
            else
            {
                var students = _context.Student.SingleOrDefault(x => x.id == id);
                var student_description = _context.Student_description.SingleOrDefault(x => x.id == id);

                students.grade = student.grade;
                student_description.students_id = student.students_id;
                student_description.age = student.age;
                student_description.first_name = student.first_name;
                student_description.last_name = student.last_name;
                student_description.address = student.address;
                student_description.country = student.country;
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(x => x.id == id);
        }
    }
}
