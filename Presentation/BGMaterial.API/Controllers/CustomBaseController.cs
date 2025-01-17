﻿using BGMaterial.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BGMaterial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if (response.StatusCode == 204)
            {
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode,
                };

            }

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode,
            };
        }
    }
}
