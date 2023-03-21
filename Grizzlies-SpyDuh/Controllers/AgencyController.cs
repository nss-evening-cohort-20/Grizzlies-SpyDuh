using Grizzlies_SpyDuh.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Grizzlies_SpyDuh.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgencyController : ControllerBase
    {
        private readonly IAgencyRepository _agencyRepository;

        public AgencyController(IAgencyRepository agencyRepository)
        {
            _agencyRepository = agencyRepository;
        }

        [HttpGet("AgenciesAvgSkillLevel")]
        public IActionResult GetAllAvgSkill()
        {
            return Ok(_agencyRepository.GetAllAvgSkill());
        }
    }
}
