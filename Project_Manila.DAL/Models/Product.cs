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
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "{0} length cannot exceed {1} characters")]
        [Column(TypeName = "nvarchar(500)")]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [Required]
        [StringLength(3000, ErrorMessage = "{0} length cannot exceed {1} characters")]
        [Column(TypeName = "nvarchar(3000)")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [DataType(DataType.Currency)]
        public decimal CurrentPrice { get; set; }
    }
}
