using AutoMapper;
using BGMaterial.Application.Dtos;
using BGMaterial.Application.Features.Mediator.Commands;
using BGMaterial.Application.Features.Mediator.Queries;
using BGMaterial.Application.Interfaces;
using MediatR;
using System.Net;
namespace BGMaterial.Application.Services
{
    public class MaterialService : IService, IMaterialService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public MaterialService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<CustomResponseDto<NoContentDto>> AddAsync(CreateMaterialDto dto)
        {
            var values = await GetAllAsync();
            var errors = new List<string>();


            if (values.Data.Any(x => x.Name.ToLower() == dto.Name.ToLower()))
            {
                errors.Add($"Eklemeye çalıştığınız '{dto.Name}' isimli materyal zaten mevcut.");
            }

            if (values.Data.Any(x => x.Code.ToLower() == dto.Code.ToLower()))
            {
                errors.Add($"Eklemeye çalıştığınız '{dto.Code}' kodlu materyal zaten mevcut.");
            }


            if (errors.Any())
            {
                return CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.BadRequest, errors);
            }

            var newEntity = _mapper.Map<CreateMaterialCommand>(dto);
            await _mediator.Send(newEntity);
            return CustomResponseDto<NoContentDto>.Success((int)HttpStatusCode.Accepted);
        }
        public async Task<CustomResponseDto<List<GetMaterialsDto>>> GetAllAsync()
        {
            var values = await _mediator.Send(new GetMaterialQuery());
            if (!values.Any())
            {
                return CustomResponseDto<List<GetMaterialsDto>>.Fail((int)HttpStatusCode.NotFound, "Materiyal Bulunamadı");
            }
            var newValues = _mapper.Map<List<GetMaterialsDto>>(values);
            return CustomResponseDto<List<GetMaterialsDto>>.Success((int)HttpStatusCode.Accepted, newValues);

        }

        public async Task<CustomResponseDto<List<GetMaterialByCodeDto>>> GetByCodeAsync(string code)
        {
            var values = await _mediator.Send(new GetMaterialByCodeQuery(code));
            if (!values.Any())
            {
                return CustomResponseDto<List<GetMaterialByCodeDto>>.Fail((int)HttpStatusCode.NotFound, $"'{code}' kodlu materyal bulunamadı");
            }
            var newValues = _mapper.Map<List<GetMaterialByCodeDto>>(values);
            return CustomResponseDto<List<GetMaterialByCodeDto>>.Success((int)HttpStatusCode.Accepted, newValues);
        }

        public async Task<CustomResponseDto<GetMaterialByIdDto>> GetByIdAsync(int id)
        {
            var values = await _mediator.Send(new GetMaterialByIdQuery(id));
            if (values == null)
            {
                return CustomResponseDto<GetMaterialByIdDto>.Fail((int)HttpStatusCode.NotFound, $"{id} numaralı materyal bulunamadı");
            }
            var newValues = _mapper.Map<GetMaterialByIdDto>(values);
            return CustomResponseDto<GetMaterialByIdDto>.Success((int)HttpStatusCode.Accepted, newValues);
        }

        public async Task<CustomResponseDto<GetMaterialsDto>> GetHighestListPricedAsync()
        {
            var values = await _mediator.Send(new GetMaterialQuery());
            var value = values.OrderByDescending(x => x.ListPrice).FirstOrDefault();
            if (values == null)
            {
                return CustomResponseDto<GetMaterialsDto>.Fail((int)HttpStatusCode.NotFound, "materyal bulunamadı");
            }
            var newValues = _mapper.Map<GetMaterialsDto>(value);
            return CustomResponseDto<GetMaterialsDto>.Success((int)HttpStatusCode.Accepted, newValues);
        }

        public async Task<CustomResponseDto<List<GetMaterialsDto>>> GetPagedAsync(int pageIndex, int pageSize)
        {
            var matList = await GetAllAsync();
            IQueryable<GetMaterialsDto> query = matList.Data.AsQueryable();
            int totalCount = query.Count();
            List<GetMaterialsDto> pagedList = query.Skip(pageIndex * pageSize).Take(pageSize).ToList();

            if (!pagedList.Any())
            {
                return CustomResponseDto<List<GetMaterialsDto>>.Fail((int)HttpStatusCode.NotFound, "Materiyal Bulunamadı");

            }
            return CustomResponseDto<List<GetMaterialsDto>>.Success((int)HttpStatusCode.Accepted, pagedList);

        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id)
        {
            var material = await GetByIdAsync(id);
            if (material.StatusCode == (int)HttpStatusCode.NotFound)
            {
                return CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.NotFound, $"{id} numaralı materyal bulunamadı");
            }
            await _mediator.Send(new RemoveMaterialCommand(id));
            return CustomResponseDto<NoContentDto>.Success((int)HttpStatusCode.Accepted);

        }
        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(UpdateMaterialDto dto)
        {
            var values = await GetAllAsync();
            var errors = new List<string>();


            if (values.Data.Any(x => x.Name.ToLower() == dto.Name.ToLower() && x.Id != dto.Id))
            {
                errors.Add($"Eklemeye çalıştığınız '{dto.Name}' isimli materyal zaten mevcut.");
            }

            if (values.Data.Any(x => x.Code.ToLower() == dto.Code.ToLower() && x.Id != dto.Id))
            {
                errors.Add($"Eklemeye çalıştığınız '{dto.Code}' kodlu materyal zaten mevcut.");
            }

            if (errors.Any())
            {
                return CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.BadRequest, errors);
            }
            var material = await GetByIdAsync(dto.Id);
            if (material.StatusCode == (int)HttpStatusCode.NotFound)
            {
                return CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.NotFound, $"{dto.Id} numaralı materyal bulunamadı");
            }
            var entity = _mapper.Map<UpdateMaterialCommand>(dto);
            await _mediator.Send(entity);
            return CustomResponseDto<NoContentDto>.Success(204);
        }
    }
}
