using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace BackEndApiEF.entities
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Column("AccountId")]
        [Display(Name = "Account ID")]
        public int AccountId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = " First Name not more than 50 characters")]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = " First Name not more than 50 characters")]
        [Column("LastName")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
