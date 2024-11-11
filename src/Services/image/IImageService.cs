using Backend_Teamwork.src.Utils;
using static Backend_Teamwork.src.DTO.ImageDTO;

namespace Backend_Teamwork.src.Services.image
{
    public interface IImageService
    {
        Task<ImageReadDto> CreateOneAsync(Guid userId, ImageCreateDto image);
        Task<List<ImageReadDto>> GetAllAsync(PaginationOptions paginationOptions);
        Task<int> GetTotalImagesCountAsync();
        Task<ImageReadDto> GetByIdAsync(Guid id);
        Task<bool> DeleteOneAsync(Guid id);
        Task<ImageReadDto> UpdateOneAsync(Guid id, ImageUpdateDto updateImage);
    }
}
