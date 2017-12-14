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
    /// <summary>Provides a base implementation of <see cref="IMovieDatabase"/>/// </summary>
    public abstract class MovieDatabase : IMovieDatabase
    {
        #region Protected Members

        /// <summary>Adds a Movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The added Movie.</returns>
        protected abstract Movie AddCore(Movie movie);

        /// <summary>Updates a movie.</summary>
        /// <param name="existing">The existing movie.</param>
        /// <param name="newMovie">The movie to update.</param>
        /// <returns>The updated movie.</returns>
        protected abstract Movie UpdateCore(Movie existing, Movie newMovie);

        /// <summary>Get a movie given its ID.</summary>
        /// <param name="id">The ID.</param>
        /// <returns>The movie, if any.</returns>
        protected abstract Movie GetCore(int id);

        protected abstract IEnumerable<Movie> GetAllCore();

        /// <summary>Removes a movie given its ID.</summary>
        /// <param name="id">The ID.</param>
        protected abstract void RemoveCore(int id);

        /// <summary>Checks to see if a movie title already exists in the database./// </summary>
        /// <param name="title"></param>
        /// <returns>Returns the id if a duplicate title is found, -1 otherwise</returns>
        protected abstract int CheckExisting(string title);
        #endregion

        /// <summary>Adds a movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The added product.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="movie"/> is null.</exception>
        /// <exception cref="ValidationException"><paramref name= "movie"/> title already added. </exception>
        /// <exception cref="ValidationException"><paramref name="movie"/> is invalid.</exception>
        public Movie Add(Movie movie)
        {
            //validate
            if(movie == null)
                throw new ArgumentNullException(nameof(movie), "Movie was null");

            if(CheckExisting(movie.Title) > 0)
                throw new Exception("Movie title already added");

            ObjectValidator.Validate(movie);

            try
            {
                return AddCore(movie);
            } catch(Exception e)
            {
                //Throw different exception
                throw new Exception("Add failed", e);
            }
        }

        /// <summary>Get a specific  movie.</summary>
        /// <returns>The movie, if it exists.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="ID"/> must be greater than or equal to 0.</exception> 
        public Movie Get(int ID)
        {
            //validate
            if(ID <= 0)
                throw new ArgumentOutOfRangeException(nameof(ID), "Id must be greater than 0");

            return GetCore(ID);
        }

        /// <summary>Gets all movies.</summary>
        /// <returns>The movies.</returns>
        public IEnumerable<Movie> GetAll()
        {
            return GetAllCore();
        }

        /// <summary>Removes the movie.</summary>
        /// <param name="id">The movie to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="ID"/> must be greater than or equal to 0.</exception> 
        public void Remove(int ID)
        {
            if(ID <= 0)
                throw new ArgumentOutOfRangeException(nameof(ID), "Id must be greater than 0.");

            RemoveCore(ID);
        }

        /// <summary>Updates a movie.</summary>
        /// <param name="movie">The movie to update.</param>
        /// <returns>The updated movie.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="movie"/> is null.</exception>
        /// <exception cref="ValidationException"><paramref name="movie"/> is invalid.</exception>
        /// <exception cref="Exception">Movie not found.</exception>
        /// <exception cref="Exception">Movie title already added</exception>"
        public Movie Update(Movie movie)
        {
            //validate
            if(movie == null)
                throw new ArgumentNullException(nameof(movie));

            ObjectValidator.Validate(movie);

            //get existing movie
            var existing = GetCore(movie.ID) ?? throw new Exception("Movie not found");

            if(CheckExisting(movie.Title) >= 0 && CheckExisting(movie.Title) != movie.ID)
                throw new Exception("Movie title already added");

            return UpdateCore(existing, movie);
        }
    }
}
