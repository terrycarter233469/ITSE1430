using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieLib.Web.Models
{
    public class MovieViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "Please enter a valid number.")]
        public int Length { get; set; }

        public bool IsOwned { get; set; }
    }
}