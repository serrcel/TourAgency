using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourAgencyProject
{
    public class Records
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PersonFIO { get; set; }
        public int Person_id { get; set; }
        public int Tour_id { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
