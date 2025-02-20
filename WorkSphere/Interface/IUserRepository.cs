using System.Collections.Generic;
using WorkSphere.Models;

namespace WorkSphere.Repositories
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
        bool ValidateUser(string username, string password);
        void RegisterUser(string username, string password, string fullName, string role);
        void UpdatePassword(string username, string newPasswordHash);
        IEnumerable<User> GetAllUsers(); // Add this method

    }
}
