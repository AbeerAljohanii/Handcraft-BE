using System.ComponentModel.DataAnnotations;
using Backend_Teamwork.src.Utils;

namespace Backend_Teamwork.src.DTO
{
    public class CategoryDTO
    {
        public class CategoryCreateDto
        {
            [
                Required(ErrorMessage = "Name shouldn't be null"),
                MinLength(2, ErrorMessage = "Name should be at at least 2 characters"),
                MaxLength(20, ErrorMessage = "Name shouldn't be more than 20 characters")
            ]
            public string CategoryName { get; set; }
            public string? CategoryUrl { get; set; }
        }

        [AtLeastOneRequired(ErrorMessage = "At least one property must be updated.")]
        public class CategoryUpdateDto
        {
            [
                Required(ErrorMessage = "Name shouldn't be null"),
                MinLength(2, ErrorMessage = "Name should be at at least 2 characters"),
                MaxLength(20, ErrorMessage = "Name shouldn't be more than 20 characters")
            ]
            public string? CategoryName { get; set; }
            public string? CategoryUrl { get; set; }
        }

        public class CategoryReadDto
        {
            public Guid Id { get; set; }
            public string CategoryName { get; set; }
            public string CategoryUrl { get; set; }
        }
    }
}
