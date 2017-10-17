using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile
{
    /// <summary>Base class for product database.</summary>
    public class ProductDatabase
    {
        public ProductDatabase()
        {

            //_products.Add(new Product(){ID = 1, Name = "Galaxy S7", Price = 650});
            //_products.Add(new Product(){ID = 2, Name = "Samsung Note 7", Price = 150, IsDiscontinued = true});
            //_products.Add(new Product(){ID = 3, Name = "Windows Phone",Price = 100});
            //_products.Add(new Product(){ID = 4, Name = "iPhone X", Price = 1900, IsDiscontinued = true});

            //Collection initializer syntax with array
            _products.AddRange(new [] { //passes in the elements of the array into a list
                new Product() { ID = 1, Name = "Galaxy S7", Price = 650 },
                new Product() { ID = 2, Name = "Samsung Note 7", Price = 150, IsDiscontinued = true },
                new Product() { ID = 3, Name = "Windows Phone", Price = 100 },
                new Product() { ID = 4, Name = "iPhone X", Price = 1900, IsDiscontinued = true },
            });

            ////Collection initializer Syntax, automatically creates a list and adds all the objects to it
            //_products = new List<Product>() {
            //    new Product() { ID = 1, Name = "Galaxy S7", Price = 650 },
            //    new Product() { ID = 2, Name = "Samsung Note 7", Price = 150, IsDiscontinued = true },
            //    new Product() { ID = 3, Name = "Windows Phone", Price = 100 },
            //    new Product() { ID = 4, Name = "iPhone X", Price = 1900, IsDiscontinued = true },
            //};

            _nextId = _products.Count + 1;
        }

        /// <summary>Adds a product.</summary>
        /// <param name="product">The product to add.</param>
        /// <returns>The added product.</returns>
        public Product Add( Product product )
        {
            //validate
            if (product == null)
                return null;

            //using invalidatable object
            if (!ObjectValidator.TryValidate(product, out var errors))
                return null;

            //if (!String.IsNullOrEmpty(product.Validate()))
            //    return null;

            //emulate database by storing copy
            var newProduct = CopyProduct(product);
            _products.Add(newProduct);
            newProduct.ID = _nextId++;

            return CopyProduct(newProduct);
            // var item = _list[0];
            // return product;
        }

        /// <summary>Get a specific product.</summary>
        /// <returns>The product, if it exists.</returns>
        public Product Get(int id)
        {
            //validate
            if(id <= 0)
                return null;
            var product = FindProduct(id);
            return (product != null)? CopyProduct(product) : null;
        }

        /// <summary>Gets all products.</summary>
        /// <returns>The products.</returns>
        public Product[] GetAll()
        {
            var items = new Product[_products.Count];
            var index = 0;
            foreach (var product in _products)
                items[index++] = (CopyProduct(product));

            return items;
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
        public void Remove( int id )
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
        public Product Update( Product product )
        { 
            //validate
            if (product == null)
                return null;
            //Using IValidatableObject
            if (!ObjectValidator.TryValidate(product, out var errors))
                return null;

            //if (!String.IsNullOrEmpty(product.Validate()))
            //    return null;


            //get existing product
            var existing = FindProduct(product.ID);
            if (existing == null)
                return null;
            //replace
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
