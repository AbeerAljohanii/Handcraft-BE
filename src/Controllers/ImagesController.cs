using System.Security.Claims;
using Backend_Teamwork.src.Services.image;
using Backend_Teamwork.src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Backend_Teamwork.src.DTO.ImageDTO;

namespace Backend_Teamwork.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;

        // Constructor
        public ImagesController(IImageService service)
        {
            _imageService = service;
        }

        // Create
        // End-Point: api/v1/images
        [HttpPost]
        [Authorize(Roles = "Admin,Artist")]
        public async Task<ActionResult<ImageReadDto>> CreateOne([FromBody] ImageCreateDto createDto)
        {
            // extract user information
            var authenticateClaims = HttpContext.User;
            // get user id from claim
            var userId = authenticateClaims
                .FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!
                .Value;
            // string => guid
            var userGuid = new Guid(userId);

            var createdImage = await _imageService.CreateOneAsync(userGuid, createDto);
            return Ok(createdImage);
        }

        // Get all
        // End-Point: api/v1/images
        [HttpGet]
        public async Task<ActionResult<List<ImagesListDto>>> GetAll(
            [FromQuery] PaginationOptions paginationOptions
        )
        {
            var imageList = await _imageService.GetAllAsync(paginationOptions);
            var totalCount = await _imageService.GetTotalImagesCountAsync();
            var response = new ImagesListDto { Images = imageList, TotalCount = totalCount };
            return Ok(response);
        }

        // Get by image id
        // End-Point: api/v1/images/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Artist")]
        public async Task<ActionResult<ImageReadDto>> GetById([FromRoute] Guid id)
        {
            var image = await _imageService.GetByIdAsync(id);
            return Ok(image);
        }

        // Update
        // End-Point: api/v1/images/{id}
        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin,Artist")]
        public async Task<ActionResult> UpdateOne(
            [FromRoute] Guid id,
            [FromBody] ImageUpdateDto updateDTO
        )
        {
            var image = await _imageService.UpdateOneAsync(id, updateDTO);
            return Ok(image);
        }

        // Delete
        // End-Point: api/v1/images/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Artist")]
        public async Task<ActionResult> DeleteOne([FromRoute] Guid id)
        {
            await _imageService.DeleteOneAsync(id);
            return NoContent();
        }
    }
}
