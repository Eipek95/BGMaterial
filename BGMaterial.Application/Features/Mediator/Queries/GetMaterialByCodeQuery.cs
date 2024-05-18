using BGMaterial.Application.Features.Mediator.Results;
using MediatR;

namespace BGMaterial.Application.Features.Mediator.Queries
{
    public class GetMaterialByCodeQuery : IRequest<List<GetMaterialByCodeQueryResult>>
    {
        public string Code { get; set; }

        public GetMaterialByCodeQuery(string code)
        {
            Code = code.ToLower();
        }
    }
}
