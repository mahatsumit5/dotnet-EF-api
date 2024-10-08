using DotnetApi.Data;
using DotnetApi.models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetApi.Controllers;

// helps to send and receive json data
[ApiController]
// route connecter=> logic to find WeatherForecaseController
[Route("[controller]")]
// inherit ControllerBase class
public class UserController(IConfiguration config) : ControllerBase
{
    readonly DataContextDapper _dapper = new(config);

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        return _dapper.LoadData<User>("SELECT * FROM TutorialAppSchema.Users");
    }

    [HttpGet("GetSingleUser/{id}")]
    public User GetSingleUser(int id)
    {
        return _dapper.LoadDataSingle<User>(
            @" SELECT * FROM TutorialAppSchema.Users WHERE UserId =" + id.ToString()
        );
    }

    [HttpPost("CreateUser")]
    public IActionResult CreateUser(User user)
    {
        string sql =
            @"INSERT INTO TutorialAppSchema.Users (
                    
                     [FirstName],
                     [LastName],
                     [Email],
                     [Gender],
                     [Active]                           
                      ) VALUES (
                      '"
            + user.FirstName
            + @"',
                      '"
            + user.LastName
            + @"',
                    '"
            + user.Email
            + @"',
                    '"
            + user.Gender
            + @"',
                     '"
            + user.Active
            + @"')";

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }
        else
        {
            return BadRequest("");
        }
    }

    [HttpPut("UpdateUser")]
    public IActionResult UpdateUser(User user)
    {
        string sql =
            @"UPDATE  TutorialAppSchema.Users
                SET [FirstName]='"
            + user.FirstName
            + "',[LastName]='"
            + user.LastName
            + "',                    [Email]='"
            + user.Email
            + @"',
                    [Gender]='"
            + user.Gender
            + @"',
                    [Active]='"
            + user.Active
            + @"'
                    WHERE UserId='"
            + user.UserId
            + "'";

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }
        else
        {
            return BadRequest("");
        }
    }
}
