using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentBusStop
{
    internal class Stop
    {
        public int ID { get; set; }
        public string name { get; set; }
        public /*string*/List<int> connectedRoutes { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double weight { get; set; }

        public Stop()
        {
        }

        public Stop(Stop obj)
        {
            ID = obj.ID;
            name = obj.name;
            connectedRoutes = obj.connectedRoutes;
            latitude = obj.latitude;
            longitude = obj.longitude;
            weight = obj.weight;
        }
    }
}