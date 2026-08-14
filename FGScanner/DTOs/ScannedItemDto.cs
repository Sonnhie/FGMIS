using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGScanner.DTOs
{
    public class ScannedItemDto
    {
        public string PartNumber { get; set; }
        public DateOnly ProductionDate { get; set; }
        public string CustomerId { get; set; }
        public int Quantity { get; set; }
        public string ProductionVersion { get; set; }
        public string Location { get; set; }
        public string Remarks { get; set; }
        public string StorageLocation { get; set; }
        public string WhId { get; set; }
    }
}
