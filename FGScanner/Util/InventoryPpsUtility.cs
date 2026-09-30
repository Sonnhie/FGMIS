using System;
using System.Collections.Generic;
using System.Linq;

namespace FGScanner.Util
{
    public static class InventoryPpsUtility
    {
        public const string Bpps = "BPPS";
        public const string ExactPps = "Exact PPS";
        public const string Mixed = "Mixed";

        public static string GetPpsType(int quantity, int boxes, int productPps, string remarks)
        {
            if (string.Equals(remarks, Bpps, StringComparison.OrdinalIgnoreCase))
                return Bpps;

            if (boxes > 0 && productPps > 0 && quantity == boxes * productPps)
                return ExactPps;

            return boxes > 0 ? Bpps : string.Empty;
        }

        public static int GetLedgerBoxQuantity(string category, int? storedBoxes)
        {
            bool isScanBased = string.Equals(category, Bpps, StringComparison.OrdinalIgnoreCase) ||
                               string.Equals(category, "Finished Goods", StringComparison.OrdinalIgnoreCase) ||
                               string.Equals(category, "Shipment", StringComparison.OrdinalIgnoreCase) ||
                               string.Equals(category, "Warehouse Return", StringComparison.OrdinalIgnoreCase);

            return isScanBased ? 1 : Math.Max(storedBoxes ?? 0, 0);
        }

        public static string GetGroupedPpsType(IEnumerable<string> ppsTypes)
        {
            if (ppsTypes == null)
                return string.Empty;

            var distinctTypes = ppsTypes
                .Where(type => !string.IsNullOrWhiteSpace(type))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            return distinctTypes.Count switch
            {
                0 => string.Empty,
                1 => distinctTypes[0],
                _ => Mixed
            };
        }

        public static decimal CalculateDisplayedPps(
            string ppsType,
            int quantity,
            int boxes,
            int productPps)
        {
            if (string.Equals(ppsType, ExactPps, StringComparison.OrdinalIgnoreCase))
                return productPps;

            return boxes > 0
                ? Math.Round((decimal)quantity / boxes, 2)
                : quantity;
        }
    }
}
