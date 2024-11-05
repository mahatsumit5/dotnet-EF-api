using AutoMapper;
using DotnetApi.Data;
using DotnetApi.dtos;
using DotnetApi.models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetApi.Controllers;

// helps to send and receive json data
[ApiController]
// route connecter=> logic to find WeatherForecaseController
[Route("[controller]")]
// inherit ControllerBase class
public class UserEFController(IUserRepository userRepository) : ControllerBase
{
    readonly IMapper _mapper = new Mapper(
        new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserToAddDto, User>();
        })
    );

    readonly IUserRepository _userRepository = userRepository;

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        return _userRepository.GetUsers();
    }

    [HttpGet("GetSingleUser/{id}")]
    public User GetSingleUser(int id)
    {
        // User? user = entityFramework.Users.Where(u => u.UserId == id).FirstOrDefault<User>();
        // if (user != null)
        // {
        //     return user;
        // }
        // throw new Exception("Failed to get user");

        return _userRepository.GetSingleUser(id);
    }

    [HttpPost("CreateUser")]
    public IActionResult CreateUser(User user)
    {
        User userDB =
            new()
            {
                Active = user.Active,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                Email = user.Email,
            };
        _userRepository.AddEntity<User>(userDB);
        if (_userRepository.SaveChanges())
        {
            return Ok();
        }
        throw new Exception("Unable to create a new user");
    }

    [HttpPut("UpdateUser")]
    public IActionResult UpdateUser(User user)
    {
        User? userDB = _userRepository.GetSingleUser(user.UserId);

        if (userDB != null)
        {
            userDB.Active = user.Active;
            userDB.FirstName = user.FirstName;
            userDB.LastName = user.LastName;
            userDB.Gender = user.Gender;
            userDB.Email = user.Email;
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            else
            {
                throw new Exception("Failed to update user");
            }
        }
        throw new Exception("Failed to get user to update");
    }

    [HttpDelete("DeleteUser/{id}")]
    public IActionResult DeleteUser(int id)
    {
        return Ok();
    }
}
