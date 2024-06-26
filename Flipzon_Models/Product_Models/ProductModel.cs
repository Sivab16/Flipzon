using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flipzon_Models.Product_Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsAvailable { get; set; }

        public double Price { get; set; }

        public List<ProductFeature> Feature { get; set; }
    }
}
