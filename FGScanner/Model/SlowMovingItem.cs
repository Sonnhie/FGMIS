using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGScanner.Model
{
    public class SlowMovingItem
    {
            public string Partnumber { get; set; }
            public string Customer { get; set; }
            public DateTime ProdDate { get; set; }
            public int Box { get; set; }
            public int Quantity { get; set; }
            public string Location { get; set; }
            public string Storage_location { get; set; }
            public DateTime Lastmovement { get; set; }
            public string Classification { get; set; }

    }
}
