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
   public interface IMovieDatabase
    {
        Movie Add(Movie movie);
        Movie Get(int ID);
        IEnumerable<Movie> GetAll();
        void Remove(int ID);
        Movie Update(Movie movie);
    }
}
