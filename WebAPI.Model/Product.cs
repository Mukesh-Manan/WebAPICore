using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Model
{
    public class Product
    {
        public long ProductID { get; set; }
        public string Name { get; set; }
        public string  Category { get; set; }
        public string Color { get; set; }
        public string UnitPrice { get; set; }
        public int AvailableQuatity { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
