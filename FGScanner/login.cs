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

namespace FGScanner
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void Login(string username, string password)
        {
            try
            {
                var Service = new UserService();

                bool CheckUser = Service.CheckIfExist(username);

                if (!CheckUser)
                {
                    MessageBox.Show("User does not exist on the database!", "Login error");
                    return;
                }

                string userid = Service.VerifyUser(username, password);

                if (string.IsNullOrEmpty(userid))
                {
                    MessageBox.Show("Invalid Credentials", "Login error");
                    return;
                }

                MainForm main = new MainForm(userid);
                this.Hide();
                main.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void BtnSignIn_Click(object sender, EventArgs e)
        {
            string username = TxtUserId.Text;
            string password = TxtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username and password required!");
                return;
            }

            Login(username, password);
        }
    }
}
