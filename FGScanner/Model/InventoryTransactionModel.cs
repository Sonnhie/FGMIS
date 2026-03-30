using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace FGScanner.Model
{
    public class InventoryTransactionModel
    {
        //Transaction Data Model
        public string TransactionId { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public string Customer { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime AssemblyDate { get; set; }
        public int Quantity { get; set; }
        public int Box { get; set; }
        public int Price { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string User { get; set; }
        public string ProductionVersion { get; set; }
        public string Location { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string Storage_location { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public string FromStorageLocation { get; set; }
        public string ToStorageLocation { get; set; }
        public DateTime Updated_date { get; set; }
        public string WhId { get; set; }
    }

    public class LegendColor
    {
        public string colors { get; set; }
        public string title { get; set; }
    }

    public class RackCount
    {
        public string RackId { get; set; }
        public int Count { get; set; }
    }

    public class OrdersSummary
    {
        public string Partnumber { get; set; }
        public DateTime ProductionDate { get; set; }
        public string Customer { get; set; }
        public int Box { get; set; }
        public int Quantity { get; set; }
        public string TransactionId { get; set; }
    }

    public class OrdersSummaryExtend: OrdersSummary
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Remarks { get; set; }
        public string PartName { get; set; }
        public DateTime EntryDate { get; set; }
        public string User { get; set; }
    }

    public class FilterOptions
    {
        public string FilterByPartNumber { get; set; }
        public string FilterBycustomer { get; set; }
        public string FilterByLocation { get; set; }
        public string FilterByProductionVersion { get; set; }
        public string FilterByStorageLocation { get; set; }
        public DateTime FilterByFromDate { get; set; }
        public DateTime FilterByToDate { get; set; }
        public string SortedBy { get; set; }
    }

    public class SlowMovingSummary
    {
        public string Partnumber { get; set; }
        public string Customer { get; set; }
        public int Box { get; set; }
        public int Quantity { get; set; }
        public DateTime LotDate { get; set; }
        public DateTime LastMovementDate { get; set; }
    }

    public class RackList
    {
        public string RackId { get; set; }
        public string WhId { get; set; }
        public int Box { get; set; }
        public int Quantity { get; set; }
        public string PickStatus { get; set; }
    }

    public class InventoryCardData
    {
        public string MonthYear { get; set; }
        public string ErpLocation { get; set; }
        public string PreparedBy { get; set; }
        public int ControlNo { get; set; }
        public string PartNo { get; set; }
        public int PPS { get; set; }
        public int GrandTotalBoxes { get; set; }
        public int GrandTotalQuantity { get; set; }
        public List<InventoryRow> Rows { get; set; } = new List<InventoryRow>();
        public System.Drawing.Image QrCode { get; set; }
    }

    public class InventoryRow
    {
        public string LotNo { get; set; }
        public int Boxes { get; set; }
        public int Quantity { get; set; }
        public int TotalQty => Boxes * Quantity; 
    }

    public class TransferSlipData
    {
        public string DocumentNo { get; set; }
        public string Shift { get; set; }
        public string IssueDate { get; set; }
        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
        public string PreparedBy { get; set; }
        public string CheckedBy { get; set; }
        public string ReceivedBy { get; set; }
        public string EncodedBy { get; set; }
        public List<TransferRow> Rows { get; set; } = new List<TransferRow>();
    }

    public class TransferRow
    {
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public string RevNo { get; set; }
        public string InspectionDate { get; set; }
        public string ProductionDate { get; set; }
        public string NoBox { get; set; }
        public string PPS { get; set; }
        public string Pcs { get; set; }
        public string Remarks { get; set; }
    }
}
