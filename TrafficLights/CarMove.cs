using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrafficLights
{
    internal class CarMove
    {
        public PictureBox car;
        public Point start;
        public Point finish;
        public CarMove(PictureBox car, Point start, Point finish) {
            this.car = car;
            this.start = start;
            this.finish = finish;
        }
    }
}
