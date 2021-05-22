using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModels
{
    public class ProductModel
    {      
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public int CategoryId { get; set; }
    
        public string CategoryName { get; set; }

    }
}
