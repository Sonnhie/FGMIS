using FGScanner.DTOs;
using FGScanner.Models;
using FGScanner.Services;
using FGScanner.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FGScanner.Forms.Reports
{
    public sealed class MonthEndCheckTagForm : Form
    {
        private readonly string _userId;
        private readonly InventoryDbContext _dbContext;
        private readonly MonthEndCheckTagService _tagService;
        private readonly CheckTagPrintService _printService = new();
        private readonly InventoryCheckTagExcelService _excelService = new();
        private readonly PrintDocument _printDocument = new();
        private readonly DateTimePicker _cutoffDate = new();
        private readonly ComboBox _warehouse = new();
        private readonly ComboBox _generationMode = new();
        private readonly TextBox _rackFilter = new();
        private readonly TextBox _partNumberFilter = new();
        private readonly CheckBox _prefillSystemCount = new();
        private readonly Button _previewButton = new();
        private readonly Button _historyButton = new();
        private readonly Button _exportButton = new();
        private readonly Label _statusLabel = new();
        private List<InventoryCheckTagData> _tags = new();

        public MonthEndCheckTagForm(string userId)
        {
            _userId = userId ?? string.Empty;
            _dbContext = new InventoryDbContext();
            _tagService = new MonthEndCheckTagService(_dbContext);

            InitializeUi();
            EnterpriseTheme.Apply(this);

            _printDocument.BeginPrint += (_, _) => _printService.Reset();
            _printDocument.PrintPage += PrintDocument_PrintPage;
            _printDocument.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
            _printDocument.DefaultPageSettings.Margins = new Margins(45, 45, 40, 40);
        }

        private void InitializeUi()
        {
            Text = "Month-End Inventory Check Tags";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(640, 390);
            Font = new Font("Segoe UI", 9F);

            var title = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Location = new Point(24, 18),
                Size = new Size(590, 34),
                Text = "Generate Month-End Inventory Check Tags"
            };

            var explanation = new Label
            {
                AutoSize = false,
                ForeColor = Color.DimGray,
                Location = new Point(26, 56),
                Size = new Size(585, 40),
                Text = "Read-only report. Six check tags print on each A4 sheet. Balances use transaction history through the selected cutoff date; no inventory records are changed."
            };

            AddFieldLabel("Cutoff date", 28, 108);
            DateTime firstDayOfCurrentMonth = new(DateTime.Today.Year, DateTime.Today.Month, 1);
            _cutoffDate.Format = DateTimePickerFormat.Custom;
            _cutoffDate.CustomFormat = "MMMM dd, yyyy";
            _cutoffDate.Location = new Point(145, 104);
            _cutoffDate.Size = new Size(185, 27);
            _cutoffDate.Value = firstDayOfCurrentMonth.AddDays(-1);

            AddFieldLabel("Warehouse", 350, 108);
            _warehouse.DropDownStyle = ComboBoxStyle.DropDownList;
            _warehouse.Items.AddRange(new object[] { "WH1", "WH2" });
            _warehouse.Location = new Point(455, 104);
            _warehouse.Size = new Size(150, 28);
            _warehouse.SelectedIndex = 0;

            AddFieldLabel("Generation", 28, 151);
            _generationMode.DropDownStyle = ComboBoxStyle.DropDownList;
            _generationMode.Items.AddRange(new object[]
            {
                "One-time (all racks)",
                "Per rack"
            });
            _generationMode.Location = new Point(145, 147);
            _generationMode.Size = new Size(185, 28);
            _generationMode.SelectedIndexChanged += GenerationMode_SelectedIndexChanged;
            _generationMode.SelectedIndex = 0;

            AddFieldLabel("Rack location", 350, 151);
            _rackFilter.Location = new Point(455, 147);
            _rackFilter.Size = new Size(150, 27);
            _rackFilter.PlaceholderText = "Select Per rack";
            _rackFilter.CharacterCasing = CharacterCasing.Upper;
            _rackFilter.Enabled = false;

            AddFieldLabel("Part number", 28, 194);
            _partNumberFilter.Location = new Point(145, 190);
            _partNumberFilter.Size = new Size(185, 27);
            _partNumberFilter.PlaceholderText = "Optional print filter";
            _partNumberFilter.CharacterCasing = CharacterCasing.Upper;

            _prefillSystemCount.AutoSize = true;
            _prefillSystemCount.Location = new Point(350, 194);
            _prefillSystemCount.Text = "Prefill system quantity and boxes";
            _prefillSystemCount.Checked = false;

            var blindCountNote = new Label
            {
                AutoSize = false,
                ForeColor = Color.FromArgb(150, 85, 0),
                Location = new Point(145, 228),
                Size = new Size(455, 38),
                Text = "Leave unchecked for blind physical counting. Product and location information will still be printed."
            };

            _previewButton.Location = new Point(455, 288);
            _previewButton.Size = new Size(150, 38);
            _previewButton.Text = "Generate Preview";
            _previewButton.UseVisualStyleBackColor = true;
            _previewButton.Click += PreviewButton_Click;

            _statusLabel.AutoEllipsis = true;
            _statusLabel.Location = new Point(28, 297);
            _statusLabel.Size = new Size(405, 26);
            _statusLabel.TextAlign = ContentAlignment.MiddleLeft;

            _historyButton.Location = new Point(28, 344);
            _historyButton.Size = new Size(130, 30);
            _historyButton.Text = "Saved Batches";
            _historyButton.UseVisualStyleBackColor = true;
            _historyButton.Click += HistoryButton_Click;

            _exportButton.Location = new Point(315, 344);
            _exportButton.Size = new Size(130, 30);
            _exportButton.Text = "Export Excel";
            _exportButton.UseVisualStyleBackColor = true;
            _exportButton.Enabled = false;
            _exportButton.Click += ExportButton_Click;

            var closeButton = new Button
            {
                DialogResult = DialogResult.Cancel,
                Location = new Point(455, 344),
                Size = new Size(150, 30),
                Text = "Close",
                UseVisualStyleBackColor = true
            };

            Controls.Add(title);
            Controls.Add(explanation);
            Controls.Add(_cutoffDate);
            Controls.Add(_warehouse);
            Controls.Add(_generationMode);
            Controls.Add(_rackFilter);
            Controls.Add(_partNumberFilter);
            Controls.Add(_prefillSystemCount);
            Controls.Add(blindCountNote);
            Controls.Add(_previewButton);
            Controls.Add(_statusLabel);
            Controls.Add(_historyButton);
            Controls.Add(_exportButton);
            Controls.Add(closeButton);
            CancelButton = closeButton;
        }

        private void AddFieldLabel(string text, int x, int y)
        {
            Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(x, y),
                Text = text
            });
        }

        private async void PreviewButton_Click(object sender, EventArgs e)
        {
            await GenerateAndPreviewAsync();
        }

        private void GenerationMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool perRack = _generationMode.SelectedIndex == 1;
            _rackFilter.Enabled = perRack;
            _rackFilter.PlaceholderText = perRack ? "Required" : "Select Per rack";
            if (!perRack)
            {
                _rackFilter.Clear();
            }
        }

        private async Task GenerateAndPreviewAsync()
        {
            _previewButton.Enabled = false;
            _exportButton.Enabled = false;
            _statusLabel.ForeColor = Color.DimGray;
            _statusLabel.Text = "Calculating month-end balances...";

            try
            {
                bool perRack = _generationMode.SelectedIndex == 1;
                if (perRack && string.IsNullOrWhiteSpace(_rackFilter.Text))
                {
                    _statusLabel.ForeColor = Color.DarkOrange;
                    _statusLabel.Text = "Enter the exact rack location for Per rack generation.";
                    _rackFilter.Focus();
                    return;
                }

                DateOnly cutoff = DateOnly.FromDateTime(_cutoffDate.Value.Date);
                string rack = perRack ? _rackFilter.Text : string.Empty;
                InventoryCheckTagBatchData batch;
                var existing = await _tagService.FindLatestBatchAsync(
                    cutoff,
                    _warehouse.Text,
                    perRack,
                    rack);

                if (existing != null)
                {
                    DialogResult choice = MessageBox.Show(
                        this,
                        $"A finalized batch already exists for this scope.\n\n" +
                        $"Batch: {existing.BatchCode}\n" +
                        $"Revision: {existing.Revision}\n" +
                        $"Generated: {existing.CreatedAt:g} by {existing.CreatedBy}\n\n" +
                        "Yes = reprint the saved snapshot\n" +
                        "No = capture current stock as a new revision\n" +
                        "Cancel = do nothing",
                        "Existing Check-Tag Batch",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);

                    if (choice == DialogResult.Cancel)
                    {
                        _statusLabel.Text = "Generation cancelled.";
                        return;
                    }

                    batch = choice == DialogResult.Yes
                        ? await _tagService.LoadBatchAsync(existing.BatchId)
                        : await _tagService.CreateSnapshotAsync(
                            cutoff,
                            _warehouse.Text,
                            perRack,
                            rack,
                            _prefillSystemCount.Checked,
                            _userId);
                }
                else
                {
                    batch = await _tagService.CreateSnapshotAsync(
                        cutoff,
                        _warehouse.Text,
                        perRack,
                        rack,
                        _prefillSystemCount.Checked,
                        _userId);
                }

                _tags = ApplyPartNumberPrintFilter(batch.Tags);

                if (_tags.Count == 0)
                {
                    _statusLabel.ForeColor = Color.DarkOrange;
                    _statusLabel.Text = "The saved batch has no tags matching the optional part-number print filter.";
                    return;
                }

                _statusLabel.ForeColor = Color.FromArgb(0, 110, 70);
                _statusLabel.Text =
                    $"Batch {batch.BatchCode}: showing {_tags.Count:N0} of {batch.Tags.Count:N0} saved tags.";
                _exportButton.Enabled = true;
                ShowPrintPreview();
            }
            catch (Exception ex)
            {
                _statusLabel.ForeColor = Color.Firebrick;
                _statusLabel.Text = "Generation failed.";
                MessageBox.Show(
                    this,
                    $"Unable to generate check tags.\n\n{ex.Message}",
                    "Check Tag Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                _previewButton.Enabled = true;
            }
        }

        private async void HistoryButton_Click(object sender, EventArgs e)
        {
            _historyButton.Enabled = false;
            try
            {
                using var history = new MonthEndCheckTagHistoryForm(_tagService);
                if (history.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                InventoryCheckTagBatchData batch =
                    await _tagService.LoadBatchAsync(history.SelectedBatchId);
                _tags = batch.Tags;
                _statusLabel.ForeColor = Color.FromArgb(0, 110, 70);
                _statusLabel.Text =
                    $"Reprinting {batch.BatchCode}, revision {batch.Revision}, {_tags.Count:N0} tags.";
                _exportButton.Enabled = true;
                ShowPrintPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    $"Unable to open the saved batch.\n\n{ex.Message}",
                    "Batch History",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                _historyButton.Enabled = true;
            }
        }

        private async void ExportButton_Click(object sender, EventArgs e)
        {
            if (_tags.Count == 0)
            {
                MessageBox.Show(
                    this,
                    "Generate or load a saved check-tag batch before exporting.",
                    "Excel Export",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            string batchCode = _tags[0].BatchCode;
            string safeBatchCode = string.Concat(
                batchCode.Select(character =>
                    Path.GetInvalidFileNameChars().Contains(character) ? '_' : character));
            if (string.IsNullOrWhiteSpace(safeBatchCode))
            {
                safeBatchCode = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            }

            using var save = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                Title = "Export Inventory Count Tags",
                DefaultExt = "xlsx",
                AddExtension = true,
                FileName = $"InventoryCheckTags_{safeBatchCode}.xlsx"
            };

            if (save.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            _exportButton.Enabled = false;
            _statusLabel.ForeColor = Color.DimGray;
            _statusLabel.Text = "Exporting inventory count tags to Excel...";
            try
            {
                var result = await _excelService.ExportAsync(_tags, save.FileName);
                _statusLabel.ForeColor = result.isSuccess
                    ? Color.FromArgb(0, 110, 70)
                    : Color.Firebrick;
                _statusLabel.Text = result.Message;
                MessageBox.Show(
                    this,
                    result.Message,
                    result.isSuccess ? "Excel Export Complete" : "Excel Export Failed",
                    MessageBoxButtons.OK,
                    result.isSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            finally
            {
                _exportButton.Enabled = _tags.Count > 0;
            }
        }

        private List<InventoryCheckTagData> ApplyPartNumberPrintFilter(
            IEnumerable<InventoryCheckTagData> tags)
        {
            string partNumber = _partNumberFilter.Text.Trim();
            return tags
                .Where(tag => string.IsNullOrWhiteSpace(partNumber) ||
                              tag.PartNumber.Contains(partNumber, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        private void ShowPrintPreview()
        {
            using var preview = new PrintPreviewDialog
            {
                Document = _printDocument,
                Width = 1100,
                Height = 850,
                StartPosition = FormStartPosition.CenterParent
            };
            preview.PrintPreviewControl.Zoom = 0.75;
            preview.ShowDialog(this);
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            _printService.PrintPage(_tags, e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _printDocument.Dispose();
                _dbContext.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
