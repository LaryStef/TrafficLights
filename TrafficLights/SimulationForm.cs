using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrafficLights
{
    public partial class Simulation : Form
    {
        private int intencity;
        private int speed;
        private int duration;
        private int elapsedTime = 0;
        private int trafficLightState = 1;
        private RoadStrip strip1 = new RoadStrip(new Point[] {
            new Point (145, 334),
            new Point (115, 334),
            new Point (85, 334),
            new Point (55, 334),
        });
        private CarMove carMove;
        public Simulation(int duration, int intencity, int speed)
        {
            InitializeComponent();
            this.intencity = intencity;
            this.speed = speed;
            this.duration = duration;
        }

        private void Simulation_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            label2.Text = elapsedTime.ToString() + " секунд";
            if (elapsedTime == duration) {
                Close();
            }
            if (elapsedTime % 10 == 0)
            {
                changeLights();
            }
            elapsedTime += 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PictureBox car = new PictureBox();

            car.Image = Properties.Resources.blueSquare;
            car.Size = new Size(26, 26);
            car.SizeMode = PictureBoxSizeMode.StretchImage;
            car.Location = new Point(40, 334);
            car.Name = "car";
            Controls.Add(car);
            car.BringToFront();

            Point target = strip1.AddCar(car);
            carMove = new CarMove(car, car.Location, target);
            label3.Text = target.ToString();
            timer2.Enabled = true;
        }

        private void changeLights()
        {
            if (trafficLightState == 0)
            {
                pictureBox2.Image = Properties.Resources.greenTrafficLight;
                pictureBox3.Image = Properties.Resources.greenTrafficLight;
                pictureBox4.Image = Properties.Resources.redTrafficLight;
                pictureBox5.Image = Properties.Resources.redTrafficLight;
                trafficLightState = 1;
            }
            else
            {
                pictureBox2.Image = Properties.Resources.redTrafficLight;
                pictureBox3.Image = Properties.Resources.redTrafficLight;
                pictureBox4.Image = Properties.Resources.greenTrafficLight;
                pictureBox5.Image = Properties.Resources.greenTrafficLight;
                trafficLightState = 0;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (carMove.car.Location != carMove.finish)
            {
                carMove.car.Location = new Point(carMove.car.Location.X + 1, carMove.car.Location.Y);
            }
        }
    }
}
