using FGScanner.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGScanner.Services.Interfaces
{
    public class ITransactionInterface
    {
        Task<List<string>> GetRackLocationsAsync(string warehouseId);
        Task<List<InventoryDto>> GetActualInventories(string warehouseid, string location);

    }
}
