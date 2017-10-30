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
    public abstract class MovieDatabase : IMovieDatabase
    {
        protected abstract Movie AddCore(Movie movie);
        protected abstract Movie UpdateCore(Movie existing, Movie newMovie);
        protected abstract Movie GetCore(int id);
        protected abstract IEnumerable<Movie> GetAllCore();
        protected abstract void RemoveCore(int id);

        public Movie Add(Movie movie)
        {
            //validate
            if(movie == null)
                return null;

            if(!ObjectValidator.TryValidate(movie, out var errors))
                return null;

            return AddCore(movie);
        }
        public Movie Get(int ID)
        {
            //validate
            if(ID <= 0)
                return null;
            return GetCore(ID);
        }
        public IEnumerable<Movie> GetAll()
        {
            return GetAllCore();
        }
        public void Remove(int ID)
        {
            if(ID <= 0)
                return;
            RemoveCore(ID);
        }
        public Movie Update(Movie movie)
        {
            //validate
            if(movie == null)
                return null;
            
            if(!ObjectValidator.TryValidate(movie, out var errors))
                return null;
            
            //get existing movie
            var existing = GetCore(movie.ID);
            if(existing == null)
                return null;

            return UpdateCore(existing, movie);
        }
    }
}
