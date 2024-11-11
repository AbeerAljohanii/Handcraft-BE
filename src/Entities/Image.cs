using System.ComponentModel.DataAnnotations;

namespace Backend_Teamwork.src.Entities
{
    public class Image
    {
        public Guid Id { get; set; }

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

        // image

        [Required(ErrorMessage = "Artwork Id shouldn't be null")]
        public Guid? ArtworkId { get; set; } // Nullable in case it's only for Category
        public Artwork Artwork { get; set; } = null!;

        [Required(ErrorMessage = "Category Id shouldn't be null")]
        public Guid? CategoryId { get; set; } // Nullable in case it's only for Artwork
        public Category Category { get; set; } = null!;
    }
}
