using BGMaterial.Application.Features.Mediator.Queries;
using BGMaterial.Application.Features.Mediator.Results;
using BGMaterial.Application.Interfaces;
using MediatR;

namespace BGMaterial.Application.Features.Mediator.Handlers
{
    public class GetCheckAppUserQueryHandler : IRequestHandler<GetCheckAppUserQuery, GetCheckAppUserQueryResult>
    {
        private readonly IAppUserRepository _appUserRepository;

        public GetCheckAppUserQueryHandler(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<GetCheckAppUserQueryResult> Handle(GetCheckAppUserQuery request, CancellationToken cancellationToken)
        {
            var value = new GetCheckAppUserQueryResult();
            var user = await _appUserRepository.GetByFilterAsync(x => x.Username == request.Username && x.Password == request.Password);
            if (user == null)
            {

            }
            else
            {
                value.Id = user.Id;
                value.Username = user.Username;
            }

            return value;
        }
    }
}
