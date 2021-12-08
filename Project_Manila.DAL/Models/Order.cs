using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Manila.DAL.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Order Date")]
        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }

        [Required]
        public Customer Customer { get; set; }

        [Required]

        public int ShippingAddressId { get; set; }

        [Required]
        public Address ShippingAddress { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
