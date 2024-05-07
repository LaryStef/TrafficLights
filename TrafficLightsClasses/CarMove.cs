using System.Drawing;

namespace TrafficLightsClasses
{
    public class CarMove
    {
        public Car car;
        public Point start;
        public Point finish;
        private readonly int shiftX;
        private readonly int shiftY;
        public CarMove(Car car, Point start, Point finish, int shiftX, int shiftY)
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