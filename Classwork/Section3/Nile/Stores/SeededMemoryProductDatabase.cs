using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Stores
{
    /// <summary>Base class for product database.</summary>
    public class SeededMemoryProductDatabase : MemoryProductDatabase
    {
        public SeededMemoryProductDatabase()
        {
            #region
            ////Collection initializer Syntax, automatically creates a list and adds all the objects to it
            //_products = new List<Product>() {
            //    new Product() { ID = 1, Name = "Galaxy S7", Price = 650 },
            //    new Product() { ID = 2, Name = "Samsung Note 7", Price = 150, IsDiscontinued = true },
            //    new Product() { ID = 3, Name = "Windows Phone", Price = 100 },
            //    new Product() { ID = 4, Name = "iPhone X", Price = 1900, IsDiscontinued = true },
            //};

            //_products.Add(new Product(){ID = 1, Name = "Galaxy S7", Price = 650});
            //_products.Add(new Product(){ID = 2, Name = "Samsung Note 7", Price = 150, IsDiscontinued = true});
            //_products.Add(new Product(){ID = 3, Name = "Windows Phone",Price = 100});
            //_products.Add(new Product(){ID = 4, Name = "iPhone X", Price = 1900, IsDiscontinued = true});
            #endregion


            //Collection initializer syntax with array
            // _products.AddRange(new [] { //passes in the elements of the array into a list
            AddCore(new Product() { ID = 1, Name = "Galaxy S7", Price = 650 });
            AddCore(new Product() { ID = 2, Name = "Samsung Note 7", Price = 150, IsDiscontinued = true });
            AddCore(new Product() { ID = 3, Name = "Windows Phone", Price = 100 });
            AddCore(new Product() { ID = 4, Name = "iPhone X", Price = 1900, IsDiscontinued = true });
       // });

            //_nextId = _products.Count + 1;
        }

 
    }
}
