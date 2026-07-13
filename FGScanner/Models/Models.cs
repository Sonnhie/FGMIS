using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGScanner.Models
{
    [Table("transaction_history_clean")]
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("partnumber")]
        public string Partnumber { get; set; }

        [Column("prod_date")]
        public DateTime ProdDate { get; set; }

        [Column("customer_id")]
        public string CustomerId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("Box")]
        public int Box { get; set; }

        [Column("prod_ver")]
        public string ProdVer { get; set; }

        [Column("entry_date")]
        public DateTime EntryDate { get; set; }

        [Column("transaction_type")]
        public string TransactionType { get; set; }

        [Column("location")]
        public string Location { get; set; }

        [Column("remarks")]
        public string Remarks { get; set; }

        [Column("WH_id")]
        public string WhId { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("storage_location")]
        public string StorageLocation { get; set; }

        [Column("TransactionId")]
        public Guid TransactionID { get; set; }

        [Column("IsSynced")]
        public bool IsSynced { get; set; }

        [Column("SyncStatus")]
        public int? SyncStatus { get; set; }

        [Column("in_charge")]
        public string InCharge { get; set; }

        [Column("control_number")]
        public string? controlNumber { get; set; }

        [ForeignKey("controlNumber")]
        public virtual Shipment? Shipment { get; set; }

        [ForeignKey("controlNumber")]
        public virtual Return? Returns { get; set; }
    }

    [Table("Shipment_table")]
    public class Shipment
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
      
        [Column("transaction_id")]
        public string TransactionID { get; set; }

        [Column("entry_date")]
        public DateTime EntryDate { get; set; }

        [Column("Whid")]
        public string WhId { get; set; }

        [Column("Status")]
        public string Status { get; set; }

        [Column("ShipmentId")]
        public Guid ShipmentID { get; set; }

        [Column("IsSynced")]
        public bool IsSynced { get; set; }

        [Column("SyncStatus")]
        public int SyncStatus { get; set; }


        [ForeignKey("TransactionID")]
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }

    [Table("Return_table")]
    public class Return
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
       
        [Column("transaction_id")]
        public string TransactionID { get; set; }


        [Column("entry_date")]
        public DateTime EntryDate { get; set; }

        [Column("remarks")]
        public string Remarks { get; set; }
        [Column("Whid")]
        public string WhId { get; set; }

        [Column("ReturnId")]
        public Guid ReturnID { get; set; }

        [Column("IsSynced")]
        public bool IsSynced { get; set; }

        [Column("SyncStatus")]
        public int SyncStatus { get; set; }

        [Column("fromLocation")]
        public string? From { get; set; } = string.Empty;

        [Column("toLocation")]
        public string? To { get; set; } = string.Empty;

        [Column("Status")]
        public string? Status { get; set; } = string.Empty;

        [NotMapped]
        public int Quantity { get; set; }

        [NotMapped]
        public int Box { get; set; }


        [ForeignKey("TransactionID")]
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }

    [Table("actual_inventory_clean")]
    public class ActualInventory
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("partnumber")]
        public string Partnumber { get; set; }
        [Column("prod_date")]
        public DateTime ProdDate { get; set; }
        [Column("customer")]
        public string CustomerId { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("prod_ver")]
        public string ProdVer { get; set; }
        [Column("location")]
        public string Location { get; set; }
        [Column("total_box")]
        public int TotalBox { get; set; }
        [Column("remarks")]
        public string Remarks { get; set; }
        [Column("WhId")]
        public string WhId { get; set; }
        [Column("storage_location")]
        public string StorageLocation { get; set; }
        [Column("last_in_date")]
        public DateTime? Last_In_Date { get; set; }
        [Column("last_out_date")]
        public DateTime? Last_Out_Date { get; set; }
        [Column("updated_date")]
        public DateTime? UpdatedDate { get; set; }
        [Column("movement_classification")]
        public string MovementClassification { get; set; }
    }

    [Table("product")]
    public class Products
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("partnumber")]
        public string PartNumber { get; set; }
        [Column("partname")]
        public string PartName { get; set; }
        [Column("customer_id")]
        public string CustomerId { get; set; }
        [Column("PPS")]
        public int PPS { get; set; }
    }

    [Table("rack_table")]
    public class RackLocation
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("wh_id")]
        public string WhId { get; set; }
        [Column("rack_no")]
        public string RackNo { get; set; }
    }


    public class ScannedData
    {
        public string PartNumber { get; set; }
        public DateTime ProductionDate { get; set; }
        public string CustomerId { get; set; }
        public int Quantity { get; set; }
        public string ProductionVersion { get; set; }
        public string Location { get; set; }
        public string Remarks { get; set; }
        public string StorageLocation { get; set; }
        public string WhId { get; set; }
    }

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

    public class PrintDocumentDTO
    {
        public string DocNo { get; set; }
        public DateTime EntryDate { get; set; }
        public string PreparedBy { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public List<PrintItemDTO> Items { get; set; } = new List<PrintItemDTO>();
    }

    public class PrintItemDTO
    {
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public DateTime ProductionDate { get; set; }
        public int Box { get; set; }
        public int Quantity { get; set; }
        public int PPS { get; set; }
        public string remarks { get; set; }
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
        public int id { get; set; }
        public string MonthYear { get; set; }
        public string ErpLocation { get; set; }
        public string PreparedBy { get; set; }
        public int ControlNo { get; set; }
        public string PartNo { get; set; }
        public string location { get; set; }
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

    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public int PageSize { get; set; }
    }

    public class ReportGeneration<T>
    {
        public string Title { get; set; } = string.Empty;
        public string[] Columns { get; set; } = Array.Empty<string>();
        public List<T> Items { get; set; } = [];
    }

    public class InventoryReport
    {
        public string partnumber { get; set; }
        public string customer { get; set; }
        public DateTime proddate { get; set; }
        public string prodver { get; set; }
        public string location { get; set; }
        public int quantity { get; set; }
        public int box { get; set; }
        public string storagelocation { get; set; }
        public DateTime? updatedInventory { get; set; }
        public string classification { get; set; }
    }

    public class SlowMovingReport
    {
        public string partnumber { get; set; }
        public string customer { get; set; }
        public DateTime proddate { get; set; }
        public string prodver { get; set; }
        public string location { get; set; }
        public int quantity { get; set; }
        public int box { get; set; }
        public string storagelocation { get; set; }
        public DateTime? updatedInventory { get; set; }
    }

    public class StockCardHeader
    {
        // 1. Fixed PascalCase naming
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public int EndingStock { get; set; }
        public string Customer { get; set; }

        // 2. The Header contains a list of Ledger transactions
        public List<StockLedger> Ledgers { get; set; } = new List<StockLedger>();
    }

    public class StockLedger
    {
        // If you need a unique ID for the database, add one here:
        // public int Id { get; set; } 

        public DateTime? InventoryDate { get; set; }
        public int In { get; set; }
        public int Out { get; set; }
        public int BeginningStock { get; set; }
        public int RunningStock { get; set; }
        public string Incharge { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;

        // 3. If you need a reference back to the header, do it like this:
        // (Do not initialize it with 'new', let the code that creates it assign it)
        public StockCardHeader Header { get; set; }
    }
}
