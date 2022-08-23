using System.ComponentModel.DataAnnotations;
namespace BookApp.Models
{
    public class CartItems
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public int Book_Id { get; set; }
        [Required]
        public string Book_Name { get; set; }
        [Required]
        public string Author { get; set; }
 
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int TotalAmt { get; set; }
        [Required]
        public string ImageUrl { get; set; }

        public int UserId { get; set; }

       
    }
}
