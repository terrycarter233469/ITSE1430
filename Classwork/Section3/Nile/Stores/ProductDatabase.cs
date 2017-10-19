using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Stores
{
    /// <summary>Base class for product database.</summary>
    public abstract  class ProductDatabase : IProductDatabase
    {
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

           return  AddCore(product);

            ////emulate database by storing copy
            //var newProduct = CopyProduct(product);
            //AddCore(newProduct);

            //return CopyProduct(newProduct);
            //// var item = _list[0];
            //// return product;
        }

        /// <summary>Get a specific product.</summary>
        /// <returns>The product, if it exists.</returns>
        public Product Get(int id)
        {
            //validate
            if (id <= 0)
                return null;

            return GetCore(id);
        }

        protected abstract Product GetCore( int id );

        /// <summary>Gets all products.</summary>
        /// <returns>The products.</returns>
        public IEnumerable<Product> GetAll()
        {
            return GetAllCore();
        }

        protected abstract IEnumerable<Product> GetAllCore();

        /// <summary>Removes the product.</summary>
        /// <param name="product">The product to remove.</param>
        public void Remove( int id )
        {
            if (id <= 0)
                return;
            RemoveCore(id);
        }

        protected abstract void RemoveCore( int id );

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
            var existing = GetCore(product.ID);
            if (existing == null)
                return null;

            return UpdateCore(existing, product);
        }

        protected abstract Product UpdateCore( Product existing, Product newItem );
        protected abstract Product AddCore( Product product );
    }
}
