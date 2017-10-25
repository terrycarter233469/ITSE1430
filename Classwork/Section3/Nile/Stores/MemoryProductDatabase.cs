using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Stores
{
    /// <summary>Base class for product database.</summary>
    public class MemoryProductDatabase : ProductDatabase
    {
        /// <summary>Adds a product.</summary>
        /// <param name="product">The product to add.</param>
        /// <returns>The added product.</returns>
        protected override Product AddCore( Product product )
        {
            var newProduct = CopyProduct(product);
            _products.Add(newProduct);

            if (newProduct.ID <= 0)
                newProduct.ID = _nextId++;
            else if (newProduct.ID >= _nextId)
                _nextId = newProduct.ID + 1;

            return CopyProduct(newProduct);
        }

        /// <summary>Get a specific product.</summary>
        /// <returns>The product, if it exists.</returns>
        protected override Product GetCore(int id)
        {
            var product = FindProduct(id);
            return (product != null)? CopyProduct(product) : null;
        }

        /// <summary>Gets all products.</summary>
        /// <returns>The products.</returns>
        protected override IEnumerable<Product> GetAllCore()
        {
            foreach (var product in _products)
                yield return CopyProduct(product);
        //    var items = new Product[_products.Length];
        //    var index = 0;

        //    foreach (var product in _products)
        //    {
        //        //product = new Product();
        //        items[index++] = CopyProduct(product);
        //    };

        //    return items;
        }

        /// <summary>Removes the product.</summary>
        /// <param name="product">The product to remove.</param>
        protected override void RemoveCore( int id )
        {
            if (id <= 0)
                return;
            var product = FindProduct(id);
            if (product != null)
                _products.Remove(product);
        }

        /// <summary>Updates a product.</summary>
        /// <param name="product">The product to update.</param>
        /// <returns>The updated product.</returns>
        protected override Product UpdateCore(Product existing, Product product )
        {
            //replace
            existing = FindProduct(product.ID);
            _products.Remove(existing);

            var newProduct = CopyProduct(product);
             _products.Add(newProduct);

            return CopyProduct( newProduct);
        }

        private Product FindProduct(int id)
        {
            foreach (var product in _products)
            {
                if (product.ID == id)
                {
                    return product;
                }
            }

            return null;
        }

        private Product CopyProduct( Product product )
        {
            if (product == null)
                return null;

            var newProduct = new Product();
            newProduct.ID = product.ID;
            newProduct.Name = product.Name;
            newProduct.Description = product.Description;
            newProduct.Price = product.Price;
            newProduct.IsDiscontinued = product.IsDiscontinued;

            return newProduct;
        }

       // private Product[] _products = new Product[100];
        //private System.Collections.ArrayList _list = new System.Collections.ArrayList(); //dynamically allocated array, only supports generic objects
        private List<Product> _products = new List<Product>(); //converts the list into a generic type that will store products in it.
        private int _nextId = 1;
    }
}
