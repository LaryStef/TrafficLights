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
        Queue<PictureBox> cars = new Queue<PictureBox>();
        Point[] positions;
        public RoadStrip(Point[] positions) {
            this.positions = positions;
        }
        public Point AddCar(PictureBox car)
        {
            cars.Enqueue(car);
            return positions[cars.Count() - 1];
        }
    }
}
