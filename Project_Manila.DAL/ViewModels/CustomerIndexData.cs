using Project_Manila.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_Manila.Common.Utility;

namespace Project_Manila.DAL.ViewModels
{
    public class CustomerIndexData
    {
        public Customer Customer { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
