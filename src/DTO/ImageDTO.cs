using System.ComponentModel.DataAnnotations;
using Backend_Teamwork.src.Utils;

namespace Backend_Teamwork.src.DTO
{
    public class ImageDTO
    {
        public class ImageCreateDto
        {
            [Required(ErrorMessage = "File is required")]
            [FileExtensions(
                Extensions = "jpg,jpeg,png",
                ErrorMessage = "Invalid file type. Only jpg, jpeg, png files are allowed."
            )]
            public IFormFile File { get; set; }

            [Required(ErrorMessage = "Alt text shouldn't be null")]
            [MinLength(3, ErrorMessage = "Alt text should be at least 3 characters")]
            [MaxLength(50, ErrorMessage = "Alt text shouldn't be more than 50 characters")]
            public string Alt { get; set; }
        }

        public class ImageReadDto
        {
            public Guid Id { get; set; }
            public IFormFile File { get; set; }
            public string Alt { get; set; }
        }

        // List of Artworks
        public class ImagesListDto
        {
            public List<ImageReadDto> Images { get; set; }
            public int TotalCount { get; set; }
        }

        // DTO for updating an existing Image
        [AtLeastOneRequired(ErrorMessage = "At least one property must be updated.")]
        public class ImageUpdateDto
        {
            [FileExtensions(
                Extensions = "jpg,jpeg,png",
                ErrorMessage = "Invalid file type. Only jpg, jpeg, png files are allowed."
            )]
            public IFormFile? File { get; set; }

            [MinLength(3, ErrorMessage = "Alt text should be at least 3 characters")]
            [MaxLength(50, ErrorMessage = "Alt text shouldn't be more than 50 characters")]
            public string? Alt { get; set; }
        }
    }
}
