using System.ComponentModel.DataAnnotations;

namespace BookApp.Models
{
    public class Book
    {

        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Book_Name { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string ImageUrl { get; set; }

        
    }
}
