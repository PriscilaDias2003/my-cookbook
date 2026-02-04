using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCookBook.Application.UseCases.User.Register;
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
        public async Task<IActionResult> Register([FromBody]RequestRegisterUserJson request, [FromServices]IRegisterUserUseCase useCase) {

            var result = await useCase.Execute(request);
            return Created(string.Empty, result);
        }
    }
}
