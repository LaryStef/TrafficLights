using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrafficLights
{
    internal class RoadStrip
    {
        private Queue<PictureBox> cars = new Queue<PictureBox>();
        private Point[] positions;
        public RoadStrip(Point[] positions)
        {
            this.positions = positions;
        }
        public Point EnqueueCar(PictureBox car)
        {
            cars.Enqueue(car);
            return positions[cars.Count() - 1];
        }
        public int CountCars()
        {
            return cars.Count();
        }
    }
}
