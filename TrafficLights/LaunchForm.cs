using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrafficLights
{
    public partial class LaunchForm : Form
    {
        public LaunchForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int duration = 0;
            int intensity1 = 0;
            int intensity2 = 0;
            int intensity3 = 0;
            int intensity4 = 0;
            int[] intensityArr;
            int speed = 0;
            string error = "";

            if (maskedTextBox1.Text == "" || Convert.ToInt32(maskedTextBox1.Text) == 0) {
                error = "Длительность симуляции не выбрана";
            } else {
                duration = Convert.ToInt32(maskedTextBox1.Text);
            }

            intensity1 = Convert.ToInt32(groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text);
            intensity2 = Convert.ToInt32(groupBox6.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text);
            intensity3 = Convert.ToInt32(groupBox4.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text);
            intensity4 = Convert.ToInt32(groupBox3.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text);
            intensityArr = new int[] { intensity1, intensity2, intensity3, intensity4 };

            if (radioButton4.Checked) {
                speed = 3;
            } else if (radioButton5.Checked) {
                speed = 2;
            } else if (radioButton6.Checked) {
                speed = 1;
            } else {
                error = "Скорость симуляции не выбрана";
            }

            if (error != "")
            {
                Form errorForm = new ErrorForm(error);
                errorForm.Show();
                return;
            }

            Form simulationForm = new Simulation(duration, intensityArr, speed);
            simulationForm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton28_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void radioButton24_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
