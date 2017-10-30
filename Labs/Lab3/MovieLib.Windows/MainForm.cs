/* Terry Carter
 * 10/2017
 * ITSE 1430
 * */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MovieLib.Data.Memory;

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

            _gridMovies.AutoGenerateColumns = false;

            if(_database != null)
                UpdateList();
        }

        private void OnMovieAdd(object sender, EventArgs e)
        {
            var child = new AddMovieForm("Add a Movie");
            if(child.ShowDialog(this) != DialogResult.OK)
                return;

            //Save movie
            _database.Add(child.Movie);
            UpdateList();
        }

        private void OnMovieEdit(object sender, EventArgs e)
        {
            var movie = GetSelectedMovie();
            if(movie == null)
            {
                MessageBox.Show("No products available.");
                return;
            }

           EditMovie(movie);
        }

        private void EditMovie(Movie movie)
        {
            var child = new AddMovieForm("Movie Info");
            child.Movie = movie;
            if(child.ShowDialog(this) != DialogResult.OK)
                return;

            //Save movie
            _database.Update(child.Movie);
            UpdateList();
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
                MessageBox.Show("No products available.");
                return;
            }

            DeleteMovie(movie);
        }

        private void DeleteMovie(Movie movie)
        {
            //Confirm
            if(MessageBox.Show(this, $"Are you sure you want to delete '{movie.Title}'?",
                                "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //Delete product
            _database.Remove(movie.ID);
            UpdateList();
        }

        private void UpdateList()
        {
            _bsMovies.DataSource = _database.GetAll().ToList();
        }

        private Movie GetSelectedMovie()
        {
            // return _listProducts.SelectedItem as Product;

            if(_gridMovies.SelectedRows.Count > 0)
                return _gridMovies.SelectedRows[0].DataBoundItem as Movie;

            return null;

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

        private IMovieDatabase _database = new MemoryMovieDatabase();

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
    }
}
