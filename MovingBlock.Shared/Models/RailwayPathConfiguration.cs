using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingBlock.Shared.Models
{
    public class RailwayPathConfiguration
    {
        public int Distance { get; set; }
        public int Speed { get; set; }
        public int SafeDistance { get; set; }
        public int CriticalDistance { get; set; }

        public Location? StartLocation { get; set; }
        public Location? EndLocation { get; set;}
    }
}
