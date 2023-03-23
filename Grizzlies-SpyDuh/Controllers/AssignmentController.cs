using Grizzlies_SpyDuh.Models;
using Grizzlies_SpyDuh.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Grizzlies_SpyDuh.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentRepository _assignmentRepository;

        public AssignmentController(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var assignments = _assignmentRepository.GetAll();
            if (assignments !=null) return Ok(assignments);
            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var assignment = _assignmentRepository.GetById(id);
            if (assignment != null) return Ok(assignment);
            return BadRequest();
        }

        [HttpGet("agency/{id}")]
        public IActionResult GetByAgency(int id) 
        {
            var assignments = _assignmentRepository.GetByAgencyId(id);
            if (assignments != null) return Ok(assignments);
            
            return BadRequest();
        }

        [HttpGet("ongoing")]
        public IActionResult GetAllOngoing()
        {
            var assignments = _assignmentRepository.GetAllOngoingAssignments();
            if (assignments != null) return Ok(assignments);

            return BadRequest();
        }

        [HttpGet("ongoingByAgency/{id}")]
        public IActionResult GetOngoingByAgency(int id)
        {
            var assignments = _assignmentRepository.GetOngoingAssignmentsByAgency(id);
            if (assignments != null) return Ok(assignments);

            return BadRequest();
        }

        [HttpGet("ongoingByUser/{id}")]
        public IActionResult GetOngoingByUser(int id)
        {
            var assignments = _assignmentRepository.GetOngoingAssignmentsByUser(id);
            if (assignments != null) return Ok(assignments);

            return BadRequest();
        }

        [HttpPost]
        public IActionResult Post(Assignment assignment)
        {
            _assignmentRepository.Add(assignment);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Assignment assignment)
        {
            _assignmentRepository.Update(id, assignment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _assignmentRepository.Delete(id);
            return NoContent();
        }
    }
}
