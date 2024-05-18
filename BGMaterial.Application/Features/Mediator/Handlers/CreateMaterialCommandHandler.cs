using AutoMapper;
using BGMaterial.Application.Features.Mediator.Commands;
using BGMaterial.Application.Interfaces;
using BGMaterial.Domain.Entities;
using MediatR;


namespace BGMaterial.Application.Features.Mediator.Handlers
{
    public class CreateMaterialCommandHandler : IRequestHandler<CreateMaterialCommand>
    {
        private readonly IRepository<Material> _repository;
        private IMapper _mapper;

        public CreateMaterialCommandHandler(IRepository<Material> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(_mapper.Map<Material>(request));
        }
    }
}
