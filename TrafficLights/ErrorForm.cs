using System;
using System.Windows.Forms;


namespace TrafficLights
{
    public partial class ErrorForm : Form
    {
        public ErrorForm(string error)
        {
            InitializeComponent();
            label2.Text = error;
        }

        private void ErrorForm_Load(object sender, EventArgs e)
        {

        }
    }
}
