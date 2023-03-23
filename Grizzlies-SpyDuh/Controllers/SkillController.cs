using Grizzlies_SpyDuh.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Grizzlies_SpyDuh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillRepository _skillRepository; 

        public SkillController(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        [HttpGet("AllSkills")]
        public IActionResult Get()
        {
            return Ok(_skillRepository.GetAll());
        }

    }
}
