/* Terry Carter
 * 10/2017
 * ITSE 1430
 * */

using System;
using System.Collections.Generic;

namespace MovieLib.Data.Memory
{
    public class MemoryMovieDatabase : MovieDatabase
    {
        protected override Movie AddCore(Movie movie)
        {
            var newMovie = CopyMovie(movie);
            _movies.Add(newMovie);

            if(newMovie.ID <= 0)
                newMovie.ID = _nextId++;
            else if(newMovie.ID >= _nextId)
                _nextId = newMovie.ID + 1;

            return CopyMovie(newMovie);
        }

        protected override IEnumerable<Movie> GetAllCore()
        {
            foreach(var movie in _movies)
                yield return CopyMovie(movie);
        }

        protected override Movie GetCore(int id)
        {
            var movie = FindMovie(id);
            return (movie != null) ? CopyMovie(movie) : null;
        }

        protected override void RemoveCore(int id)
        {
            if(id <= 0)
                return;
            var movie = FindMovie(id);
            if(movie != null)
                _movies.Remove(movie);
        }

        protected override Movie UpdateCore(Movie existing, Movie movie)
        {
            //replace
            existing =FindMovie(movie.ID);
            _movies.Remove(existing);

            var newMovie = CopyMovie(movie);
            _movies.Add(newMovie);

            return CopyMovie(newMovie);
        }

        private Movie FindMovie(int id)
        {
            foreach(var movie in _movies)
            {
                if(movie.ID == id)
                {
                    return movie;
                }
            }

            return null;
        }
        
        private Movie CopyMovie(Movie movie)
        {
            if(movie == null)
                return null;

            var newMovie = new Movie();
            newMovie.ID = movie.ID;
            newMovie.Title = movie.Title;
            newMovie.Description = movie.Description;
            newMovie.Length = movie.Length;
            newMovie.Owned = movie.Owned;

            return newMovie;
        }

        private List<Movie> _movies = new List<Movie>(); //converts the list into a generic type that will store products in it.
        private int _nextId = 1;
    }
}

