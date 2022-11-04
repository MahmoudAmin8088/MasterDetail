using MasterDetails.Core.IRepository;
using MasterDetails.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterDetails.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
                _authRepository = authRepository;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(200, Type = typeof(AuthModel))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult>Register([FromBody] RegisterModel model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authRepository.RegisterAsync(model);
            if (!result.IsAuthentication)
                return BadRequest(result.Message);

            return Ok(result);

        }

        [HttpPost("[action]")]
        [ProducesResponseType(200, Type = typeof(AuthModel))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authRepository.LoginAsync(model);

            if (!result.IsAuthentication)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
