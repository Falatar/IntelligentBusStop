using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentBusStop
{
    internal class Transport
    {
        public int trackerId { get; set; }
        public int routeId { get; set; }
        public int previousStop { get; set; }
        public int nextStop { get; set; }
        public DateTime arrivalTime { get; set; }
        public DateTime departureTime { get; set; }

        public Transport()
        {
        }

        public Transport(Transport obj)
        {
            trackerId = obj.trackerId;
            routeId = obj.routeId;
            previousStop = obj.previousStop;
            nextStop = obj.nextStop;
            arrivalTime = obj.arrivalTime;
            departureTime = obj.departureTime;
        }
    }
}