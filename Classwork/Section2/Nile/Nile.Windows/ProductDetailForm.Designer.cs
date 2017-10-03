namespace Nile.Windows
{
    partial class ProductDetailForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._txtName = new System.Windows.Forms.TextBox();
            this._txtDescription = new System.Windows.Forms.TextBox();
            this._txtPrice = new System.Windows.Forms.TextBox();
            this._chkDiscontinued = new System.Windows.Forms.CheckBox();
            this._bttnSave = new System.Windows.Forms.Button();
            this._bttnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Description:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Price:";
            // 
            // _txtName
            // 
            this._txtName.Location = new System.Drawing.Point(90, 14);
            this._txtName.Name = "_txtName";
            this._txtName.Size = new System.Drawing.Size(100, 20);
            this._txtName.TabIndex = 4;
            // 
            // _txtDescription
            // 
            this._txtDescription.Location = new System.Drawing.Point(90, 82);
            this._txtDescription.Name = "_txtDescription";
            this._txtDescription.Size = new System.Drawing.Size(100, 20);
            this._txtDescription.TabIndex = 5;
            this._txtDescription.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // _txtPrice
            // 
            this._txtPrice.Location = new System.Drawing.Point(90, 141);
            this._txtPrice.Name = "_txtPrice";
            this._txtPrice.Size = new System.Drawing.Size(100, 20);
            this._txtPrice.TabIndex = 6;
            // 
            // _chkDiscontinued
            // 
            this._chkDiscontinued.AutoSize = true;
            this._chkDiscontinued.Location = new System.Drawing.Point(90, 186);
            this._chkDiscontinued.Name = "_chkDiscontinued";
            this._chkDiscontinued.Size = new System.Drawing.Size(105, 17);
            this._chkDiscontinued.TabIndex = 7;
            this._chkDiscontinued.Text = "Is Discontinued?";
            this._chkDiscontinued.UseVisualStyleBackColor = true;
            // 
            // _bttnSave
            // 
            this._bttnSave.Location = new System.Drawing.Point(230, 236);
            this._bttnSave.Name = "_bttnSave";
            this._bttnSave.Size = new System.Drawing.Size(75, 23);
            this._bttnSave.TabIndex = 8;
            this._bttnSave.Text = "Save";
            this._bttnSave.UseVisualStyleBackColor = true;
            this._bttnSave.Click += new System.EventHandler(this.OnSave);
            // 
            // _bttnCancel
            // 
            this._bttnCancel.Location = new System.Drawing.Point(327, 236);
            this._bttnCancel.Name = "_bttnCancel";
            this._bttnCancel.Size = new System.Drawing.Size(75, 23);
            this._bttnCancel.TabIndex = 9;
            this._bttnCancel.Text = "Cancel";
            this._bttnCancel.UseVisualStyleBackColor = true;
            this._bttnCancel.Click += new System.EventHandler(this.OnCancel);
            // 
            // ProductDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 272);
            this.Controls.Add(this._bttnCancel);
            this.Controls.Add(this._bttnSave);
            this.Controls.Add(this._chkDiscontinued);
            this.Controls.Add(this._txtPrice);
            this.Controls.Add(this._txtDescription);
            this.Controls.Add(this._txtName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProductDetailForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Product Details";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProductDetailForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProductDetailForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _txtName;
        private System.Windows.Forms.TextBox _txtDescription;
        private System.Windows.Forms.TextBox _txtPrice;
        private System.Windows.Forms.CheckBox _chkDiscontinued;
        private System.Windows.Forms.Button _bttnSave;
        private System.Windows.Forms.Button _bttnCancel;
    }
}