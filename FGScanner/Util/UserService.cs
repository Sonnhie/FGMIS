using FGScanner.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FGScanner.Util
{
    public class UserService
    {
        private readonly db_connection _Connection;

        public UserService()
        {
            _Connection = new db_connection();
        }

        public bool CheckIfExist(string username)
        {
            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    string sql = "SELECT COUNT(*) FROM users WHERE user_id = @username";
                    using(SqlCommand cmd = new SqlCommand(sql,conn))
                    {
                       cmd.Parameters.Add("@username", System.Data.SqlDbType.NVarChar).Value = username;
                       
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
                return false;
            }
        }

        public string VerifyUser(string username, string password)
        {
            string userid = null;

            try
            {
                using (SqlConnection conn = _Connection.Getconnection())
                {
                    conn.Open();
                    string sql = @"SELECT name FROM users 
                                   WHERE user_id COLLATE Latin1_General_CS_AS = @username and password COLLATE Latin1_General_CS_AS = @password";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar).Value = username;
                        command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar).Value = password;
                        
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userid = reader["name"].ToString();
                            }
                        }
                    }
                }
            }catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SQL Error");
            }
            return userid;
        }

    }
}
