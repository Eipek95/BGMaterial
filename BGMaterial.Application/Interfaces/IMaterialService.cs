using BGMaterial.Application.Dtos;

namespace BGMaterial.Application.Interfaces
{
    public interface IMaterialService : IService
    {
        Task<CustomResponseDto<GetMaterialByIdDto>> GetByIdAsync(int id);
        Task<CustomResponseDto<List<GetMaterialsDto>>> GetAllAsync();
        Task<CustomResponseDto<GetMaterialsDto>> GetHighestListPricedAsync();
        Task<CustomResponseDto<List<GetMaterialByCodeDto>>> GetByCodeAsync(string code);
        Task<CustomResponseDto<NoContentDto>> AddAsync(CreateMaterialDto dto);
        Task<CustomResponseDto<NoContentDto>> UpdateAsync(UpdateMaterialDto dto);
        Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id);

    }
}
