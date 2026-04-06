using FGScanner.Model;
using OfficeOpenXml.Data.Connection;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;

namespace FGScanner.Util
{
    public class TransactionRepo
    {
        private readonly db_connection _Connection;

        public TransactionRepo()
        {
            _Connection = new db_connection();
        }
        public void InsertTransaction(InventoryTransactionModel item)
        {
            using (SqlConnection conn = _Connection.Getconnection())
            {
                conn.Open();
                SqlTransaction tx = conn.BeginTransaction();
                try
                {
                    string sql = "INSERT INTO transaction_history (partnumber, prod_date, customer_id, quantity, prod_ver, entry_date, transaction_type, location, remarks, storage_location, WH_id) " +
                                     "VALUES (@partnumber, @prod_date, @customer_id, @quantity, @prod_ver, @entry_date, @transaction_type, @location, @remarks, @storage_location, @WH_id)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn, tx))
                    {
                        cmd.Parameters.Add("@partnumber", SqlDbType.NVarChar).Value = item.PartNumber;
                        cmd.Parameters.Add("@prod_date", SqlDbType.Date).Value = item.ProductionDate;
                        cmd.Parameters.Add("@customer_id", SqlDbType.NVarChar).Value = item.Customer;
                        cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = item.Quantity;
                        cmd.Parameters.Add("@prod_ver", SqlDbType.NVarChar).Value = item.ProductionVersion;
                        cmd.Parameters.Add("@entry_date", SqlDbType.DateTime).Value = item.TransactionDate;
                        cmd.Parameters.Add("@transaction_type", SqlDbType.NVarChar).Value = item.TransactionType;
                        cmd.Parameters.Add("@location", SqlDbType.NVarChar).Value = item.Location;
                        cmd.Parameters.Add("@remarks", SqlDbType.NVarChar).Value = item.Remarks;
                        cmd.Parameters.Add("@storage_location", SqlDbType.NVarChar).Value = item.Storage_location;
                        cmd.Parameters.Add("@WH_id", SqlDbType.NVarChar).Value = item.WhId;


                        cmd.ExecuteNonQuery();
                        tx.Commit();
                    }
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    MessageBox.Show("Error: " + ex.Message, "SQL Error");
                }
            }
        }
        public async Task InsertSingleTransaction(InventoryTransactionModel item, SqlConnection conn, SqlTransaction tx)
        {
            string sql = @"INSERT INTO transaction_history
                   (partnumber, prod_date, customer_id, quantity, prod_ver, entry_date, transaction_type, location, remarks, storage_location, control_number, WH_id)
                   VALUES
                   (@partnumber, @prod_date, @customer_id, @quantity, @prod_ver, @entry_date, @transaction_type, @location, @remarks, @storage_location, @id, @WH_id)";

            using (SqlCommand cmd = new SqlCommand(sql, conn, tx))
            {
                cmd.Parameters.Add("@partnumber", SqlDbType.NVarChar).Value = item.PartNumber;
                cmd.Parameters.Add("@prod_date", SqlDbType.Date).Value = item.ProductionDate;
                cmd.Parameters.Add("@customer_id", SqlDbType.NVarChar).Value = item.Customer;
                cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = item.Quantity;
                cmd.Parameters.Add("@prod_ver", SqlDbType.NVarChar).Value = item.ProductionVersion;
                cmd.Parameters.Add("@entry_date", SqlDbType.DateTime).Value = item.TransactionDate;
                cmd.Parameters.Add("@transaction_type", SqlDbType.NVarChar).Value = item.TransactionType;
                cmd.Parameters.Add("@location", SqlDbType.NVarChar).Value = item.Location;
                cmd.Parameters.Add("@remarks", SqlDbType.NVarChar).Value = item.Remarks;
                cmd.Parameters.Add("@storage_location", SqlDbType.NVarChar).Value = item.Storage_location;
                cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = item.TransactionId;
                cmd.Parameters.Add("@WH_id", SqlDbType.NVarChar).Value = item.WhId; 

                await cmd.ExecuteNonQueryAsync();
            }
        }
        public async Task InsertShipmentTransaction(InventoryTransactionModel item, SqlConnection conn, SqlTransaction tx)
        {
            string sql = @"INSERT INTO Shipment_table
                   (transaction_id, partnumber, prod_date, customer, quantity, prod_ver, entry_date, Whid)
                   VALUES
                   (@transaction_id, @partnumber, @prod_date, @customer_id, @quantity, @prod_ver, @entry_date, @whid)";
            using (SqlCommand cmd = new SqlCommand(sql, conn, tx))
            {
                cmd.Parameters.Add("@transaction_id", SqlDbType.NVarChar).Value = item.TransactionId;
                cmd.Parameters.Add("@partnumber", SqlDbType.NVarChar).Value = item.PartNumber;
                cmd.Parameters.Add("@prod_date", SqlDbType.Date).Value = item.ProductionDate;
                cmd.Parameters.Add("@customer_id", SqlDbType.NVarChar).Value = item.Customer;
                cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = item.Quantity;
                cmd.Parameters.Add("@prod_ver", SqlDbType.NVarChar).Value = item.ProductionVersion;
                cmd.Parameters.Add("@entry_date", SqlDbType.DateTime).Value = item.TransactionDate;
                cmd.Parameters.Add("@whid", SqlDbType.NVarChar).Value = item.WhId;

                await cmd.ExecuteNonQueryAsync();
            }
        }



        public async Task InsertReturnTransaction(InventoryTransactionModel item, SqlConnection conn, SqlTransaction tx)
        {
            string sql = @"INSERT INTO return_table
                   (transaction_id, partnumber, prod_date, customer, quantity, prod_ver, entry_date, location, storage_location, remarks, ToStorageLocation, Whid)
                   VALUES
                   (@transaction_id, @partnumber, @prod_date, @customer_id, @quantity, @prod_ver, @entry_date, @location, @storage_location, @remarks, @ToStorageLocation, @whid)";
            using (SqlCommand cmd = new SqlCommand(sql, conn, tx))
            {
                cmd.Parameters.Add("@transaction_id", SqlDbType.NVarChar).Value = item.TransactionId;
                cmd.Parameters.Add("@partnumber", SqlDbType.NVarChar).Value = item.PartNumber;
                cmd.Parameters.Add("@prod_date", SqlDbType.Date).Value = item.ProductionDate;
                cmd.Parameters.Add("@customer_id", SqlDbType.NVarChar).Value = item.Customer;
                cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = item.Quantity;
                cmd.Parameters.Add("@prod_ver", SqlDbType.NVarChar).Value = item.ProductionVersion;
                cmd.Parameters.Add("@entry_date", SqlDbType.DateTime).Value = item.TransactionDate;
                cmd.Parameters.Add("@location", SqlDbType.NVarChar).Value = item.Location;
                cmd.Parameters.Add("@storage_location", SqlDbType.NVarChar).Value = item.FromStorageLocation;
                cmd.Parameters.Add("@remarks", SqlDbType.NVarChar).Value = item.Remarks;
                cmd.Parameters.Add("@ToStorageLocation", SqlDbType.NVarChar).Value = item.ToStorageLocation;
                cmd.Parameters.Add("@whid", SqlDbType.NVarChar).Value = item.WhId;

                await cmd.ExecuteNonQueryAsync();
            }
        }
        public int CheckStock(string partnumber, string location)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    string sql = "SELECT ISNULL(SUM(CASE WHEN transaction_type = 'IN' THEN 1 else 0 END),0) - " +
                                 "ISNULL(SUM(CASE WHEN transaction_type = 'OUT' THEN 1 else 0 END),0) " +
                                 "as TotalItems FROM transaction_history " +
                                 "WHERE partnumber = @partnumber AND location = @location";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@partnumber", SqlDbType.NVarChar).Value = partnumber;
                        cmd.Parameters.Add("@location", SqlDbType.NVarChar).Value = location;
                        result = Convert.ToInt32(cmd.ExecuteScalar());  
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }

            return result;
        }
        public List<InventoryTransactionModel> GetTransactionHistory()
        {
            List<InventoryTransactionModel> Inventory = new List<InventoryTransactionModel>();

            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    string sql = "SELECT * FROM transaction_history WHERE transaction_type = 'IN' AND entry_date >= CAST(GETDATE() AS Date) ORDER BY entry_date DESC";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                InventoryTransactionModel item = new InventoryTransactionModel
                                {
                                    TransactionDate = reader["entry_date"] != DBNull.Value ? Convert.ToDateTime(reader["entry_date"]).Date : DateTime.MinValue.Date,
                                    PartNumber = reader["partnumber"]?.ToString() ?? string.Empty,
                                    ProductionDate = reader["prod_date"] != DBNull.Value ? Convert.ToDateTime(reader["prod_date"]).Date : DateTime.MinValue.Date,
                                    Quantity = Convert.ToInt32(reader["quantity"]),
                                    Customer = reader["customer_id"]?.ToString() ?? string.Empty,
                                    ProductionVersion = reader["prod_ver"]?.ToString() ?? string.Empty,
                                    TransactionType = reader["transaction_type"]?.ToString() ?? string.Empty,
                                    Location = reader["location"]?.ToString() ?? string.Empty,
                                    Storage_location = reader["storage_location"]?.ToString() ?? string.Empty,
                                    Remarks = reader["remarks"]?.ToString() ?? string.Empty
                                };
                                Inventory.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }
            return Inventory;
        }
        public List<string> GetRackLocations(string whname)
        {
            List<string> list = new List<string>();

            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    string sql = "SELECT DISTINCT rack_no FROM rack_table where wh_id = @whid ORDER BY rack_no";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@whid", SqlDbType.NVarChar).Value = whname;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(reader.GetString(0));
                            }
                            ;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }
            return list;
        }
        public List<string> GetStorageLocations()
        {
            List<string> list = new List<string>();
            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    string sql = "SELECT DISTINCT erp_loc FROM storage_location WHERE erp_loc IS NOT NULL ORDER BY erp_loc";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(reader.GetString(0));
                            }
                            ;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }
            return list;
        }
        public string GetCustomer(string partnumber)
        {
            string customer = string.Empty;
            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    string sql = "SELECT DISTINCT customer_id FROM product WHERE partnumber = @partnumber";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@partnumber", SqlDbType.NVarChar).Value = partnumber;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                customer = reader["customer_id"]?.ToString() ?? string.Empty;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }
            return customer;
        }
        public int GetLatestTransactionId()
        {
            int latestId = 0;
            try
            {
               using (SqlConnection conn = _Connection.Getconnection())
               {
                   conn.Open();
                   string sql = "SELECT ISNULL(MAX(transaction_id), 0) FROM transaction_history";
                   using (SqlCommand cmd = new SqlCommand(sql, conn))
                   {
                       latestId = Convert.ToInt32(cmd.ExecuteScalar());
                   }
               }
            }
            catch (Exception ex)
            {
               MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }
            return latestId;
        }
        public int GetLatestReturnId()
        {
            int latestId = 0;
            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("GetNextReturnId", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        latestId = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }
            return latestId;
        }
        public List<RackCount> GetTotalItemPerRack(string WHid)
        {
            List<RackCount> counts = new List<RackCount>();
            
            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    string sql = @"SELECT location, COUNT(partnumber) AS item FROM actual_inventory WHERE WhId = @whid GROUP BY location";

                    using (SqlCommand cmd = new SqlCommand(sql,conn))
                    {
                        cmd.Parameters.Add("@whid", SqlDbType.NVarChar).Value = WHid;

                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                           while (rd.Read())
                            {
                                counts.Add(new RackCount
                                {
                                    RackId = rd["location"]?.ToString() ?? string.Empty,
                                    Count = Convert.ToInt32(rd["item"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }

            return counts;
        }

        public int GetItemCountByRack(string rack, string whid)
        {
            int count = 0;

            using (SqlConnection conn = _Connection.Getconnection())
            {
                conn.Open();

                string sql = @"SELECT 
                        quantity
                       FROM actual_inventory
                       WHERE location = @rack AND WhId = @whid";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@rack", rack);
                    cmd.Parameters.Add("@whid", SqlDbType.NVarChar).Value = whid;

                    var result = cmd.ExecuteScalar();

                    count = result == DBNull.Value ? 0 : Convert.ToInt32(result);
                }
            }

            return count;
        }

        public List<InventoryTransactionModel> GetItemByLocation(string location, string whId)
        {
            List<InventoryTransactionModel> Items = new List<InventoryTransactionModel>();
            
            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    string sql = @"SELECT 
                                    partnumber,
                                    location,
                                    prod_date,
                                    customer,
                                    prod_ver,
                                    total_box,
                                    quantity,
                                    ROW_NUMBER() OVER (
                                        PARTITION BY partnumber, WhId, location
                                        ORDER BY prod_date, partnumber
                                    ) AS PickOrder
                                FROM actual_inventory
                                WHERE location = @loc
                                AND WhId = @whId
                                AND quantity > 0
                                ORDER BY PickOrder";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@loc", SqlDbType.NVarChar).Value = location;
                        cmd.Parameters.Add("@whId", SqlDbType.NVarChar).Value = whId;

                        using (SqlDataReader read = cmd.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                InventoryTransactionModel inv = new InventoryTransactionModel
                                {
                                    PartNumber = read["partnumber"]?.ToString() ?? string.Empty,
                                    ProductionDate = read["prod_date"] != DBNull.Value ? Convert.ToDateTime(read["prod_date"]).Date : DateTime.MinValue.Date,
                                    Quantity = Convert.ToInt32(read["quantity"]),
                                    Box = Convert.ToInt32(read["total_box"]),
                                    Customer = read["customer"]?.ToString() ?? string.Empty,
                                    ProductionVersion = read["prod_ver"]?.ToString() ?? string.Empty,
                                    Status = read["PickOrder"]?.ToString() ?? string.Empty
                                };
                                Items.Add(inv);
                            }
                        }
                    }
                }
            }catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }

            return Items;
        }
        public int GetNextShipmentId()
        {
            int result = 0;
            try
            {
                using(SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    
                    using (SqlCommand cmd = new SqlCommand("GetNextShipmentId", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        result =  (int)(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }

            return result;
        }

        public string GetPartname(string partnumber)
        {
            string partName = string.Empty;
            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    string sql = "SELECT DISTINCT partname FROM product WHERE partnumber = @partnumber";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@partnumber", SqlDbType.NVarChar).Value = partnumber;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                partName = reader["partname"]?.ToString() ?? string.Empty;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }
            return partName;
        }

        public List<OrdersSummaryExtend> GetWarehouseReturn(string transaction_id)
        {
            List<OrdersSummaryExtend> Items = new List<OrdersSummaryExtend>();
            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    string sql = @"SELECT transaction_id, customer, storage_location, ToStorageLocation, remarks, 
                                   partnumber, CAST(prod_date AS DATE) AS prod_date, SUM(quantity) AS TotalQuantity, 
                                   COUNT(partnumber) AS TotalBox FROM Return_table 
                                   where transaction_id = @id GROUP BY partnumber, CAST(prod_date AS DATE), transaction_id, customer, storage_location, ToStorageLocation, remarks";
                    using(SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = transaction_id;
                        
                        using (SqlDataReader read = cmd.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                OrdersSummaryExtend inv = new OrdersSummaryExtend
                                {
                                    TransactionId = read["transaction_id"]?.ToString() ?? string.Empty,
                                    Partnumber = read["partnumber"]?.ToString() ?? string.Empty,
                                    ProductionDate = read["prod_date"] != DBNull.Value ? Convert.ToDateTime(read["prod_date"]).Date : DateTime.MinValue.Date,
                                    Quantity = Convert.ToInt32(read["TotalQuantity"]),
                                    Customer = read["customer"]?.ToString() ?? string.Empty,
                                    Box = Convert.ToInt32(read["TotalBox"]),
                                    From = read["storage_location"]?.ToString() ?? string.Empty,
                                    To = read["ToStorageLocation"]?.ToString() ?? string.Empty,
                                    Remarks = read["remarks"]?.ToString() ?? string.Empty
                                };
                                Items.Add(inv);
                            }
                        }
                    }
                }
            }catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }
            return Items;
        }

        public List<OrdersSummary> GetPackinglist(string shipmenID)
        {
            List<OrdersSummary> Items = new List<OrdersSummary>();
            try
            {
                string sql = "SELECT control_number, customer_id, partnumber, CAST(prod_date AS DATE) AS prod_date, SUM(quantity) AS TotalQuantity, COUNT(partnumber) AS TotalBox FROM transaction_history where control_number = @id GROUP BY partnumber, CAST(prod_date AS DATE), control_number, customer_id";
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    using(SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = shipmenID;
                        conn.Open();
                        using (SqlDataReader read = cmd.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                OrdersSummary inv = new OrdersSummary
                                {
                                    TransactionId = read["control_number"]?.ToString() ?? string.Empty,
                                    Partnumber = read["partnumber"]?.ToString() ?? string.Empty,
                                    ProductionDate = read["prod_date"] != DBNull.Value ? Convert.ToDateTime(read["prod_date"]).Date : DateTime.MinValue.Date,
                                    Quantity = Convert.ToInt32(read["TotalQuantity"]),
                                    Customer = read["customer_id"]?.ToString() ?? string.Empty,
                                    Box = Convert.ToInt32(read["TotalBox"])
                                };
                                Items.Add(inv);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }
            return Items;
        }
        public List<InventoryTransactionModel> GetFilteredData(string partnumber, int page, int size)
        {
            List<InventoryTransactionModel> items = new List<InventoryTransactionModel> ();

            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    string query = @"SELECT partnumber,customer,prod_date,prod_ver,location,quantity,total_box,storage_location,updated_date,movement_classification 
                                    FROM actual_inventory
                                    WHERE 1=1";

                    if (!string.IsNullOrEmpty(partnumber))
                        query += " AND partnumber LIKE '%' + @partnumber + '%'";

                        query += " GROUP BY partnumber, prod_ver, customer,quantity, total_box, location, prod_date, storage_location, movement_classification, updated_date";
                        query += " HAVING SUM(quantity) > 0";
                        query += " ORDER BY partnumber ASC OFFSET (@offset - 1) * @size ROWS FETCH NEXT @size ROWS ONLY";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(partnumber))
                            cmd.Parameters.Add("@partnumber", SqlDbType.NVarChar).Value = partnumber;
                        
                            cmd.Parameters.Add("@offset", SqlDbType.Int).Value = page;
                            cmd.Parameters.Add("@size", SqlDbType.Int).Value = size;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                InventoryTransactionModel item = new InventoryTransactionModel
                                {
                                    //TransactionDate = reader["entry_date"] != DBNull.Value ? Convert.ToDateTime(reader["entry_date"]).Date : DateTime.MinValue.Date,
                                    PartNumber = reader["partnumber"]?.ToString() ?? string.Empty,
                                    ProductionDate = reader["prod_date"] != DBNull.Value ? Convert.ToDateTime(reader["prod_date"]).Date : DateTime.MinValue.Date,
                                    Quantity = Convert.ToInt32(reader["quantity"]),
                                    Box = Convert.ToInt32(reader["total_box"]),
                                    Customer = reader["customer"]?.ToString() ?? string.Empty,
                                    Location = reader["location"]?.ToString() ?? string.Empty,
                                    Storage_location = reader["storage_location"]?.ToString() ?? string.Empty,
                                    ProductionVersion = reader["prod_ver"]?.ToString() ?? string.Empty,
                                    Status = reader["movement_classification"]?.ToString(),
                                    Updated_date = reader["updated_date"] != DBNull.Value ? Convert.ToDateTime(reader["updated_date"]).Date : DateTime.MinValue.Date,
                                };
                                items.Add(item);
                            }
                        }
                    }
                }
            }catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }
            return items;
        }
        public int GetTotalRows(string partnumber)
        {
            int count = 0;

            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    string query = @"SELECT COUNT(*) FROM actual_inventory
                                   WHERE quantity > 0 ";
                    if (!string.IsNullOrEmpty(partnumber))
                        query += " AND partnumber LIKE '%' + @partnumber + '%' ";

                    using (SqlCommand cmd = new SqlCommand(query,conn))
                    {
                        if (!string.IsNullOrEmpty(partnumber))
                            cmd.Parameters.Add("@partnumber", SqlDbType.NVarChar).Value = partnumber;

                        count = (int)cmd.ExecuteScalar();
                    }
                }
            }catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }
            return count;
        }
        public List<chartmodel> GetCustomerOrder(int Month, int year)
        {
            List<chartmodel> data = new List<chartmodel>();
            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    string query = "SELECT customer, SUM(quantity) AS totalquantity " +
                                   " FROM Shipment_table WHERE MONTH(entry_date) = @month AND YEAR(entry_date) = @year " +
                                   " GROUP BY customer";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@month", SqlDbType.Int).Value = Month;
                        cmd.Parameters.Add("@year", SqlDbType.Int).Value = year;

                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                data.Add(new chartmodel
                                {
                                    Title = reader["customer"]?.ToString() ?? string.Empty,
                                    Value = Convert.ToInt32(reader["totalquantity"])
                                });
                            }
                            conn.Close();
                        }
                    }
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }
            return data;
        }
        public List<int> GetYear()
        {
            List<int> year = new List<int>();

            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    string sql = "SELECT DISTINCT YEAR(entry_date) AS YEAR FROM Shipment_table";
                    using (SqlCommand cmd = new SqlCommand(sql,conn))
                    {
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                year.Add(reader["YEAR"] != DBNull.Value ? Convert.ToInt32(reader["YEAR"]) : 0);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }
            return year;
        }
        public List<MonthlyInventorySummary> GetMonthlyInventory(int year)
        {
            List<MonthlyInventorySummary> list = new List<MonthlyInventorySummary>();

            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    string query = @"SELECT 
                    m.MonthNumber,
                    ISNULL(SUM(CASE WHEN t.transaction_type = 'IN' THEN t.quantity ELSE 0 END),0) AS TotalIN,
                    ISNULL(SUM(CASE WHEN t.transaction_type = 'OUT' AND control_number IS NOT NULL THEN t.quantity ELSE 0 END),0) AS TotalOUT
                    FROM (VALUES
                    (1),(2),(3),(4),(5),(6),(7),(8),(9),(10),(11),(12)
                    ) m(MonthNumber)
                    LEFT JOIN transaction_history t
                    ON MONTH(t.entry_date) = m.MonthNumber
                    AND YEAR(t.entry_date) = @year 
                    GROUP BY m.MonthNumber
                    ORDER BY m.MonthNumber";

                    using (SqlCommand cmd = new SqlCommand(query,conn))
                    {
                        cmd.Parameters.Add("@year", SqlDbType.Int).Value = year;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new MonthlyInventorySummary
                                {
                                    Month = reader["MonthNumber"] != DBNull.Value ? Convert.ToInt32(reader["MonthNumber"]) : 0,
                                    In = reader["TotalIN"] != DBNull.Value ? Convert.ToInt32(reader["TotalIN"]) : 0,
                                    Out = reader["TotalOUT"] != DBNull.Value ? Convert.ToInt32(reader["TotalOUT"]) : 0
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }
            return list;
        }
        public List<CustomerStock> GetCurrentCustomerStock()
        {
            List<CustomerStock> customer = new List<CustomerStock>();
            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    string sql = @"SELECT customer_id,  
                                   ISNULL(SUM(CASE WHEN transaction_type = 'IN' THEN quantity ELSE 0 END),0) - 
                                   ISNULL(SUM(CASE WHEN transaction_type = 'OUT' THEN quantity ELSE 0 END),0) AS TotalQuantity 
                                   FROM transaction_history GROUP BY customer_id";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                customer.Add(new CustomerStock
                                {
                                    Customer = reader["customer_id"].ToString(),
                                    Stock = Convert.ToInt32(reader["TotalQuantity"])
                                });
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }
            return customer;
        }
        public List<MonthlyShipment> GetShipment(int year)
        {
            List<MonthlyShipment> Shipment = new List<MonthlyShipment>();

            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    string query = @"SELECT 
                    m.MonthNumber,
                    ISNULL(SUM(t.quantity),0) AS TotalOUT
                    FROM (VALUES
                    (1),(2),(3),(4),(5),(6),(7),(8),(9),(10),(11),(12)
                    ) m(MonthNumber)
                    LEFT JOIN Shipment_table t
                    ON MONTH(t.entry_date) = m.MonthNumber
                    AND YEAR(t.entry_date) = @year 
                    GROUP BY m.MonthNumber
                    ORDER BY m.MonthNumber";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@year", SqlDbType.Int).Value = year;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Shipment.Add(new MonthlyShipment
                                {
                                    Month = reader["MonthNumber"] != DBNull.Value ? Convert.ToInt32(reader["MonthNumber"]) : 0,
                                    Out = reader["TotalOUT"] != DBNull.Value ? Convert.ToInt32(reader["TotalOUT"]) : 0
                                });
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }
            return Shipment;
        }

        public int GetId(string partnumber)
        {
            int id = 0;

            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    string sql = "SELECT id from product WHERE partnumber = @partnumber";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@partnumber", SqlDbType.NVarChar).Value = partnumber;

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            id = Convert.ToInt32(result);
                        }
                    }
                }
            }catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }
            return id;
        }
        public Dictionary<string, int> GetLatestId()
        {
            Dictionary<string, int> RackIDs = new Dictionary<string, int>();
            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    string query = "SELECT location, ISNULL(MAX(id),0) AS latestID FROM transaction_history GROUP BY location";
                    using(SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader read = cmd.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                string location = read["location"].ToString();
                                int id = Convert.ToInt32(read["latestID"]);
                                RackIDs[location] = id;
                            }
                        }
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }
            return RackIDs;
        }
        
        public List<SlowMovingItem> GetSlowMovingItems(string partnumber = null)
        {
            List<SlowMovingItem> items = new List<SlowMovingItem>();

            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    string sql = @"SELECT partnumber,customer, prod_date,location, total_box,quantity ,storage_location,last_out_date, movement_classification
                                   FROM actual_inventory 
                                   WHERE 1 = 1";
                    sql += " AND movement_classification = 'SLOW'";
                    if (!string.IsNullOrWhiteSpace(partnumber))
                    {
                        sql += " AND partnumber LIKE @partnumber";
                    }

                    sql += @" GROUP BY location, partnumber, customer, prod_date, storage_location, movement_classification, total_box, quantity, last_out_date    
                              HAVING
                              SUM(quantity) > 0";

                    using (SqlCommand cmd = new SqlCommand(sql,conn))
                    {
                        if (!string.IsNullOrEmpty(partnumber))
                            cmd.Parameters.Add("@partnumber", SqlDbType.NVarChar).Value = partnumber;

                        using (SqlDataReader read = cmd.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                SlowMovingItem item = new SlowMovingItem
                                {
                                    Partnumber = read["partnumber"].ToString() ?? string.Empty,
                                    Customer = read["customer"].ToString() ?? string.Empty,
                                    ProdDate = read["prod_date"] != DBNull.Value ? Convert.ToDateTime(read["prod_date"]).Date : DateTime.MinValue.Date,
                                    Box = Convert.ToInt32(read["total_box"]),
                                    Quantity = Convert.ToInt32(read["quantity"]),
                                    Location = read["location"].ToString() ?? string.Empty,
                                    Storage_location = read["storage_location"].ToString() ?? string.Empty,
                                    Classification = read["movement_classification"].ToString() ?? string.Empty,
                                    Lastmovement = read["last_out_date"] != DBNull.Value ? Convert.ToDateTime(read["last_out_date"]).Date : DateTime.MinValue.Date
                                };
                                items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }
            return items;
        }
        public void RunMovementClassification()
        {
            try
            {
                using(SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    using(SqlCommand cmd = new SqlCommand("EXEC sp_UpdateInventoryClassification", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }
        }
        public DataTable GetInventoryData()
        {
            DataTable items = new DataTable();

            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    string query = @"SELECT partnumber,customer,prod_date,prod_ver,location,quantity,total_box,storage_location,updated_date,movement_classification 
                                    FROM actual_inventory
                                    WHERE 1=1";

                    query += " GROUP BY partnumber, prod_ver, customer,quantity, total_box, location, prod_date, storage_location, movement_classification, updated_date";
                    query += " HAVING SUM(quantity) > 0";

                    using(SqlDataAdapter dt = new SqlDataAdapter(query, conn))
                    {
                        dt.Fill(items);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }
            return items;
        }

        public DataTable GetSlowMoving()
        {
            DataTable items = new DataTable();

            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    string sql = @"SELECT partnumber,customer, prod_date,location, total_box,quantity ,storage_location,last_out_date, movement_classification
                                  FROM actual_inventory 
                                  WHERE movement_classification = 'SLOW' GROUP BY location, partnumber, customer, prod_date, storage_location, movement_classification, total_box, quantity, last_out_date    
                                  HAVING
                                  SUM(quantity) > 0";
                    using(SqlDataAdapter dt = new SqlDataAdapter(sql, conn))
                    {
                        dt.Fill(items);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }
            return items;
        }

        public List<RackList> RacksList(string partnumber, string WHId)
        {
            List<RackList> rackLists = new List<RackList>();
            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    string sql = @"SELECT 
                                        location,
                                        total_box,
                                        quantity,
                                        prod_date,
                                        ROW_NUMBER() OVER (
                                            PARTITION BY partnumber, WhId
                                            ORDER BY prod_date, location
                                        ) AS PickOrder
                                    FROM actual_inventory
                                    WHERE partnumber = @partnumber
                                    AND WhId = @whid
                                    AND quantity > 0
                                    ORDER BY PickOrder";

                    using(SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@partnumber", SqlDbType.NVarChar).Value = partnumber;
                        cmd.Parameters.Add("@whid", SqlDbType.NVarChar).Value = WHId;

                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RackList List = new RackList
                                {
                                    RackId = reader["location"].ToString() ?? string.Empty,
                                    Box = Convert.ToInt32(reader["total_box"]),
                                    Quantity = Convert.ToInt32(reader["quantity"]),
                                    PickStatus = reader["PickOrder"].ToString() ?? string.Empty,
                                };
                                rackLists.Add(List);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }
            return rackLists;
        }

        public List<InventoryCardData> GetInventoryCardData(string location, string whid)
        {
            // We are returning a LIST of cards, one for each Part Number
            List<InventoryCardData> allCards = new List<InventoryCardData>();

            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();

                    // 1. ONE efficient query to get all parts and their rows for this rack
                    string sql = @"
                        SELECT storage_location, partnumber, prod_date, total_box, quantity 
                        FROM actual_inventory 
                        WHERE location = @location AND WhId = @whid
                        ORDER BY partnumber, prod_date";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@location", SqlDbType.NVarChar).Value = location;
                        cmd.Parameters.Add("@whid", SqlDbType.NVarChar).Value = whid;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Dictionary<string, InventoryCardData> cardsByPart = new Dictionary<string, InventoryCardData>();

                            while (reader.Read())
                            {
                                string currentPartNo = reader["partnumber"].ToString() ?? string.Empty;

                                // 2. If we haven't seen this Part Number yet, create a new Card for it!
                                if (!cardsByPart.ContainsKey(currentPartNo))
                                {
                                    cardsByPart[currentPartNo] = new InventoryCardData
                                    {
                                        PartNo = currentPartNo,
                                        ErpLocation = reader["storage_location"].ToString() ?? string.Empty,
                                        MonthYear = DateTime.Now.ToString("yyyy MMMM").ToUpper(),
                                        GrandTotalBoxes = 0,
                                        GrandTotalQuantity = 0,
                                        Rows = new List<InventoryRow>() // Make sure the list is initialized
                                    };
                                }

                                // 3. Grab the card we are currently building
                                InventoryCardData card = cardsByPart[currentPartNo];

                                // 4. Safely parse the row data
                                DateTime prodDate = reader["prod_date"] != DBNull.Value ? Convert.ToDateTime(reader["prod_date"]) : DateTime.MinValue;
                                int boxes = reader["total_box"] != DBNull.Value ? Convert.ToInt32(reader["total_box"]) : 0;
                                int qty = reader["quantity"] != DBNull.Value ? Convert.ToInt32(reader["quantity"]) : 0;

                                InventoryRow row = new InventoryRow
                                {
                                    LotNo = prodDate.ToString("MM-dd-yy"),
                                    Boxes = boxes,
                                    Quantity = qty / boxes
                                };

                                card.Rows.Add(row);

                                card.GrandTotalBoxes += row.Boxes;
                                card.GrandTotalQuantity += row.TotalQty;
                                int PPS = qty / boxes;
                                card.PPS = PPS;
                            }

                            allCards = cardsByPart.Values.ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }

            return allCards;
        }

        //public List<TransferSlipData> GetWHReturnData(string controlnumber)
        //{
        //    List<TransferSlipData> WHReturn = new List<TransferSlipData>();

        //    try
        //    {

        //    }catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message, "SQL Error");
        //    }
        //}
    }
}
