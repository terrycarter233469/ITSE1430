/* Terry Carter
 * 10/2017
 * ITSE 1430
 * */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLib
{
    public class Movie
    {
        private string _title;
        private string _description;
        private int _length;
        private bool _owned;

        public string Title
        {
            get{return _title ?? "";}
            set{_title= value?.Trim();}
        }

        public string Description
        {
            get { return _description ?? ""; }
            set { _description = value?.Trim();}
        }

        public int Length {
            get { return _length; } 
            set { _length = value; }
        } 
        public bool Owned
        {
            get { return _owned; }
            set { _owned = value; }
        }

        public override string ToString()
        {
            return Title;
        }

        public virtual string Validate()
        {
            if(String.IsNullOrEmpty(Title))
                return "Name cannot be empty.";

            if(Length < 0)
                return "Price must be >= 0;";
            return null;
        }

    }
}
