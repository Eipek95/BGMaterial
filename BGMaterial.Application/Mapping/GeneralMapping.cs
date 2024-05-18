using AutoMapper;
using BGMaterial.Application.Dtos;
using BGMaterial.Application.Features.Mediator.Commands;
using BGMaterial.Application.Features.Mediator.Results;
using BGMaterial.Domain.Entities;

namespace BGMaterial.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<CreateMaterialCommand, Material>().ReverseMap();
            CreateMap<UpdateMaterialCommand, Material>().ReverseMap();
            CreateMap<GetMaterialByIdQueryResult, Material>().ReverseMap();
            CreateMap<GetMaterialByCodeQueryResult, Material>().ReverseMap();
            CreateMap<GetMaterialQueryResult, Material>().ReverseMap();


            CreateMap<GetMaterialQueryResult, GetMaterialsDto>().ReverseMap();
            CreateMap<GetMaterialByIdQueryResult, GetMaterialByIdDto>().ReverseMap();
            CreateMap<GetMaterialByCodeQueryResult, GetMaterialByCodeDto>().ReverseMap();
            CreateMap<CreateMaterialCommand, CreateMaterialDto>().ReverseMap();
            CreateMap<UpdateMaterialCommand, UpdateMaterialDto>().ReverseMap();
        }
    }
}
