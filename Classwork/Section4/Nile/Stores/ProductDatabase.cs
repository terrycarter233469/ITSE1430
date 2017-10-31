/*
 * ITSE 1430
 */
using System;
using System.Collections.Generic;

namespace Nile.Stores
{
    /// <summary>Provides a base implementation of <see cref="IProductDatabase"/>.</summary>
    public abstract class ProductDatabase : IProductDatabase
    {        
        /// <summary>Adds a product.</summary>
        /// <param name="product">The product to add.</param>
        /// <returns>The added product.</returns>
        /// <exception cref="ArgumentNullException">Product is null</exception>
        /// <exception cref="ValidationException">Product is invalid</exception>
        public Product Add ( Product product )
        {
            //Validate
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Product was null");
            //return null;

            //if (!ObjectValidator.TryValidate(product, out var errors))
            //    throw new System.ComponentModel.DataAnnotations.ValidationException("Product was not valid", nameof(product));
            //    // return null;

            ObjectValidator.Validate(product);

            try
            {
                return AddCore(product);
            } catch (Exception e)
            {
                //throw different exception
                throw new Exception("Add Failed", e);

                //re-throw the exception
                throw;

                //Silently ignore - almost always bad
            }

           // return AddCore(product);
        }

        /// <summary>Get a specific product.</summary>
        /// <returns>The product, if it exists.</returns>
        public Product Get ( int id )
        {
            //Validate
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be > 0.");
               // return null;

            return GetCore(id);
        }
        
        /// <summary>Gets all products.</summary>
        /// <returns>The products.</returns>
        public IEnumerable<Product> GetAll ()
        {
            return GetAllCore();
        }
        
        /// <summary>Removes the product.</summary>
        /// <param name="id">The product to remove.</param>
        public void Remove ( int id )
        {
            // Validate
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be > 0.");

            RemoveCore(id);
        }
        
        /// <summary>Updates a product.</summary>
        /// <param name="product">The product to update.</param>
        /// <returns>The updated product.</returns>
        public Product Update ( Product product )
        {
            // Validate
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Product was null");

            //if (!ObjectValidator.TryValidate(product, out var errors))
            //    throw new ArgumentException("Product is invalid.", nameof(product));
            ObjectValidator.Validate(product);


            //Get existing product
            var existing = GetCore(product.Id) ?? throw new Exception("Product not found");
            //if (existing == null)
            //    throw new Exception("Product not found");

            return UpdateCore(existing, product);
        }

        #region Protected Members

        /// <summary>Adds a product.</summary>
        /// <param name="product">The product to add.</param>
        /// <returns>The added product.</returns>
        protected abstract Product AddCore( Product product );

        /// <summary>Get a product given its ID.</summary>
        /// <param name="id">The ID.</param>
        /// <returns>The product, if any.</returns>
        protected abstract Product GetCore( int id );

        protected abstract IEnumerable<Product> GetAllCore();

        /// <summary>Removes a product given its ID.</summary>
        /// <param name="id">The ID.</param>
        protected abstract void RemoveCore( int id );

        /// <summary>Updates a product.</summary>
        /// <param name="existing">The existing product.</param>
        /// <param name="newItem">The product to update.</param>
        /// <returns>The updated product.</returns>
        protected abstract Product UpdateCore( Product existing, Product newItem );
        
        #endregion
    }
}
