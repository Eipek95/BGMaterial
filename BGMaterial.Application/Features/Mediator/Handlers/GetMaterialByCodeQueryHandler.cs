using AutoMapper;
using BGMaterial.Application.Features.Mediator.Queries;
using BGMaterial.Application.Features.Mediator.Results;
using BGMaterial.Application.Interfaces;
using BGMaterial.Domain.Entities;
using MediatR;


namespace BGMaterial.Application.Features.Mediator.Handlers
{
    public class GetMaterialByCodeQueryHandler : IRequestHandler<GetMaterialByCodeQuery, List<GetMaterialByCodeQueryResult>>
    {
        private readonly IRepository<Material> _materialRepository;
        private IMapper _mapper;

        public GetMaterialByCodeQueryHandler(IRepository<Material> materialRepository, IMapper mapper)
        {
            _materialRepository = materialRepository;
            _mapper = mapper;
        }



        public async Task<List<GetMaterialByCodeQueryResult>> Handle(GetMaterialByCodeQuery request, CancellationToken cancellationToken)
        {
            var material = await _materialRepository.FindAsync(e => e.Code.ToLower().Contains(request.Code));
            return _mapper.Map<List<GetMaterialByCodeQueryResult>>(material);
        }
    }
}
