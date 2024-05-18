using BGMaterial.Application.Features.Mediator.Commands;
using BGMaterial.Application.Interfaces;
using BGMaterial.Domain.Entities;
using MediatR;

namespace BGMaterial.Application.Features.Mediator.Handlers
{
    public class RemoveMaterialCommandHandler : IRequestHandler<RemoveMaterialCommand>
    {
        private readonly IRepository<Material> _repository;

        public RemoveMaterialCommandHandler(IRepository<Material> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveMaterialCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.Id);
            await _repository.RemoveAsync(value);
        }
    }
}
