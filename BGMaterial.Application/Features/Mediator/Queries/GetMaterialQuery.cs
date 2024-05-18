using BGMaterial.Application.Features.Mediator.Results;
using MediatR;

namespace BGMaterial.Application.Features.Mediator.Queries
{
    public class GetMaterialQuery : IRequest<List<GetMaterialQueryResult>>
    {
    }
}
