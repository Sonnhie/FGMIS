using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGScanner.DTOs
{
    public class InventoryDto
    {
        public string Partnumber { get; set; } = null!;
        public string CustomerId { get; set; } = null!;
        public DateOnly? ProductionDate { get; set; }
        public string ProductionVersion { get; set; } = null!;
        public int? Quantity {  get; set; }
        public int? Box { get; set; }
        public string Location { get; set; } = null!;
    }
}
