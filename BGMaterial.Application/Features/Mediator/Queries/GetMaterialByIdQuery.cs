using BGMaterial.Application.Features.Mediator.Results;
using MediatR;

namespace BGMaterial.Application.Features.Mediator.Queries
{
    public class GetMaterialByIdQuery : IRequest<GetMaterialByIdQueryResult>
    {
        public int Id { get; set; }

        public GetMaterialByIdQuery(int id)
        {
            Id = id;
        }
    }
}
