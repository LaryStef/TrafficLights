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
        public Queue<Car> cars = new Queue<Car>();
        public Point[] positions;
        public int[] shifts;
        public RoadStrip(Point[] positions, int[] shifts)
        {
            this.positions = positions;
            this.shifts = shifts;
        }
        public Point EnqueueCar(Car car)
        {
            cars.Enqueue(car);
            return positions[cars.Count() - 1];
        }
        public void DequeueCar()
        {
            cars.Dequeue();
        }
        public int CountCars()
        {
            return cars.Count();
        }
    }
}
