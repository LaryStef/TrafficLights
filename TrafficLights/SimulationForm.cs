using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrafficLights
{
    public partial class Simulation : Form
    {
        private Random random = new Random();

        
        TrafficLightsStateHandler stateHandler = new TrafficLightsStateHandler();

        private int duration;
        private int[] intencity;
        private int elapsedTime = 0;
        private int trafficLightState = 0;
        private int timeSinceLstChange = 26;

        private List<int> statesList = new List<int>();
        private List<RoadStrip> stripList = new List<RoadStrip>();
        private List<CarMove> carMoveList = new List<CarMove>();
        private List<Car> carsOnFirstPosition = new List<Car>();
        private List<Point> finishPositions = new List<Point>();
        private List<Point> startPositions = new List<Point>();
        private List<Point> firstLinePositions = new List<Point>();

        public Simulation(int duration, int[] intencity, int speed)
        {
            InitializeComponent();
            timer1.Interval = 1000 * speed;
            timer2.Interval = 10 * speed;
            this.intencity = intencity;
            this.duration = duration;

            addPositions();
            timer2.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = elapsedTime.ToString() + " секунд";
            if (elapsedTime == duration) { Close(); }

            if (timeSinceLstChange == 26)
            {
                int sum = 0;
                foreach (int strip in new int[] { 0, 1, 4, 5 })
                {
                    sum += stripList[strip].CountCars();
                }
                stateHandler.horizontalJam = sum;
                label12.Text = stateHandler.verticalJam.ToString() + " " + stateHandler.horizontalJam.ToString();
                statesList = stateHandler.GetStatesArray();

                string a = "";
                for (int i = 0; i < 26; i++)
                {
                    a += statesList[i].ToString();
                }
                label3.Text = a;
                

                timeSinceLstChange = 0;
            }

            if (timeSinceLstChange != 0)
            {
                if (statesList[timeSinceLstChange] == 2 && statesList[timeSinceLstChange - 1] == 0)
                {
                    int sum = 0;
                    foreach (int strip in new int[] { 2, 3, 6, 7 })
                    {
                        sum += stripList[strip].CountCars();
                    }
                    stateHandler.verticalJam = sum;
                }
            }

            changeLights(statesList[timeSinceLstChange]);
            elapsedTime++;
            timeSinceLstChange++;

            for (int i = 0; i < intencity.Length; i++)
            {
                switch (intencity[i])
                {
                    case 1:
                        if (random.Next(0, 15) == 0) { addCar(i); }
                        break;
                    case 2:
                        if (random.Next(0, 8) == 0) { addCar(i); }
                        break;
                    case 3:
                        if (random.Next(0, 4) == 0) { addCar(i); }
                        break;
                    case 4:
                        if (random.Next(0, 3) == 0) { addCar(i); }
                        break;
                    case 5:
                        if (random.Next(0, 2) == 0) { addCar(i); }
                        break;
                }
            }
        }
        
        private void addCar(int road)
        {
            int stripNum = road * 2 + random.Next(0, 2);

            if (stripList[stripNum].CountCars() < 4)
            {
                Car car = new Car(stripNum + 1)
                {
                    Image = Properties.Resources.blueSquare,
                    Size = new Size(26, 26),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Location = startPositions[stripNum]
                };
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Car car = new Car(1)
            //{
            //    Image = Properties.Resources.blueSquare,
            //    Size = new Size(26, 26),
            //    SizeMode = PictureBoxSizeMode.StretchImage,
            //    Location = new Point(40, 292)
            //};
            //Controls.Add(car);
            //car.BringToFront();
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
                        carsOnFirstPosition.Add(carMoveList[i].car);
                        
                    }
                    if (finishPositions.Contains(carMoveList[i].car.Location))
                    {
                        carMoveList[i].car.Dispose();
                    }
                    carMoveList[i].car.arrived = true;
                    carMoveList.Remove(carMoveList[i]);
                }
            }
            
            if (trafficLightState == 1)                                                                                                                                                                               
            {
                for (int i = 0; i < carsOnFirstPosition.Count; i++)
                {
                    switch (carsOnFirstPosition[i].strip)
                    {
                        case 1:
                            carMoveList.Add(new CarMove(carsOnFirstPosition[i], carsOnFirstPosition[i].Location, finishPositions[carsOnFirstPosition[i].strip - 1], 1, 0));
                            stripList[carsOnFirstPosition[i].strip - 1].DequeueCar();
                            break;
                        case 2:
                            carMoveList.Add(new CarMove(carsOnFirstPosition[i], carsOnFirstPosition[i].Location, finishPositions[carsOnFirstPosition[i].strip - 1], 1, 0));
                            stripList[carsOnFirstPosition[i].strip - 1].DequeueCar();
                            break;
                        case 5:
                            carMoveList.Add(new CarMove(carsOnFirstPosition[i], carsOnFirstPosition[i].Location, finishPositions[carsOnFirstPosition[i].strip - 1], -1, 0));
                            stripList[carsOnFirstPosition[i].strip - 1].DequeueCar();
                            break;
                        case 6:
                            carMoveList.Add(new CarMove(carsOnFirstPosition[i], carsOnFirstPosition[i].Location, finishPositions[carsOnFirstPosition[i].strip - 1], -1, 0));
                            stripList[carsOnFirstPosition[i].strip - 1].DequeueCar();
                            break;
                    }
                }
                carsOnFirstPosition.RemoveAll(isHorizontalMovingCar);
            }

            else if (trafficLightState == 2)
            {
                for (int i = 0; i < carsOnFirstPosition.Count; i++)
                {
                    switch (carsOnFirstPosition[i].strip)
                    {
                        case 3:
                            carMoveList.Add(new CarMove(carsOnFirstPosition[i], carsOnFirstPosition[i].Location, finishPositions[carsOnFirstPosition[i].strip - 1], 0, 1));
                            stripList[carsOnFirstPosition[i].strip - 1].DequeueCar();
                            break;
                        case 4:
                            carMoveList.Add(new CarMove(carsOnFirstPosition[i], carsOnFirstPosition[i].Location, finishPositions[carsOnFirstPosition[i].strip - 1], 0, 1));
                            stripList[carsOnFirstPosition[i].strip - 1].DequeueCar();
                            break;
                        case 7:
                            carMoveList.Add(new CarMove(carsOnFirstPosition[i], carsOnFirstPosition[i].Location, finishPositions[carsOnFirstPosition[i].strip - 1], 0, -1));
                            stripList[carsOnFirstPosition[i].strip - 1].DequeueCar();
                            break;
                        case 8:
                            carMoveList.Add(new CarMove(carsOnFirstPosition[i], carsOnFirstPosition[i].Location, finishPositions[carsOnFirstPosition[i].strip - 1], 0, -1));
                            stripList[carsOnFirstPosition[i].strip - 1].DequeueCar();
                            break;
                    }
                }
                carsOnFirstPosition.RemoveAll(isVerticalMovingCar);
            }

            for (int i = 0; i < stripList.Count; i++)
            {
                int carNum = 0;
                foreach (Car car in stripList[i].cars)
                {
                    if (car.arrived && car.Location != stripList[i].positions[carNum])
                    {
                        carMoveList.Add(new CarMove(car, car.Location, stripList[i].positions[carNum], stripList[i].shifts[0], stripList[i].shifts[1]));
                        car.arrived = false;
                    }
                    carNum++;
                }
            }
        }
        
        private bool isHorizontalMovingCar(Car car)
        {
            return (new int[] { 1, 2, 5, 6 }).Contains(car.strip);
        }

        private bool isVerticalMovingCar(Car car)
        {
            return (new int[] { 3, 4, 7, 8 }).Contains(car.strip);
        }

        private void changeLights(int state)
        {
            switch (state)
            {
                case 0:
                    pictureBox2.Image = Properties.Resources.redTrafficLight;
                    pictureBox3.Image = Properties.Resources.redTrafficLight;
                    pictureBox4.Image = Properties.Resources.redTrafficLight;
                    pictureBox5.Image = Properties.Resources.redTrafficLight;
                    trafficLightState = 0;
                    break;
                case 1:
                    pictureBox2.Image = Properties.Resources.greenTrafficLight;
                    pictureBox3.Image = Properties.Resources.greenTrafficLight;
                    pictureBox4.Image = Properties.Resources.redTrafficLight;
                    pictureBox5.Image = Properties.Resources.redTrafficLight;
                    trafficLightState = 1;
                    break;
                case 2:
                    pictureBox2.Image = Properties.Resources.redTrafficLight;
                    pictureBox3.Image = Properties.Resources.redTrafficLight;
                    pictureBox4.Image = Properties.Resources.greenTrafficLight;
                    pictureBox5.Image = Properties.Resources.greenTrafficLight;
                    trafficLightState = 2;
                    break;
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

            finishPositions.Add(new Point(665, 334));
            finishPositions.Add(new Point(665, 367));
            finishPositions.Add(new Point(325, 585));
            finishPositions.Add(new Point(290, 585));
            finishPositions.Add(new Point(40, 292));
            finishPositions.Add(new Point(40, 262));
            finishPositions.Add(new Point(375, 28));
            finishPositions.Add(new Point(410, 28));

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
            }, new int[] { 1, 0 });
            RoadStrip strip2 = new RoadStrip(new Point[]
            {
                new Point (145, 367),
                new Point (115, 367),
                new Point (85, 367),
                new Point (55, 367)
            }, new int[] { 1, 0 });
            RoadStrip strip3 = new RoadStrip(new Point[]
            {
                new Point (325, 130),
                new Point (325, 100),
                new Point (325, 70),
                new Point (325, 40)
            }, new int[] { 0, 1 });
            RoadStrip strip4 = new RoadStrip(new Point[]
            {
                new Point (290, 130),
                new Point (290, 100),
                new Point (290, 70),
                new Point (290, 40)
            }, new int[] { 0, 1 });
            RoadStrip strip5 = new RoadStrip(new Point[]
            {
                new Point (555, 292),
                new Point (585, 292),
                new Point (615, 292),
                new Point (645, 292)
            }, new int[] { -1, 0 });
            RoadStrip strip6 = new RoadStrip(new Point[]
            {
                new Point (555, 262),
                new Point (585, 262),
                new Point (615, 262),
                new Point (645, 262)
            }, new int[] { -1, 0 });
            RoadStrip strip7 = new RoadStrip(new Point[]
            {
                new Point (375, 495),
                new Point (375, 525),
                new Point (375, 555),
                new Point (375, 585)
            }, new int[] { 0, -1 });
            RoadStrip strip8 = new RoadStrip(new Point[]
            {
                new Point (410, 495),
                new Point (410, 525),
                new Point (410, 555),
                new Point (410, 585)
            }, new int[] { 0, -1 });

            stripList.Add(strip1);
            stripList.Add(strip2);
            stripList.Add(strip3);
            stripList.Add(strip4);
            stripList.Add(strip5);
            stripList.Add(strip6);
            stripList.Add(strip7);
            stripList.Add(strip8);
        }

        private void Simulation_Load(object sender, EventArgs e) { }

        private void timer3_Tick(object sender, EventArgs e)
        {
            
        }
    }
}
