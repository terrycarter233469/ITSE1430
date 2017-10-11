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

namespace MovieLib.Windows
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private Movie _movie;

        private void OnMovieAdd(object sender, EventArgs e)
        {
            var child = new AddMovieForm("Add a Movie");
            if(child.ShowDialog(this) != DialogResult.OK)
                return;

            _movie = child.Movie;
        }

        private void OnMovieEdit(object sender, EventArgs e)
        {
            if(_movie == null)
            {
                MessageBox.Show(this, "No movie data available, please add a movie first.");
                return;
            }
            var child = new AddMovieForm("Edit a Movie");
            child.Movie = _movie;

            if(child.ShowDialog(this) != DialogResult.OK)
                return;

            _movie = child.Movie;
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
            if(_movie == null)
            {
                MessageBox.Show(this, "No movie data available, please add a movie first.");
                return;
            }

            if(_movie == null)
                return;

            //confirm
            if(MessageBox.Show(this, $"Are you sure you want to remove '{_movie.Title}'?",
                "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //TODO: Delete product
            _movie = null;
        }
    }
}
