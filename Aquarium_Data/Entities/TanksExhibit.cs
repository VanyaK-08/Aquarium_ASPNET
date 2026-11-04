using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aquarium_Data.Entities
{
    public class TanksExhibit
    {
        public int TankId { get; set; }
        public Tank Tank { get; set; } = null!;
        public int ExhibitId { get; set; }
        public Exhibit Exhibit { get; set; } = null!;
    }
}
