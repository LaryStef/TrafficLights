using System.Windows.Forms;


namespace TrafficLights
{
    internal class Car: PictureBox
    {
        public int strip;
        public bool arrived = false;
        public Car(int strip)
        {
            this.strip = strip;
        }
    }
}
