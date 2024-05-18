using MediatR;

namespace BGMaterial.Application.Features.Mediator.Commands
{
    public class RemoveMaterialCommand : IRequest
    {
        public RemoveMaterialCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
