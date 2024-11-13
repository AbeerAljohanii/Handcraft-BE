using System.ComponentModel.DataAnnotations;

namespace Backend_Teamwork.src.Entities
{
    public class Category
    {
        public Guid Id { set; get; }

        [
            Required(ErrorMessage = "Name shouldn't be null"),
            MinLength(2, ErrorMessage = "Name should be at at least 2 characters"),
            MaxLength(20, ErrorMessage = "Name shouldn't be more than 20 characters")
        ]
        public string CategoryName { set; get; }

        public string? CategoryUrl { get; set; }
    }
}
