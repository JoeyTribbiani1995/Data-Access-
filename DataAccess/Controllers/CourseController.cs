using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataAccess.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller
    {
        private readonly SchoolContext _context;

        public CourseController(SchoolContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Course), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Course>> Create([FromBody] Course temp)
        {
            try
            {
                if (temp != null)
                {
                    Course course = new Course
                    {
                        CourseID = temp.CourseID,
                        Title = temp.Title,
                        Credits = temp.Credits
                    };

                    _context.Add(course);
                    await _context.SaveChangesAsync();
                    return Ok(course);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                throw new Exception("Invalid create student", e);
            }
        }
    }
}
