using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStop
{
    internal class Road
    {
        public int firstStop { get; set; }
        public int secondStop { get; set; }
        public double weight { get; set; }
        public int length { get; set; }

        public Road()
        {
        }

        public Road(Road obj)
        {
            firstStop = obj.firstStop;
            secondStop = obj.secondStop;
            weight = obj.weight;
            length = obj.length;
        }
    }
}