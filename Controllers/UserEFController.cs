using DotnetApi.Data;
using DotnetApi.models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetApi.Controllers;

// helps to send and receive json data
[ApiController]
// route connecter=> logic to find WeatherForecaseController
[Route("[controller]")]
// inherit ControllerBase class
public class UserEFController(IConfiguration config) : ControllerBase
{
    readonly DataContextEF entityFramework = new(config);

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        IEnumerable<User> users = entityFramework.Users.ToList<User>();
        return users;
    }

    [HttpGet("GetSingleUser/{id}")]
    public User GetSingleUser(int id)
    {
        User? user = entityFramework.Users.Where(u => u.UserId == id).FirstOrDefault<User>();
        if (user != null)
        {
            return user;
        }
        throw new Exception("Failed to get user");
    }

    [HttpPost("CreateUser")]
    public IActionResult CreateUser(User user)
    {
        User userDB = new()
        {
            Active = user.Active,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Gender = user.Gender,
            Email = user.Email
        };
        entityFramework.Add(userDB);
        if(entityFramework.SaveChanges()>0){
            return Ok();

        }
        throw new Exception("Unable to create a new user");
    }

    [HttpPut("UpdateUser")]
    public IActionResult UpdateUser(User user)
    {
        User? userDB = entityFramework
            .Users.Where(u => u.UserId == user.UserId)
            .FirstOrDefault<User>();

        if (userDB != null)
        {
            userDB.Active = user.Active;
            userDB.FirstName = user.FirstName;
            userDB.LastName = user.LastName;
            userDB.Gender = user.Gender;
            userDB.Email = user.Email;
            if (entityFramework.SaveChanges() > 0)
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
}
