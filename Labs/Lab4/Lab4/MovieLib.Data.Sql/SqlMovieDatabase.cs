using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLib.Data.Sql
{
    /// <summary>
    /// Provides an SQL Server implementation of IMovieDatabase
    /// </summary>
    public class SqlMovieDatabase : MovieDatabase
    {

        #region Construction
        public SqlMovieDatabase(string connectionString)
        {
            _connectionString = connectionString;
        }

        private readonly string _connectionString;

        #endregion

        protected override Movie AddCore(Movie movie)
        {
            var id = 0;
            using(var conn = OpenDatabase())
            {
                var cmd = new SqlCommand("AddMovie", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@Title", movie.Title);
                cmd.Parameters.AddWithValue("@Description", movie.Description); 
                cmd.Parameters.AddWithValue("@Length", movie.Length);
                cmd.Parameters.AddWithValue("@IsOwned", movie.Owned);

                id = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return GetCore(id);
        }

        protected override int CheckExisting(string title)
        {
            var movies = new List<Movie>();
            using(var connection = OpenDatabase())
            {
                var cmd = new SqlCommand("GetAllMovies", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                using(var reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        var movie = new Movie()
                        {
                            ID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                            Title = reader.GetFieldValue<string>(1),
                            Description = reader.GetString(2),
                            Length = reader.GetInt32(3),
                            Owned = reader.GetBoolean(4)
                        };
                        movies.Add(movie);
                    }
                };
            }

            foreach(Movie movie in movies)
            {
                if(movie.Title == title)
                    return movie.ID;
            }

            return -1;
        }

        protected override IEnumerable<Movie> GetAllCore()
        {
            var movies = new List<Movie>();
            using(var connection = OpenDatabase())
            {
                var cmd = new SqlCommand("GetAllMovies", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                using(var reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        var movie = new Movie()
                        {
                            ID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                            Title = reader.GetFieldValue<string>(1),
                            Description = reader.GetString(2),
                            Length = reader.GetInt32(3),
                            Owned = reader.GetBoolean(4)
                        };
                        movies.Add(movie);
                    }
                };
                return movies;
            }
        }

        protected override Movie GetCore(int id)
        {
            using(var conn = OpenDatabase())
            {
                var cmd = new SqlCommand("GetMovie", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@id", id);

                //using a dataset instead of a reader
                var ds = new DataSet();
                var da = new SqlDataAdapter()
                {
                    SelectCommand = cmd
                };

                da.Fill(ds);

                var tables = ds.Tables.OfType<DataTable>().FirstOrDefault();

                if(tables != null)
                {
                    var row = tables.AsEnumerable().FirstOrDefault();
                    if(row != null)
                    {
                        return new Movie()
                        {
                            ID = Convert.ToInt32(row["id"]),
                            Title = row.Field<string>("Title"),
                            Description = row.Field<string>("Description"),
                            Length = row.Field<int>("Length"),
                            Owned = row.Field<bool>("IsOwned")
                        };

                    }
                }
            }

            return null;
        }

        protected override void RemoveCore(int id)
        {
            using(var conn = OpenDatabase())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "RemoveMovie";
                cmd.CommandType = CommandType.StoredProcedure;

                var parameter = new SqlParameter("@id", id);
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();
            }
        }

        protected override Movie UpdateCore(Movie existing, Movie newMovie)
        {
            using(var conn = OpenDatabase())
            {
                var cmd = new SqlCommand("UpdateMovie", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@id", existing.ID);
                cmd.Parameters.AddWithValue("@title", newMovie.Title);
                cmd.Parameters.AddWithValue("@description", newMovie.Description);
                cmd.Parameters.AddWithValue("@length", newMovie.Length);
                cmd.Parameters.AddWithValue("@IsOwned", newMovie.Owned);

                cmd.ExecuteNonQuery();
            }

            return GetCore(existing.ID);
        }

        private SqlConnection OpenDatabase()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open(); //explicitly creates a connection to the database
            return connection;
        }
    }
}
