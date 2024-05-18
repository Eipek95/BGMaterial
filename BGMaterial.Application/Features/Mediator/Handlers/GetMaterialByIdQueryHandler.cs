using AutoMapper;
using BGMaterial.Application.Features.Mediator.Queries;
using BGMaterial.Application.Features.Mediator.Results;
using BGMaterial.Application.Interfaces;
using BGMaterial.Domain.Entities;
using MediatR;


namespace BGMaterial.Application.Features.Mediator.Handlers
{
    public class GetMaterialByIdQueryHandler : IRequestHandler<GetMaterialByIdQuery, GetMaterialByIdQueryResult>
    {
        private readonly IRepository<Material> _materialRepository;
        private IMapper _mapper;

        public GetMaterialByIdQueryHandler(IRepository<Material> materialRepository, IMapper mapper)
        {
            _materialRepository = materialRepository;
            _mapper = mapper;
        }

        public async Task<GetMaterialByIdQueryResult> Handle(GetMaterialByIdQuery request, CancellationToken cancellationToken)
        {
            var material = await _materialRepository.GetByIdAsync(request.Id);
            return _mapper.Map<GetMaterialByIdQueryResult>(material);
        }
    }
}
