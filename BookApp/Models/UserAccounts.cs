using System.ComponentModel.DataAnnotations;

namespace BookApp.Models
{
    public class UserAccounts
    {
        [Key]
        [Required]
        [Display(Name = "User Id")]
        
        public string UserId { get; set; }
        
        [Display(Name = "First Name")]
        public string First_Name { get; set; }
        [Display(Name = "Last Name")]
        public string Last_Name { get; set; }


        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }
        [Required]
     
    
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
