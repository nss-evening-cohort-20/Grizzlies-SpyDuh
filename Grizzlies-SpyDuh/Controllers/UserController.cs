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

    /*---------------------------------------------*/

    [HttpGet("GetUserBySkill/{skillName}")]//Taco
    public IActionResult GetUser_BySkill(string skillName)//Taco
    {
        //return Ok(_userRepository.GetBySkill_1(skillName));//used Model User class:User
        return Ok(_userRepository.GetBySkill_2(skillName));//used Model User class: UserBySkill
    }
    /*---------------------------------------------*/
}
