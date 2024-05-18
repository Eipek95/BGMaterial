using BGMaterial.Application.Features.Mediator.Commands;
using BGMaterial.Application.Interfaces;
using BGMaterial.Domain.Entities;
using MediatR;

namespace BGMaterial.Application.Features.Mediator.Handlers
{
    public class UpdateMaterialCommandHandler : IRequestHandler<UpdateMaterialCommand>
    {
        private readonly IRepository<Material> _repository;


        public UpdateMaterialCommandHandler(IRepository<Material> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateMaterialCommand request, CancellationToken cancellationToken)
        {
            var material = await _repository.GetByIdAsync(request.Id);
            material.Engine = request.Engine;
            material.ListPrice = request.ListPrice;
            material.StockErz = request.StockErz;
            material.StockLzm = request.StockLzm;
            material.StockMrk = request.StockMrk;
            material.StockAnk = request.StockAnk;
            material.StockAdn = request.StockAdn;
            material.Code = request.Code;
            material.Model = request.Model;
            material.Name = request.Name;
            material.Year = request.Year;
            await _repository.UpdateAsync(material);
        }
    }
}
