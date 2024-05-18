using AutoMapper;
using BGMaterial.Application.Features.Mediator.Queries;
using BGMaterial.Application.Features.Mediator.Results;
using BGMaterial.Application.Interfaces;
using BGMaterial.Domain.Entities;
using MediatR;

namespace BGMaterial.Application.Features.Mediator.Handlers
{
    public class GetMaterialQueryHandler : IRequestHandler<GetMaterialQuery, List<GetMaterialQueryResult>>
    {
        private readonly IRepository<Material> _repository;
        private readonly IMapper _mapper;

        public GetMaterialQueryHandler(IRepository<Material> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetMaterialQueryResult>> Handle(GetMaterialQuery request, CancellationToken cancellationToken)
        {
            var materials = await _repository.GetAllAsync();
            return _mapper.Map<List<GetMaterialQueryResult>>(materials);
        }
    }
}
