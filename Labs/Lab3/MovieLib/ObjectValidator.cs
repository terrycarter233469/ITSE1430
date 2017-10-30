/* Terry Carter
 * 10/2017
 * ITSE 1430
 * */


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLib
{
    /// <summary>
    /// Validate Objects
    /// </summary>
    public class ObjectValidator
    {
        public static bool TryValidate(IValidatableObject value, out IEnumerable<ValidationResult> errors)
        {
            var context = new ValidationContext(value);
            var results = new List<ValidationResult>();

            errors = results;
            return Validator.TryValidateObject(value, context, results);
        }
    }
}
