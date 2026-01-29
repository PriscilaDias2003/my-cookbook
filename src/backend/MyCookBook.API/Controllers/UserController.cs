using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCookBook.Communication.Requests;
using MyCookBook.Communication.Responses;

namespace MyCookBook.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
        public IActionResult Register(RequestRegisterUserJson request) {
            return Created();
        }
    }
}
