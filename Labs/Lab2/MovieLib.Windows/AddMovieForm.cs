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
    public partial class AddMovieForm : Form
    {
        public AddMovieForm()
        {
            InitializeComponent();
        }

        public AddMovieForm(string title) : this()
        {
            Text = title;
        }

        public AddMovieForm(string title, Movie movie) : this()
        {
            Movie = movie;
        } 

        public Movie Movie {get; set;}

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if(Movie != null)
            {
                _txtTitle.Text = Movie.Title;
                _txtDescription.Text = Movie.Description;
                _txtLength.Text = Movie.Length.ToString();
                _chkOwned.Checked = Movie.Owned;
            }

            ValidateChildren();
        }

        private void ShowError(string message, string title)
        {
            MessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void _OnSave(object sender, EventArgs e)
        {
            if(!ValidateChildren())
            {
                return;
            }
            var movie = new Movie
            {
                Title = _txtTitle.Text,
                Description = _txtDescription.Text,
                Length = GetLength(_txtLength),
                Owned = _chkOwned.Checked
            };

            var error = movie.Validate();
            if(!String.IsNullOrEmpty(error))
            {
                ShowError(error, "Validation Error");
                return;
            }

            Movie = movie;
            DialogResult = DialogResult.OK;
            Close();
        }

        private int GetLength(TextBox control)
        {
            if(Int32.TryParse(control.Text, out int length))
                return length;
            return -1;
        }

        private void _OnCancel(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void AddMovieForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var form = sender as Form;

            if(sender is int)
            {
                var intValue = (int)sender;
            }

            if(MessageBox.Show(this, "Are you sure?", "Exiting", MessageBoxButtons.YesNo) == DialogResult.No)
                e.Cancel = true;
        }

        private void _OnValidatingLength(object sender, CancelEventArgs e)
        {
            var tb = sender as TextBox;

            if(GetLength(tb) < 0)
            {
                e.Cancel = true;
                _errors.SetError(_txtLength, "Length must be >= 0.");
            } else
                _errors.SetError(_txtLength, "");
        }

        private void _OnValidatingName(object sender, CancelEventArgs e)
        {
            var tb = sender as TextBox;
            if(String.IsNullOrEmpty(tb.Text))
                _errors.SetError(tb, "Name is required");
            else
                _errors.SetError(tb, "");
        }
    }
}
