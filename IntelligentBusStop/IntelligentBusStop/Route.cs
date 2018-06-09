using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStop
{
    internal class Route
    {
        public int routeNumber { get; set; }
        public string transportType { get; set; }
        public int ID { get; set; }
        public string path { get; set; }

        public Route()
        {
        }

        public Route(Route obj)
        {
            routeNumber = obj.routeNumber;
            transportType = obj.transportType;
            ID = obj.ID;
            path = obj.path;
        }
    }
}