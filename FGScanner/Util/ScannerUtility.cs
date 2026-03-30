using FGScanner.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FGScanner.Util
{
    
    public class ScannerUtility
    { 
        public bool ProcessQRData(string QRData, out InventoryItemModel itemModel, out string error, string location = null)
        {
            itemModel = null;
            error = null;

            string Partnumber = "";
            string ProductionVer = "";
            string date;
            string Initial;
            string Quantity;

            var SlashedPart = QRData.Split('/');
            var Check = new TransactionRepo();

            if (SlashedPart.Length != 2)
            {
                error = "Invalid QR Code!";
                return false;
            }

            var LeftPart = SlashedPart[0].Split('-');

            if (LeftPart.Length < 2)
            {
                error = "Invalid QR Code!";
                return false;
            }

            if (LeftPart.Length == 3)
            {
                Partnumber = LeftPart[0] + "-" + LeftPart[1];
                ProductionVer = LeftPart[2];
            }
            else if(LeftPart.Length == 2) 
            {
                Partnumber = LeftPart[0];
                ProductionVer = LeftPart[1];
            }
            else if(LeftPart.Length == 4)
            {
               Partnumber = LeftPart[0] + "-" + LeftPart[1] + "-" + LeftPart[2];
               ProductionVer = LeftPart[3];
             }

            var RightPart = SlashedPart[1].Split('-');

            if (RightPart.Length != 3)
            {
                error = "Invalid QR Code";
                return false;
            }

            if (!RightPart[0].StartsWith("O"))
            {
                error = "Invalid order format!";
                return false;
            }

            Initial = RightPart[0].Substring(1);
            Quantity = Initial.Substring(0, Initial.Length - 2);
            date = RightPart[0].Substring(RightPart[0].Length - 2) + "-" + RightPart[1] + "-" + RightPart[2];


            if (!DateTime.TryParseExact(date, "dd-MM-yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var ProdDate))
            {
                error = "Invalid date format!";
                return false;
            }

            if (!int.TryParse(Quantity, out int qty))
            {
                error = "Invalid quantity format!";
                return false;
            }


            itemModel = new InventoryItemModel
            {
                PartNumber = Partnumber,
                ProductionDate = ProdDate,
                Quantity = qty,
                ProductionVer = ProductionVer
            };


            return true;
        }
    }
}
