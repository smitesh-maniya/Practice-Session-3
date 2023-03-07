using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PraticeSessionDemo.Models;
using PraticeSessionDemo.Services.StudentService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PraticeSessionDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IStudentService _student;
        private ILogger<UserController> _logger;

        public UserController(IStudentService student, ILogger<UserController> logger) 
        {
            _student = student;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> Get()
        {
            var result = await _student.GetAllStudents();
            
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}",Name ="Get")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            var result = await _student.GetSigleStudent(id);
            if(result == null) 
                return NotFound("Student Not Found.");
            _logger.LogInformation("Response: ",result.ToString());
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Student student)
        {
            await _student.AddStudent(student);
            return CreatedAtAction("Get",new {id = student.Id}, student);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Student student)
        {
            var result = await _student.UpdateStudent(id, student);
            if(!result) 
                return NotFound("Student not found");
            var getUpdatedStudent = await _student.GetSigleStudent(id);
            return Ok(getUpdatedStudent);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var result = await _student.DeleteStudent(id);
            if(!result) 
                return NotFound();
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchStudent(int id, [FromBody] JsonPatchDocument<Student> student)
        {
            var result = await _student.UpdateStudentPartially(id, student);

            if(!result) return NotFound("Not Modified.");

            return Ok();

        }
    }
}
