using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGScanner.Model
{
    public class chartmodel
    {
      public string Title { get; set; }
      public int Value { get; set; }
    }

    public class MonthlyInventorySummary
    {
        public int Month { get; set; }
        public int In { get; set; }
        public int Out { get; set; }
    }

    public class CustomerStock
    {
        public string Customer { get; set; }
        public int Stock { get; set; }
    }

    public class MonthlyShipment
    {
        public int Month { get; set; }
        public int Out { get; set; }
    }
}
