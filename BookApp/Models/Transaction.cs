using System.ComponentModel.DataAnnotations;

namespace BookApp.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Country/Region")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Required")]
        public string First_Name { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Last_Name { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Apartment,suite")]
        public string Apartment { get; set; }
        [Required(ErrorMessage = "Required")]

        public string city { get; set; }
        [Required(ErrorMessage = "Required")]
        public string state { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.PostalCode)]
        public string Pin_Code { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.PhoneNumber)]

        public string Phone_Number { get; set; }
     
        public int User_Id { get; set; }
    }
}
