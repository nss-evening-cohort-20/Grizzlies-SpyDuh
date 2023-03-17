using Grizzlies_SpyDuh.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Grizzlies_SpyDuh.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("{id}/SkillsServices")]
    public IActionResult GetByIdWithSkillsServices(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        var user = _userRepository.GetByIdWithSkillsAndServices(id);
        if (user == null)
        {
            return BadRequest();
        }
        return Ok(user);
    }
}
