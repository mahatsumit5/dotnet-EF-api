using Microsoft.AspNetCore.Mvc;

namespace DotnetApi.Controllers;

// helps to send and receive json data
[ApiController]
// route connecter=> logic to find WeatherForecaseController
[Route("[controller]")]
// inherit ControllerBase class
public class UserController : ControllerBase
{
    public UserController() { }

    [HttpGet("GetUsers/{test}")]
    public string[] GetUsers(string test)
    {
        string[] response = ["test1", "test2",test];
        return response;
    }
}
