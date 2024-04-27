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
        private int shiftX;
        private int shiftY;
        public CarMove(PictureBox car, Point start, Point finish, int shiftX, int shiftY)
        {
            this.car = car;
            this.start = start;
            this.finish = finish;
            this.shiftX = shiftX;
            this.shiftY = shiftY;
        }
        public void Move()
        {
            car.Location = new Point(car.Location.X + shiftX, car.Location.Y + shiftY);
        }
    }
}
