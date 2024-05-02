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
        private List<RoadStrip> stripList = new List<RoadStrip>();
        private List<CarMove> carMoveList = new List<CarMove>();
        private List<Point> startPositions = new List<Point>();
        private List<Point> firstLinePositions = new List<Point>();
        private List<Car> carsOnFirstPosition = new List<Car>();
        // добавляем машины, которые доехали до первой позиции на светофоре
        // при 1 едут боковые, при 0 едут верхние и нижние
        public Simulation(int duration, int intencity, int speed)
        {
            InitializeComponent();
            this.intencity = intencity;
            this.speed = speed;
            this.duration = duration;

            addPositions();
            timer2.Enabled = true;
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
            addCar();
            elapsedTime += 1;
        }

        private void addCar()
        {
            Random random = new Random();
            int stripNum = random.Next(0, 8);

            if (stripList[stripNum].CountCars() < 4)
            {
                Car car = new Car(stripNum);

                car.Image = Properties.Resources.blueSquare;
                car.Size = new Size(26, 26);
                car.SizeMode = PictureBoxSizeMode.StretchImage;
                car.Location = startPositions[stripNum];
                Controls.Add(car);
                car.BringToFront();

                Point target = stripList[stripNum].EnqueueCar(car);

                if (stripNum == 0 || stripNum == 1)
                {
                    carMoveList.Add(new CarMove(car, car.Location, target, 1, 0));
                } else if (stripNum == 2 || stripNum == 3) 
                {
                    carMoveList.Add(new CarMove(car, car.Location, target, 0, 1));
                } else if (stripNum == 4 || stripNum == 5)
                {
                    carMoveList.Add(new CarMove(car, car.Location, target, -1, 0));
                } else if (stripNum == 6 || stripNum == 7)
                {
                    carMoveList.Add(new CarMove(car, car.Location, target, 0, -1));
                }

                return;
            }
            label3.Text = $"полоса переполнена {stripNum + 1} : {elapsedTime}";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < carMoveList.Count; i++)
            {
                if (carMoveList[i].car.Location != carMoveList[i].finish)
                {
                    carMoveList[i].Move();
                }
                else
                {
                    if (firstLinePositions.Contains(carMoveList[i].car.Location))
                    {
                        
                    }
                    carMoveList.Remove(carMoveList[i]);
                }
            }
            if (trafficLightState == 1)
            {
                // создаем CarMove для первых автомобилей из 1, 2, 5, 6 полос
                foreach (Car car in carsOnFirstPosition)
                {
                    carMoveList.Add(new CarMove(car, car.Location, new Point(), 1, 0));
                }
            }
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

        private void addPositions()
        {
            startPositions.Add(new Point(40, 334));
            startPositions.Add(new Point(40, 367));
            startPositions.Add(new Point(325, 28));
            startPositions.Add(new Point(290, 28));
            startPositions.Add(new Point(665, 292));
            startPositions.Add(new Point(665, 262));
            startPositions.Add(new Point(375, 585));
            startPositions.Add(new Point(410, 585));

            firstLinePositions.Add(new Point(145, 334));
            firstLinePositions.Add(new Point(145, 367));
            firstLinePositions.Add(new Point(325, 130));
            firstLinePositions.Add(new Point(290, 130));
            firstLinePositions.Add(new Point(555, 292));
            firstLinePositions.Add(new Point(555, 262));
            firstLinePositions.Add(new Point(375, 495));
            firstLinePositions.Add(new Point(410, 495));


            RoadStrip strip1 = new RoadStrip(new Point[]
            {
                new Point (145, 334),
                new Point (115, 334),
                new Point (85, 334),
                new Point (55, 334)
            });
            RoadStrip strip2 = new RoadStrip(new Point[]
            {
                new Point (145, 367),
                new Point (115, 367),
                new Point (85, 367),
                new Point (55, 367)
            });
            RoadStrip strip3 = new RoadStrip(new Point[]
            {
                new Point (325, 130),
                new Point (325, 100),
                new Point (325, 70),
                new Point (325, 40)
            });
            RoadStrip strip4 = new RoadStrip(new Point[]
            {
                new Point (290, 130),
                new Point (290, 100),
                new Point (290, 70),
                new Point (290, 40)
            });
            RoadStrip strip5 = new RoadStrip(new Point[]
            {
                new Point (555, 292),
                new Point (585, 292),
                new Point (615, 292),
                new Point (645, 292)
            });
            RoadStrip strip6 = new RoadStrip(new Point[]
            {
                new Point (555, 262),
                new Point (585, 262),
                new Point (615, 262),
                new Point (645, 262)
            });
            RoadStrip strip7 = new RoadStrip(new Point[]
            {
                new Point (375, 495),
                new Point (375, 525),
                new Point (375, 555),
                new Point (375, 585)
            });
            RoadStrip strip8 = new RoadStrip(new Point[]
            {
                new Point (410, 495),
                new Point (410, 525),
                new Point (410, 555),
                new Point (410, 585)
            });

            stripList.Add(strip1);
            stripList.Add(strip2);
            stripList.Add(strip3);
            stripList.Add(strip4);
            stripList.Add(strip5);
            stripList.Add(strip6);
            stripList.Add(strip7);
            stripList.Add(strip8);
        }

        private void Simulation_Load(object sender, EventArgs e)
        {

        }
    }
}
