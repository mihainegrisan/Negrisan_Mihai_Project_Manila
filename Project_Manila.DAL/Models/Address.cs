using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Manila.DAL.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "{0} length cannot exceed {1} characters")]
        [Column(TypeName = "nvarchar(200)")]
        [DisplayName("Street Address")]
        public string StreetAddress { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "{0} length cannot exceed {1} characters")]
        [Column(TypeName = "nvarchar(200)")]
        [DisplayName("Street Name")]
        public string StreetName { get; set; }
        
        [Required]
        [StringLength(90, ErrorMessage = "{0} length cannot exceed {1} characters")]
        [Column(TypeName = "nvarchar(90)")]
        public string City { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} length cannot exceed {1} characters")]
        [Column(TypeName = "nvarchar(50)")]
        public string State { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "{0} length cannot exceed {1} characters")]
        [Column(TypeName = "nvarchar(60)")]
        public string Country { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "{0} length cannot exceed {1} characters")]
        [Column(TypeName = "nvarchar(10)")]
        [DataType(DataType.PostalCode)]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }
    }
}