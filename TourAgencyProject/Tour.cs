using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourAgencyProject
{
    public class Tour
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Arrival { get; set; }
        public string Departure { get; set; }
        public int Cost { get; set; }
        public int Programm { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateBack { get; set; }
    }
}
