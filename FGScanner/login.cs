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
//using FGScanner.Util;
using System.Reflection;
using FGScanner.Services.Interfaces;
using FGScanner.Services.Classes;
using FGScanner.Models;
using FGScanner.DTOs;

namespace FGScanner
{
    public partial class login : Form
    {
        private readonly IAuthInterface _authservice;  
        //private readonly InventoryDbContext _context;
        private readonly InventoryDbDevContext _context;
        public login()
        {
            InitializeComponent();
            _context = new InventoryDbDevContext();

            _authservice = new AuthenticationServices(_context);
        }

        private async Task Login(UserInputDto inputDto)
        {
            try
            {

                var result = await _authservice.AuthenticateUser(inputDto);
                if (result.Item1.Success)
                {
                    MessageBox.Show(result.Item1.Message, "Login successfull", MessageBoxButtons.OK);
                    MainForm m = new MainForm(result.Item2.UserId, result.Item2.GroupId);
                    this.Hide();
                    m.Show();
                }
                else
                {
                    MessageBox.Show(result.Item1.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
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
            var UserInput = new UserInputDto
            {
                Username = TxtUserId.Text,
                Password = TxtPassword.Text
            };

            if (UserInput == null)
            {
                MessageBox.Show("Username and password required!");
                return;
            }

            await Login(UserInput);
        }
    }
}
