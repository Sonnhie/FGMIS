using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGScanner.DTOs
{
    public record StockKey(
      string PartNo,
      DateOnly ProductionDate,
      string ProductionVersion,
      string Warehouse,
      string Location
    );
}
