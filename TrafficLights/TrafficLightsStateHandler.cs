using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrafficLights
{
    internal class TrafficLightsStateHandler
    {
        private const int loopDuration = 26;
        private const int pause = 3;
        public int horizontalJam = 1;
        public int verticalJam = 1;

        public List<int> GetStatesArray()
        {
            horizontalJam = horizontalJam == 0 ? 1 : horizontalJam;
            verticalJam = verticalJam == 0 ? 1 : verticalJam;
            int horTime = horizontalJam * (loopDuration - pause * 2) / (horizontalJam + verticalJam);
            List<int> states = new List<int>();

            for (int i = 0; i < horTime; i++)
            {
                states.Add(1);
            }
            for (int i = horTime; i < horTime + pause; i++)
            {
                states.Add(0);
            }
            for (int i = horTime + pause; i < loopDuration - pause; i++)
            {
                states.Add(2);
            }
            for (int i = loopDuration - pause; i < loopDuration; i++)
            {
                states.Add(0);
            }
            if (states.Count != 26)
            {
                for (int i = 0; i < 26 - states.Count; i++)
                {
                    states.Insert(0, 1);
                }
            }
            return states;
        }
    }
}
