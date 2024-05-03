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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int duration = 0;
            int intencity = 0;
            int speed = 0;
            string error = "";

            if (maskedTextBox1.Text != "") {
                duration = Convert.ToInt32(maskedTextBox1.Text);
            } else {
                error = "Длительность симуляции не выбрана";
            }

            if (radioButton1.Checked) {
                intencity = 1;
            } else if (radioButton2.Checked)
            {
                intencity = 3;
            } else if (radioButton3.Checked) {
                intencity = 5;
            } else {
                error = "Интерсивность трафика не выбрана";
            }

            if (radioButton4.Checked) {
                speed = 1;
            } else if (radioButton5.Checked) {
                speed = 3;
            } else if (radioButton6.Checked) {
                speed = 5;
            } else {
                error = "Скорость симуляции не выбрана";
            }

            if (error != "")
            {
                Form errorForm = new ErrorForm(error);
                errorForm.Show();
                return;
            }

            Form simulationForm = new Simulation(duration, intencity, speed);
            simulationForm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
