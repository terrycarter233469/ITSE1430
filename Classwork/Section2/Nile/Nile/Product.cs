using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile
{

    /// <summary>Represents a product.</summary>
    /// <remarks>This will represent a product with other stuff.</remarks>
    public class Product
    {
        public Product()
        {
            //Cross field initialization
        }

       // public readonly Product None = new Product();   //reference type, creates an empty object when the instance is created. the field is only readonly, 
                                                        //they can still call all of it's methods and properties. 
        private string _name;
        private string _description;

        private readonly double _someValueICannotChange = 10; //readonly works only on fields and effectively makes it a constant with some differences. 
                                                              //readonly means the value is fixed at the point the instance is created.                                                             

        /// <summary>Gets or sets the name.</summary>
        /// <value>Never returns null.</value>
        public string Name  //property. A mixture of a field and a method
        {
            get //must always return a value
            {
                return _name ?? ""; //prevents a null return
            }

            set //value is a keyword in a set
            {
                _name = value?.Trim();
            }
        }

        /// <summary>Gets or sets the description.</summary>
        public string Description
        {
            get{return _description ?? "";}
            set{_description = value?.Trim();}
        }

        /// <summary>Gets or sets the price.</summary>
        public decimal Price { get; set; } = 0; //shorthand auto property. will auto generate _price. = 0 will initialize the _price field.

        /// <summary>Determines if discontinued.</summary>
        public bool IsDiscontinued { get; set; }
        //{
        //    get { return _isDiscontinued; }
        //    set { _isDiscontinued = value; }
        //}

        public const decimal DiscontinuedDiscoutRate = 0.10M;  //only time it makes sense to have a public field, constant
        /// <summary>Gets the discounted price, if applicable/// </summary>
        public decimal DiscountedPrice
        {
            get 
            {
                if (IsDiscontinued)
                    return Price * DiscontinuedDiscoutRate;
                return Price;
            }
        }

        public override string ToString()
        {
            return Name;
        }
        public virtual string Validate()        //virtual and abstract both allow overriding.
                                                //abstract provides no implementation and makes the class abstract that cannot have an instance created.
        {
            //Name cannotg be empty
            if (String.IsNullOrEmpty(Name))
                return "Name cannot be empty.";

            //Price >= 0
            if (Price < 0)
                return "Price must be >= 0.";
            return null;
        }
        //public int ICanOnlySetIt { get; private set; } //only one of the get or set can be changed and must always be more restrictive.
        //public int ICanOnlySetIt2 { get;} //same as above

    }
}
