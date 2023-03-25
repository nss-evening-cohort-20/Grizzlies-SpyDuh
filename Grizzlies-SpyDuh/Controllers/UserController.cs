using Grizzlies_SpyDuh.Models;
using Grizzlies_SpyDuh.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;

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


    [HttpPut("UserUpdate1/{id}")]
    public IActionResult UpdateUser(int id, UserUD UserUD)
    {
        if (id != UserUD.Id)
        {
            return BadRequest();
        }

        _userRepository.UpdateUser(UserUD);
        return NoContent();
        //return new JsonResult(new JsonSerializerOptions()
        //{
        //    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        //});
    }

    [HttpPut("UserUpdate2/{id}")]
    public IActionResult UpdateUser2(int id, UserUpdate UserUpdate)
    {
        if (id != UserUpdate.Id)
        {
            return BadRequest();
        }

        _userRepository.UpdateUser2(UserUpdate);
        return NoContent();
    }


    [HttpDelete("User/{id}")]
    public IActionResult DeleteUser(int id)
    {
        _userRepository.DeleteUser(id);
        return NoContent();
    }

    /*---------------------------------------------*/

    [HttpGet("GetUserFriends/{name}")]
    public IActionResult Get_Friends(string name)
    {
        return Ok(_userRepository.GetUserFriends(name));
    }

}


