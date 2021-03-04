using backend_week3.Data;
using backend_week3.DTO;
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
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetBooks()
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
        public ActionResult<StudentDTO> GetBooks_byId(int id)
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
    }
}
