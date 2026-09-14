using FGScanner.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace FGScanner.Util
{
    public class UserService
    {
        private readonly db_connection _connection;

        public UserService()
        {
            _connection = new db_connection();
        }

        public async Task<AuthenticationResult> AuthenticateAsync(string username, string password)
        {
            using SqlConnection connection = _connection.Getconnection();
            await connection.OpenAsync();

            const string authenticationSql = @"
                SELECT TOP (1) name, role, group_id, status
                FROM users
                WHERE user_id COLLATE Latin1_General_CS_AS = @username
                  AND password COLLATE Latin1_General_CS_AS = @password;";

            UserModel user;
            string status;

            using (SqlCommand command = new(authenticationSql, connection))
            {
                command.Parameters.Add("@username", SqlDbType.NVarChar, 255).Value = username;
                command.Parameters.Add("@password", SqlDbType.NVarChar, 255).Value = password;

                using SqlDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow);
                if (!await reader.ReadAsync())
                {
                    return AuthenticationResult.Failed("The user ID or password is incorrect.");
                }

                status = reader["status"] == DBNull.Value ? string.Empty : reader["status"].ToString();
                user = new UserModel
                {
                    Name = reader["name"] == DBNull.Value ? username : reader["name"].ToString(),
                    Role = reader["role"] == DBNull.Value ? string.Empty : reader["role"].ToString(),
                    UserGroup = reader["group_id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["group_id"])
                };
            }

            if (status.Equals("Inactive", StringComparison.OrdinalIgnoreCase) ||
                status.Equals("Disabled", StringComparison.OrdinalIgnoreCase) ||
                status.Equals("Locked", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticationResult.Failed("This account is disabled. Contact an administrator.");
            }

            if (string.IsNullOrWhiteSpace(user.Role) || user.UserGroup <= 0)
            {
                return AuthenticationResult.Failed("This account does not have a valid role or user group.");
            }

            const string activitySql = @"
                UPDATE users
                SET last_login_date = GETDATE(), last_active = GETDATE()
                WHERE user_id COLLATE Latin1_General_CS_AS = @username;";

            using (SqlCommand command = new(activitySql, connection))
            {
                command.Parameters.Add("@username", SqlDbType.NVarChar, 255).Value = username;
                await command.ExecuteNonQueryAsync();
            }

            return AuthenticationResult.Succeeded(user);
        }
    }

    public sealed class AuthenticationResult
    {
        private AuthenticationResult(bool isSuccess, UserModel user, string errorMessage)
        {
            IsSuccess = isSuccess;
            User = user;
            ErrorMessage = errorMessage;
        }

        public bool IsSuccess { get; }
        public UserModel User { get; }
        public string ErrorMessage { get; }

        public static AuthenticationResult Succeeded(UserModel user) => new(true, user, string.Empty);
        public static AuthenticationResult Failed(string message) => new(false, null, message);
    }
}
