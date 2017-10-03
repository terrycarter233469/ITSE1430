using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nile.Windows
{
    public partial class ProductDetailForm : Form
    {
        #region Construction
        public ProductDetailForm() //: base() calls the constructor of the type inherited from
        {
            InitializeComponent();
        }

        public ProductDetailForm(string title) : this()
        {
            Text = title;
        }

        public ProductDetailForm( string title, Product product ) : this(title)
        {
            Product = product;
        }
        #endregion

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            if(Product != null)
            {
                _txtName.Text = Product.Name;
                _txtDescription.Text = Product.Description;
                _txtPrice.Text = Product.Price.ToString();
                _chkDiscontinued.Checked = Product.IsDiscontinued;
            }
        }

        private void textBox2_TextChanged( object sender, EventArgs e )
        {

        }

        public Product Product { get; set; }
        private void showError(string message, string title)
        {
            MessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void OnSave( object sender, EventArgs e )
        {
            var product = new Product();
            product.Name = _txtName.Text;
            product.Description = _txtDescription.Text;
            product.Price = GetPrice();
            product.IsDiscontinued = _chkDiscontinued.Checked;

            //TODO: add validation
            var error = product.Validate();
            if(!String.IsNullOrEmpty(error))
            {
                //Show the error
                showError(error, "Validation Error");
                return;
           
            }

            Product = product;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void OnCancel( object sender, EventArgs e )
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private decimal GetPrice()
        {
            if (Decimal.TryParse(_txtPrice.Text, out decimal price))
                return price;

            //TODO: validate price
            return 0;
        }

        private void ProductDetailForm_FormClosing( object sender, FormClosingEventArgs e )
        {
            // var form = (Form)sender; //if sender is not form or derived from form it crashes the program, not recommended
            var form = sender as Form; //runtime safe type cast, must be reference type. returns null of that type if not successful.

            //for value types
            if(sender is int)
            {
                var intValue = (int)sender;
            }

            //Patter matching
            if(sender is int intValue2)
            {

            }

            if (MessageBox.Show(this, "Are you sure?", "Closing", MessageBoxButtons.YesNo) == DialogResult.No)
                e.Cancel = true;
        }

        private void ProductDetailForm_FormClosed( object sender, FormClosedEventArgs e )
        {

        }
    }
}
