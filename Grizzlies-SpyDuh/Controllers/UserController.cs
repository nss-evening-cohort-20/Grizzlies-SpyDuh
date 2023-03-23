using Grizzlies_SpyDuh.Models;
using Grizzlies_SpyDuh.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

    [HttpGet("AllPaginatedWithSkills")]
    public IActionResult GetAllPaginatedUsersWithSkills(int page, int limit) 
    {
        int offset = 0;
        if (page != 1 && page != 0) { offset = page * limit; };
        (var users, int quantity) = _userRepository.GetAllUsersPaginatedWithSkills(offset, limit);
        HttpContext.Response.Headers.Add("X-Total-Count", quantity.ToString());
        return Ok(users);
    }

    [HttpGet("GetUserEnemies/{userName}")]//Taco
    public IActionResult Get_Enemies(string userName)//Taco
    {
        //return Ok(_userRepository.GetBySkill_1(skillName));
        return Ok(_userRepository.GetEnemies(userName));
    }
    /*---------------------------------------------*/
    [HttpPost]
    public IActionResult Post(User user)
    {
        _userRepository.Add(user);
        return Created("", user);
    }

    /*---------------------------------------------*/

    [HttpGet("GetSkillCounr/{SkillName}")]
    public IActionResult CounSkill(string SkillName)
    {
        return Ok(_userRepository.GetSkillCounr(SkillName));
    }


    [HttpGet("GetByAgency")]
    public IActionResult GetNonHandlerByAgencyId(int agencyId)
    {
        var users = _userRepository.GetNonHandlerByAgencyId(agencyId);
        return Ok(users);

    }

    [HttpPut("UserService/{id}")]
    public IActionResult UpdateUserService(int id, UserService userService)
    {
        if (id != userService.Id)
        {
            return BadRequest();
        }

        _userRepository.UpdateUserService(userService);
        return NoContent();
    }

    [HttpDelete("UserService")]
    public IActionResult DeleteUserService(int id)
    {
        _userRepository.DeleteUserService(id);
        return NoContent();
    }

    [HttpPut("UserSkill/{id}")]
    public IActionResult UpdateUserSkill(int id, UserSkill userSkill)
    {
        if (id != userSkill.Id)
        {
            return BadRequest();
        }

        _userRepository.UpdateUserSkill(userSkill);
        return NoContent();
    }

    [HttpDelete("UserSkill")]
    public IActionResult DeleteUserSkill(int id)
    {
        _userRepository.DeleteUserSkill(id);
        return NoContent();
    }


    /*---------------------------------------------*/

    [HttpGet("GetUserFriends/{name}")]
    public IActionResult Get_Friends(string name)
    {
        return Ok(_userRepository.GetUserFriends(name));
    }

}


