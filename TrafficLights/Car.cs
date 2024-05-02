using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrafficLights
{
    internal class Car: PictureBox
    {
        public int strip;
        public Car(int strip)
        {
            this.strip = strip;
        }
    }
}
