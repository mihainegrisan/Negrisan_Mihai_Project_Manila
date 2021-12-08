using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Manila.DAL.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} length cannot exceed {1} characters")]
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} length cannot exceed {1} characters")]
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Range(0, 150, ErrorMessage = "The {0} should be between {1} and {2} years")]
        public int Age { get; set; }


        [Required]
        [StringLength(350, ErrorMessage = "{0} length cannot exceed {1} characters")]
        [Column(TypeName = "nvarchar(350)")]
        [EmailAddress]
        [DisplayName("Email")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "{0} length cannot exceed {1} characters")]
        [Column(TypeName = "nvarchar(15)")]
        [Phone]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Entry Date")]
        public DateTime EntryDate { get; set; }


        public int? AddressId { get; set; }
        public Address Address { get; set; }

        public ICollection<Order> Orders { get; set; }
        
    }
}
