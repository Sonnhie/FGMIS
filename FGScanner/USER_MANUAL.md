# FGIMS / FGScanner User Manual

## Purpose

FGIMS is a desktop finished-goods inventory system. It records incoming stock, BPPS activity, shipments, warehouse returns, and location transfers; it also provides inventory, rack, ledger, and document reports.

This manual is for operators and supervisors. Menu visibility and actions depend on the account assigned to you.

## Before you begin

- Use a Windows workstation that can reach the company inventory database.
- Have an active User ID and password. Contact the system administrator if you cannot sign in.
- Keep the current, approved Excel scan exports and document templates. The provided templates are in the application's `Templates` folder.
- Check the warehouse, rack/location, part number, production date/version, customer, quantity, and box count before committing a transaction.

> **Important:** Selecting a file only creates a preview. A transaction is not saved until you choose **Upload file** and confirm the prompt.

## Start and sign in

1. Start FGIMS and wait for the splash screen to finish loading.
2. Enter your **User ID** and **Password** on the Login screen.
3. Select **Login**.
4. The main window opens to the **Inventory Summary** dashboard. The signed-in user, date, and time appear in the header.

If the application reports a database connection error, verify network access and contact IT; the system cannot be used offline.

To leave the system, choose **Menu > Logout** and confirm. Use the window close button only when no upload or document generation is in progress.

## Navigation

| Main menu | Command | Use |
| --- | --- | --- |
| Inventory | Data Entry > Transfer Location | Move selected quantities between racks in the same warehouse. |
| Inventory | Data Entry > BPPS | Load and post BPPS scan data. |
| Inventory | Data Entry > Incoming | Load and post incoming finished goods. |
| Inventory | Data Entry > Outgoing | Post shipments using scan data and a DPI reference file. |
| Inventory | Data Entry > Warehouse Return | Return stock and create a return slip. |
| Inventory | Rack Viewer > Warehouse / Ecozone | Find stock and inspect rack details. |
| Inventory | Stock List | Search, export, and—when authorized—correct current stock. |
| Inventory | Slow Moving List | Review/export slow-moving items. |
| Inventory | IN & OUT Ledger | View a part's movement history and create a stock card. |
| Inventory | Master List | Search and register part master data. |
| Reports | Inventory Summary | Return to the dashboard. |
| Documents | Packing List | Review shipments, create packing lists, export, or cancel a shipment. |
| Documents | Warehouse Return Slip | Review returns, generate a slip, or cancel a return. |

Some restricted accounts cannot use Data Entry, Rack Viewer, document, or ledger functions. Disabled items indicate that the action is not available for your account.

## Dashboard and lookup tools

### Inventory Summary

The dashboard presents current stock, shipped quantity, warehouse-return quantity, slow-moving-item count, stock by customer, and monthly inventory trends. Choose a year where the dashboard provides a year selector.

### Stock List

1. Choose **Inventory > Stock List**.
2. Search by part number, then select **Search**.
3. Use **Prev Page** and **Next Page** to navigate results.
4. Select **Export to CSV** to save the displayed inventory report.
5. If an **Edit Stock** action is available, use it only for approved corrections. Enter the requested part information, quantity to deduct, and a clear reason; then confirm.

### Slow Moving List

Search by part number, customer, or production version. Review the displayed totals, use page controls as needed, and select **Export Data** to save the report.

### Rack Viewer

Choose **Rack Viewer > Warehouse** or **Ecozone**. Search for a part number, select the relevant result/rack, and review rack details and totals. The color legend identifies availability/customer categories. Use **Generate Ledger** to open the selected part's ledger.

### IN & OUT Ledger

Enter a part number, inventory date, production version, and warehouse, then select **Search**. The result shows beginning stock and movement history. Select **Export CSV** to generate the stock-card file.

## Master list

The Master List stores the product information that supports uploads and box calculations.

1. Choose **Inventory > Master List**.
2. Search by part number or browse with page controls.
3. Select **Add new item** to register a product.
4. Enter **Part number**, **Part name**, **Customer**, and numeric **PPS** (pieces per box), then select **Add Item**.

Do not add duplicate or unapproved part numbers. Product deletion is limited to specifically authorized accounts; confirm the deletion prompt only when the master-data change is approved.

## Excel upload conventions

Use Excel `.xlsx` or `.xls` files. The system reads data beginning at row 2, so row 1 should contain headings.

| Upload type | Required columns, in order |
| --- | --- |
| Incoming, BPPS, outgoing scan file, warehouse return | Column A: QR/barcode value; Column B: storage/rack location. |
| DPI file for an outgoing shipment | Column A: part number; Column B: quantity; Column C: PPS; Column D: box count. |

Use the current approved QR/scan export format. The part and location must be valid for the selected warehouse. A file that can be previewed can still be rejected at posting time if it contains an invalid location, insufficient stock, or a mismatch with the DPI data.

## Posting inventory transactions

### Incoming stock

1. Choose **Data Entry > Incoming**.
2. Select the destination **Warehouse ID**.
3. Select **Select file** and choose the scan Excel file.
4. Wait for processing to finish; review the preview grid and its part count, quantity, box count, customer, and locations.
5. Select **Upload file**, then choose **Yes** to save.
6. Resolve any invalid-location warning, correct the source file, and repeat if necessary.

Select **Clear Upload** to discard the preview before posting.

### BPPS

The BPPS workflow is the same as Incoming: select a warehouse, choose the BPPS Excel scan file, review the preview and totals, select **Upload file**, and confirm. The system validates all rack locations before it saves the BPPS transaction.

### Outgoing shipment

1. Choose **Data Entry > Outgoing**.
2. Select the warehouse.
3. Load the **DPI** file and review its part, quantity, box-count, and PPS totals.
4. Load the outgoing scan file and review the shipment preview and generated Shipment ID.
5. Select **Upload file** and confirm to post the shipment. The system checks the uploaded scan data against stock and the DPI reference.
6. After a successful post, select **Generate Packing List** to preview/print the packing document, if required.

Use the clear buttons to remove either the DPI or scan preview before posting. Do not reuse a posted shipment file unless a supervisor has instructed you to do so.

### Warehouse return

1. Choose **Data Entry > Warehouse Return**.
2. Select the warehouse, storage location to transfer to, and enter a meaningful **Remarks** value.
3. Select the return scan Excel file and review the preview and generated Return ID.
4. Select **Upload file** and confirm.
5. The system validates available rack stock using an all-or-nothing policy. If any item exceeds available stock or has invalid data, the entire upload is rejected to prevent inventory discrepancy.
6. After a successful upload, the preview grid resets automatically while the Return ID remains active. Select **Generate Return Slip** to preview and print the transfer/return document.

The application prevents reuse of an existing Return ID. Do not re-upload the same file; check inventory and transaction history before submitting any replacement upload.

### Transfer location

1. Choose **Data Entry > Transfer Location**.
2. Select the warehouse, **current location**, and **new location**.
3. Review the source-location inventory.
4. Tick **Select** for each item to move and enter the quantity for that row.
5. Select **Transfer** and verify the confirmation dialog, including total quantity and calculated boxes.

Enter quantities carefully. Transfer quantity is used to calculate boxes from the product PPS, and the transaction changes the physical rack allocation.

## Shipment and return documents

### Packing List

1. Choose **Documents > Packing List**.
2. Filter by date range and/or shipment ID, then select **Filter**.
3. Select **View Items** for a shipment to inspect its lines and totals.
4. Select **Generate Packing List** to create the document, or **Export Excel** to save report data.
5. Use **Cancel Shipment** only for an approved reversal; cancellation affects inventory history.

### Warehouse Return Slip

1. Choose **Documents > Warehouse Return Slip**.
2. Filter by start date, end date, and/or transfer destination, then select **Search**.
3. Select **View Items** to inspect the chosen return.
4. Select **Generate Slip** to create the return document.
5. Use **Cancel Return** only when the return must be reversed and approval has been obtained.

## Troubleshooting

| Message or symptom | What to do |
| --- | --- |
| Database connection failed | Check the network and contact IT or the system administrator. |
| User does not exist / cannot sign in | Re-enter credentials; ask the administrator to verify your account. |
| Please select a warehouse | Choose the warehouse before selecting the upload file. |
| Invalid location | Correct the location in the source scan file or have the rack master data verified. |
| No file to upload / No inventory uploaded | Select a supported Excel file, allow it to finish processing, and confirm that preview rows appear. |
| Stock limit / overflow warning | Do not retry blindly. Compare the requested quantity with current stock and correct or split the transaction. |
| Duplicate Return ID | Refresh the transaction screen and create a new return; do not attempt to reuse the existing control number. |
| Export failed | Check that the destination file is not open or read-only, choose a writable folder, and try again. |

## Good operating practice

- Review preview totals before every **Upload file** confirmation.
- Post one business transaction once; use document cancellation/reversal procedures rather than duplicate uploads.
- Include specific remarks on warehouse returns and stock corrections so the ledger can be audited.
- Generate or export documents immediately after posting when they are needed for dispatch or receiving records.
- Log out when leaving a shared workstation.

