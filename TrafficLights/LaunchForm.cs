using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TrafficLights
{
    public partial class LaunchForm : Form
    {
        public LaunchForm()
        {
            InitializeComponent();

            try
            {
                string path = Path.GetFullPath("UserSettings.txt");
                StreamReader reader = new StreamReader(path);
                string line = reader.ReadLine();
                string[] data = line.Split(new char[] { ';', });

                maskedTextBox1.Text = data[0];
                groupBox1.Controls.OfType<RadioButton>().ToList().Where(el => el.Text == data[1]).ToList()[0].Checked = true;
                groupBox6.Controls.OfType<RadioButton>().ToList().Where(el => el.Text == data[2]).ToList()[0].Checked = true;
                groupBox4.Controls.OfType<RadioButton>().ToList().Where(el => el.Text == data[3]).ToList()[0].Checked = true;
                groupBox3.Controls.OfType<RadioButton>().ToList().Where(el => el.Text == data[4]).ToList()[0].Checked = true;
                groupBox2.Controls.OfType<RadioButton>().ToList().Where(el => el.Text == data[5]).ToList()[0].Checked = true;


                checkBox1.Checked = Convert.ToBoolean(data[6]);
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int duration;
            int[] intensityArr;
            int speed;
            bool simpleMode = false;

            if (maskedTextBox1.Text == "" || Convert.ToInt32(maskedTextBox1.Text) == 0)
            {
                Form errorForm = new ErrorForm("Длительность симуляции не выбрана");
                errorForm.Show();
                return;
            }
            duration = Convert.ToInt32(maskedTextBox1.Text);

            intensityArr = new int[]
            {
                Convert.ToInt32(groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text),
                Convert.ToInt32(groupBox6.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text),
                Convert.ToInt32(groupBox4.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text),
                Convert.ToInt32(groupBox3.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text)
            };

            if (radioButton4.Checked) {
                speed = 3;
            } else if (radioButton5.Checked) {
                speed = 2;
            } else {
                speed = 1;
            }

            if (checkBox1.Checked) { simpleMode = true; }

            Form simulationForm = new Simulation(duration, intensityArr, speed, simpleMode);
            simulationForm.Show();
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void radioButton28_CheckedChanged(object sender, EventArgs e) { }
        private void Form1_Load(object sender, EventArgs e) { }
        private void radioButton24_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton10_CheckedChanged(object sender, EventArgs e) { }
    }
}
