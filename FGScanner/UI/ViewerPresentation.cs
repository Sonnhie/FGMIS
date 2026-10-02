using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FGScanner.UI
{
    internal static class ViewerPresentation
    {
        private const int WmSetRedraw = 0x000B;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(
            IntPtr windowHandle,
            int message,
            IntPtr parameter,
            IntPtr result);

        public static void EnableDoubleBuffering(Control root)
        {
            if (root == null)
            {
                return;
            }

            PropertyInfo doubleBuffered = typeof(Control).GetProperty(
                "DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);

            EnableDoubleBuffering(root, doubleBuffered);
        }

        private static void EnableDoubleBuffering(Control control, PropertyInfo doubleBuffered)
        {
            doubleBuffered?.SetValue(control, true);
            foreach (Control child in control.Controls)
            {
                EnableDoubleBuffering(child, doubleBuffered);
            }
        }

        public static void PerformAtomicLayout(Control control, Action layoutAction)
        {
            if (control == null)
            {
                layoutAction?.Invoke();
                return;
            }

            bool redrawSuspended = control.IsHandleCreated;
            control.SuspendLayout();
            if (redrawSuspended)
            {
                SendMessage(control.Handle, WmSetRedraw, IntPtr.Zero, IntPtr.Zero);
            }

            try
            {
                layoutAction?.Invoke();
            }
            finally
            {
                control.ResumeLayout(true);
                if (redrawSuspended && !control.IsDisposed)
                {
                    SendMessage(control.Handle, WmSetRedraw, new IntPtr(1), IntPtr.Zero);
                    control.Invalidate(true);
                    control.Update();
                }
            }
        }

        public static Label ConfigureCollapsibleDetails(
            UserControl owner,
            Panel header,
            Panel details,
            Label title,
            Label searchLabel,
            TextBox searchBox,
            Action onClose)
        {
            details.Visible = false;

            var closeButton = new Button
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.FromArgb(220, 53, 69),
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(details.ClientSize.Width - 39, 8),
                Name = "CloseDetailsButton",
                Size = new Size(30, 28),
                TabStop = false,
                Text = "×",
                UseVisualStyleBackColor = false
            };
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.Click += (_, _) =>
            {
                PerformAtomicLayout(owner, () =>
                {
                    details.Visible = false;
                    onClose?.Invoke();
                    owner.PerformLayout();
                });
            };
            details.Controls.Add(closeButton);
            closeButton.BringToFront();

            var searchHost = new Panel
            {
                Dock = DockStyle.Right,
                Name = "RackSearchPanel",
                Width = 430
            };

            searchLabel.Parent = searchHost;
            searchLabel.AutoSize = true;
            searchLabel.Location = new Point(8, 13);
            searchLabel.Text = "Find part number:";

            searchBox.Parent = searchHost;
            searchBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            searchBox.Location = new Point(132, 10);
            searchBox.Size = new Size(170, 23);

            var searchStatus = new Label
            {
                AutoEllipsis = true,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 184),
                Location = new Point(310, 12),
                Name = "RackSearchStatusLabel",
                Size = new Size(112, 20),
                Text = ""
            };
            searchHost.Controls.Add(searchStatus);
            header.Controls.Add(searchHost);
            searchHost.BringToFront();

            title.Dock = DockStyle.Fill;
            title.Location = Point.Empty;
            title.TextAlign = ContentAlignment.MiddleCenter;

            void ResizeDetails()
            {
                int preferredWidth = Math.Clamp((int)(owner.ClientSize.Width * 0.34), 420, 620);
                details.Width = Math.Min(preferredWidth, Math.Max(320, owner.ClientSize.Width - 320));
                searchHost.Width = owner.ClientSize.Width < 900 ? 380 : 430;
            }

            owner.SizeChanged += (_, _) => ResizeDetails();
            ResizeDetails();
            return searchStatus;
        }

        public static void ShowDetails(Control owner, Panel details)
        {
            PerformAtomicLayout(owner, () =>
            {
                if (!details.Visible)
                {
                    details.Visible = true;
                }

                details.BringToFront();
                owner.PerformLayout();
            });
        }
    }
}
