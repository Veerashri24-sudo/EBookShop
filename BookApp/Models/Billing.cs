using System.ComponentModel.DataAnnotations;

namespace BookApp.Models
{
    public class Billing
    {
        [Key]
        [Required]
        public int TransactionId { get; set; }
        public int Total_Payment { get; set; }
        [Required]
        public string Payment_Type { get; set; }
       
        public string Bank_Name { get; set; }
       
        public string Branch { get; set; }
       
        public string CreditCard_Number { get; set; }
        [DataType(DataType.Date)]
        public DateTime CardExpiryDate { get; set; }
            
        
        public string cvv { get; set; }
       
        public int User_Id { get; set; }
      
        //public string Details { get; set; }
        public int Book_Id { get; set; }

        public string Book_Name { get; set; }
      
        public int Quantity { get; set; }
        public string DateTime { get; set; }
    }
}
