    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingBlock.Shared.Models
{
    public class SectionModel
    {
        public int Distance { get; set; }  // kms
        public int Speed { get; set; }     // kmph
        public int SafeDistance { get; set; } // kms
        public int CriticalDistance { get; set; } // kms

        public Location? StartLocation { get; set; }
        public Location? EndLocation { get; set;}
    }
}
