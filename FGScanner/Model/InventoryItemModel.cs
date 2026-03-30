using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGScanner.Model
{
    public class InventoryItemModel
    {
        //Identification Label
        public int Id { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime AssemblyDate { get; set; }
        public int Quantity { get; set; }
        public int BoxNumber { get; set; }
        public string SerialNumber { get; set; }
        public string ProductionVer { get; set; }
    }


}
