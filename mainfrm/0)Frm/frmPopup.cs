using System;
using System.Windows.Forms;

namespace mainfrm
{
    public partial class frmPopup : Form
    {
        public frmPopup(string title, string name, string value)
        {
            InitializeComponent();
            this.Text = title;
            lbName.Text = name;
            tbValue.Text = value;
        }
        public string Value { get { return tbValue.Text; } }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
