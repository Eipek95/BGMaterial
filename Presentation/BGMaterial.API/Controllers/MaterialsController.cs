using BGMaterial.API.Filters;
using BGMaterial.Application.Dtos;
using BGMaterial.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BGMaterial.API.Controllers
{

    //[Authorize]
    public class MaterialsController : CustomBaseController
    {
        private readonly IMaterialService _materialService;

        public MaterialsController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var responseData = await _materialService.GetAllAsync();
            return CreateActionResult(responseData);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var responseData = await _materialService.GetByIdAsync(id);
            return CreateActionResult(responseData);
        }
        [HttpGet("GetHighestListPriced")]
        public async Task<IActionResult> GetHighestListPriced()
        {
            var responseData = await _materialService.GetHighestListPricedAsync();
            return CreateActionResult(responseData);
        }
        [HttpGet("GetByCode/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var responseData = await _materialService.GetByCodeAsync(code);
            return CreateActionResult(responseData);

        }
        [HttpPost]
        [ServiceFilter(typeof(ValidateFilterAttribute<CreateMaterialDto>))]
        public async Task<IActionResult> Save(CreateMaterialDto request)
        {
            var response = await _materialService.AddAsync(request);
            return CreateActionResult(response);
        }
        [HttpPut]
        [ServiceFilter(typeof(ValidateFilterAttribute<UpdateMaterialDto>))]

        public async Task<IActionResult> Update(UpdateMaterialDto request)
        {
            var response = await _materialService.UpdateAsync(request);
            return CreateActionResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _materialService.RemoveAsync(id);
            return CreateActionResult(response);
        }
    }
}
