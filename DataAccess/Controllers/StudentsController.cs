using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Models;
using DataAccess.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataAccess.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;
        private readonly IMapper _mapper;

        public StudentsController(SchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        /// <summary>
        /// Get all student in table and map to model view student 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllStudent")]
        [ProducesResponseType(typeof(IEnumerable<StudentViewModel>),(int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<StudentViewModel>>> GetAllStudentAsync()
        {
            var temp = await _context.Students.ToListAsync();
            var studentViewModel = _mapper.Map<StudentViewModel[]>(temp);
            if(studentViewModel == null)
            {
                return BadRequest("Invalid get all student");
            }
            return Ok(studentViewModel);
        }

        [HttpGet]
        [Route("Details")]
        [ProducesResponseType(typeof(IEnumerable<Student>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Student>>> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Student), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Student>> Create([FromBody] Student student)
        {
            try
            {
                if (student != null)
                {
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    return Ok(student);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                throw new Exception("Invalid create student",e);
            }
        }
    }
}
