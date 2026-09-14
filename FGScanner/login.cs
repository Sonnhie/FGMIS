using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using FGScanner.Util;
using System.Reflection;
using FGScanner.UI;

namespace FGScanner
{
    public partial class login : Form
    {
        private readonly UserService _userService = new();

        public login()
        {
            InitializeComponent();
            EnterpriseTheme.Apply(this);
            AcceptButton = BtnSignIn;
            TxtUserId.PlaceholderText = "Enter your user ID";
            TxtPassword.PlaceholderText = "Enter your password";
            TxtPassword.UseSystemPasswordChar = true;
            SessionManager.SessionEnded += SessionManager_SessionEnded;
        }

        private async Task LoginAsync(string username, string password)
        {
            BtnSignIn.Enabled = false;
            BtnSignIn.Text = "Signing in...";

            try
            {
                AuthenticationResult result = await _userService.AuthenticateAsync(username, password);
                if (!result.IsSuccess)
                {
                    TxtPassword.Clear();
                    TxtPassword.Focus();
                    MessageBox.Show(result.ErrorMessage, "Sign-in failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MainForm main = new(result.User.Name, result.User.UserGroup);
                Hide();
                main.Show();
                SessionManager.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to sign in. Please check the database connection and try again.\n\n{ex.Message}",
                    "Sign-in error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                BtnSignIn.Enabled = true;
                BtnSignIn.Text = "Sign in";
            }
        }

        private void SessionManager_SessionEnded(object sender, SessionEndedEventArgs e)
        {
            foreach (Form openForm in Application.OpenForms.Cast<Form>().Where(form => form != this).ToArray())
            {
                openForm.Close();
            }

            TxtPassword.Clear();
            Show();
            BringToFront();
            Activate();
            TxtUserId.Focus();

            if (e.Reason == SessionEndReason.IdleTimeout)
            {
                MessageBox.Show(
                    this,
                    "You were signed out after 30 minutes without keyboard or mouse activity.",
                    "Session expired",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void login_Load(object sender, EventArgs e)
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            version_lbl.Text = $"Version: {version}";
        }

        private async void BtnSignIn_Click_1(object sender, EventArgs e)
        {
            string username = TxtUserId.Text.Trim();
            string password = TxtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username and password required!");
                return;
            }

            await LoginAsync(username, password);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            SessionManager.Stop();
            SessionManager.SessionEnded -= SessionManager_SessionEnded;
            base.OnFormClosed(e);
        }
    }
}
