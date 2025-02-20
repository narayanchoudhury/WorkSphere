using System;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Configuration;
using BCrypt.Net;
using WorkSphere.Models;
using System.Collections.Generic;

namespace WorkSphere.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public User GetUserByUsername(string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<User>("SELECT * FROM Users WHERE Username = @Username", new { Username = username }).FirstOrDefault();
            }
        }

        public bool ValidateUser(string username, string password)
        {
            var user = GetUserByUsername(username);
            if (user == null) return false;
            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }

        public void RegisterUser(string username, string password, string fullName, string role)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            DateTime createdAt = DateTime.UtcNow;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "INSERT INTO Users (Username, PasswordHash, FullName, Role, CreatedAt) VALUES (@Username, @PasswordHash, @FullName, @Role, @CreatedAt)",
                    new { Username = username, PasswordHash = passwordHash, FullName = fullName, Role = role, CreatedAt = createdAt }
                );
            }
        }

        public void UpdatePassword(string username, string newPasswordHash)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "UPDATE Users SET PasswordHash = @PasswordHash WHERE Username = @Username",
                    new { PasswordHash = newPasswordHash, Username = username }
                );
            }
        }
        public IEnumerable<User> GetAllUsers()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<User>("SELECT Id, FullName FROM Users").ToList();
            }
        }

    }
}
