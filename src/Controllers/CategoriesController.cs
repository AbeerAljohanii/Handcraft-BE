using Backend_Teamwork.src.Services.category;
using Backend_Teamwork.src.Utils;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Backend_Teamwork.src.DTO.CategoryDTO;

namespace Backend_Teamwork.src.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly Cloudinary _cloudinary;

        public CategoriesController(ICategoryService categoryService, Cloudinary cloudinary)
        {
            _categoryService = categoryService;
            _cloudinary = cloudinary;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryReadDto>>> GetCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryReadDto>> GetCategoryById([FromRoute] Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return Ok(category);
        }

        [HttpGet("search/{name}")]
        public async Task<ActionResult<CategoryReadDto>> GetCategoryByName([FromRoute] string name)
        {
            var category = await _categoryService.GetByNameAsync(name);
            return Ok(category);
        }

        [HttpGet("page")]
        public async Task<ActionResult<List<CategoryReadDto>>> GetCategoriesWithPaginationAsync(
            [FromQuery] PaginationOptions paginationOptions
        )
        {
            var categories = await _categoryService.GetWithPaginationAsync(paginationOptions);
            return Ok(categories);
        }

        [HttpGet("sort")]
        public async Task<ActionResult<List<CategoryReadDto>>> SortCategoriesByName()
        {
            var categories = await _categoryService.SortByNameAsync();
            return Ok(categories);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryReadDto>> CreateCategory(
            [FromForm] CategoryCreateDto categoryDTO,
            [FromForm] IFormFile CategoryUrl
        )
        {
            // Handle image upload
            string imageUrl = null;
            if (CategoryUrl != null)
            {
                imageUrl = await UploadImageAsync(CategoryUrl, "categories");
            }
            else
            {
                Console.WriteLine("No image received");
            }
            var category = await _categoryService.CreateAsync(categoryDTO, imageUrl);
            return CreatedAtAction(nameof(CreateCategory), new { id = category.Id }, category);
        }

        private async Task<string> UploadImageAsync(IFormFile image, string folder)
        {
            var uploadResult = await _cloudinary.UploadAsync(
                new ImageUploadParams
                {
                    File = new FileDescription(image.FileName, image.OpenReadStream()),
                    Folder = folder,
                }
            );
            return uploadResult?.SecureUrl?.ToString();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryReadDto>> UpdateCategory(
            [FromRoute] Guid id,
            [FromBody] CategoryUpdateDto categoryDTO
        )
        {
            var category = await _categoryService.UpdateAsync(id, categoryDTO);
            return Ok(category);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteCategory([FromRoute] Guid id)
        {
            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
