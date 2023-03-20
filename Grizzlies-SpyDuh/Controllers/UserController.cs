using Grizzlies_SpyDuh.Models;
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

    /*---------------------------------------------*/

    [HttpGet("GetUserBySkill/{skillName}")]//Taco
    public IActionResult GetUser_BySkill(string skillName)//Taco
    {
        //return Ok(_userRepository.GetBySkill_1(skillName));//used Model User class:User
        return Ok(_userRepository.GetBySkill_2(skillName));//used Model User class: UserBySkill
    }
    /*---------------------------------------------*/
    [HttpGet("GetAll/Users")]//Taco
    public IActionResult GetAll_Users()//Taco
    {
        return Ok(_userRepository.GetAllUsers());
    }
    /*---------------------------------------------*/

    [HttpPost]
    public IActionResult Post(User user)
    {
        _userRepository.Add(user);
        return Created("", user);
    }


    /*---------------------------------------------*/

    //[HttpGet("GetSkillCounr/{SkillName}")]//Taco
    //public IActionResult CounSkill(string SkillName)//Taco
    //{
    //    return Ok(_userRepository.GetSkillCounr(SkillName));

    //}
}


