    using Production.Core.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Production.Models
    {
            internal class Product
            {
                public ProductType ProductType { get; set; }
                public ProductType? TargetProduct { get; private set; }
                public Product(ProductType productType, ProductType? targetProduct) {
                    ProductType = productType;
                    TargetProduct = targetProduct;
                }
            }
    }