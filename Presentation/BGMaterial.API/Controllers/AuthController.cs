using BGMaterial.Application.Features.Mediator.Queries;
using BGMaterial.Application.Tools;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BGMaterial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]


        public async Task<IActionResult> Login(GetCheckAppUserQuery query)
        {
            if (string.IsNullOrEmpty(query.Username) || string.IsNullOrEmpty(query.Password))
                return BadRequest("Kullanıcı Adı ve Parola Boş Olamaz");

            var values = await _mediator.Send(query);

            if (values.Id != 0) return Created("", JwtTokenGenerator.GenerateToken(values));

            return BadRequest("Kullanıcı adı yada şifre hatalıdır");
        }
    }
}
