using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationModel.Models
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string ProductName { get; set; }
        public double Price { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
