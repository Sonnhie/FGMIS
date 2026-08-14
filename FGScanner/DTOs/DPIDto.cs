using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGScanner.DTOs
{
    public class DPIList
    {
        public string Partnumber { get; set; }
        public int Quantity { get; set; }
        public int PPS { get; set; }
        public int Box { get; set; }
    }

    public class DPIInfo
    {
        public DPIList Item { get; set; }
        public int TotalBox { get; set; }
    }

    public class DPIDTO
    {
        public string Partnumber { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int PPS { get; set; }
        public int Box { get; set; }
    }
}
