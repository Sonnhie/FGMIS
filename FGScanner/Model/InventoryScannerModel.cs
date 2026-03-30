using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGScanner.Model
{
    public class InventoryScannerModel
    {
        public string QRCode { get; set; }
        public DateTime ScanDate { get; set; }
        public string User {  get; set; }
    }

    public class ScannedModel 
    {
        public string TransactionId { get; set; }
        public string PartNumber { get; set; }
      
        public string Customer { get; set; }
        public DateTime ProductionDate { get; set; }
        public int Quantity { get; set; }
        
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ProductionVersion { get; set; }
        public string Location { get; set; }
        public string New_Location { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string Storage_location { get; set; }
        public string Whid { get; set; }
       
    }

    public class DPIModel
    {
        public string partnumber { get; set; }
        public int quantity { get; set; }
        public int totalbox { get; set; }
        public int PPS { get; set; }
    }
}
