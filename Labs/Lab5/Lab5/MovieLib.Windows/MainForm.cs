/* Terry Carter
 * 10/2017
 * ITSE 1430
 * */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MovieLib.Data.Memory;
using MovieLib.Data.Sql;

namespace MovieLib.Windows
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var connString = ConfigurationManager.ConnectionStrings["MovieDatabase"].ConnectionString;
            _database = new SqlMovieDatabase(connString);

            _gridMovies.AutoGenerateColumns = false;
            UpdateList();
        }

        #region Event Handlers
        private void OnMovieAdd(object sender, EventArgs e)
        {
            var child = new AddMovieForm("Add a Movie");
            if(child.ShowDialog(this) != DialogResult.OK)
                return;

            //Save movie
            try
            {
                _database.Add(child.Movie);
            } catch(ValidationException ex)
            {
                DisplayError(ex, "Validation Failed");
            } catch(Exception ex)
            {
                DisplayError(ex, "Add Failed");
            };
            UpdateList();
        }

        private void OnMovieEdit(object sender, EventArgs e)
        {
            var movie = GetSelectedMovie();
            if(movie == null)
            {
                MessageBox.Show("No movies available.");
                return;
            }

            EditMovie(movie);
        }

        private void OnHelpAbout(object sender, EventArgs e)
        {
            var about = new AboutBox();
            about.ShowDialog(this);
        }

        private void OnFileExit(object sender, EventArgs e)
        {
            Close();
        }

        private void OnMovieRemove(object sender, EventArgs e)
        {
            var movie = GetSelectedMovie();
            if(movie == null)
            {
                MessageBox.Show("No movies available.");
                return;
            }

            DeleteMovie(movie);
        }

        private void OnKeyDownGrid(object sender, KeyEventArgs e)
        {
            var movie = GetSelectedMovie();

            if(e.KeyCode == Keys.Delete)
            {
                if(movie != null)
                    DeleteMovie(movie);
            }

            if(e.KeyCode == Keys.Enter)
            {
                if(movie != null)
                    EditMovie(movie);
            }

        }

        private void OnEditRow(object sender, DataGridViewCellEventArgs e)
        {
            var grid = sender as DataGridView;

            //Handle Column Clicks
            if(e.RowIndex < 0)
                return;

            var row = grid.Rows[e.RowIndex];
            var item = row.DataBoundItem as Movie;

            if(item != null)
                EditMovie(item);
        }
        #endregion

        #region Private Members
        private void EditMovie(Movie movie)
        {
            var child = new AddMovieForm("Movie Info");
            child.Movie = movie;
            if(child.ShowDialog(this) != DialogResult.OK)
                return;

            //Save movie
            try
            {
                _database.Update(child.Movie);
            } catch(Exception ex)
            {
                DisplayError(ex, "Update Failed");
            };
            UpdateList();
        }

        private void DeleteMovie(Movie movie)
        {
            //Confirm
            if(MessageBox.Show(this, $"Are you sure you want to delete '{movie.Title}'?",
                                "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //Delete product
            try
            {
                _database.Remove(movie.ID);
            } catch(Exception e)
            {
                DisplayError(e, "Delete Failed");
            };
            UpdateList();
        }

        private void UpdateList()
        {
            try
            {
                _bsMovies.DataSource = _database.GetAll().ToList();
            } catch(Exception e)
            {
                DisplayError(e, "Refresh Failed");
                _bsMovies.DataSource = null;
            };
        }

        private Movie GetSelectedMovie()
        {
            // return _listProducts.SelectedItem as Product;

            if(_gridMovies.SelectedRows.Count > 0)
                return _gridMovies.SelectedRows[0].DataBoundItem as Movie;

            return null;

        }

        private void DisplayError(Exception error, string title = "Error")
        {
            DisplayError(error.Message, title);
        }

        private void DisplayError(string message, string title = "Error")
        {
            MessageBox.Show(this, message, title ?? "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        private IMovieDatabase _database;
    }
}
