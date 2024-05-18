using BGMaterial.Application.Features.Mediator.Results;
using MediatR;

namespace BGMaterial.Application.Features.Mediator.Queries
{
    public class GetCheckAppUserQuery : IRequest<GetCheckAppUserQueryResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
