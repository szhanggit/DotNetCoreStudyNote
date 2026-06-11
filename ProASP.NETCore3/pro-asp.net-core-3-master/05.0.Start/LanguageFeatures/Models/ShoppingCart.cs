using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageFeatures.Models
{
    public class ShoppingCart : IEnumerable<Product>
    {
        public IEnumerable<Product> Products { get; set; }

        public IEnumerator<Product> GetEnumerator()
        {
            return Products.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
/*
 Suppose I need to be able to determine the total value of the Product objects in the ShoppingCart class, but I cannot modify the
class because it comes from a third party, and I do not have the source code. I can use an extension method to add the functionality I
need.
     */
