using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using WorkSphere.Interface;
using WorkSphere.Models;

namespace WorkSphere.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly string _connectionString;

        public ProjectRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public IEnumerable<Project> GetAllProjects()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Project>("SELECT * FROM Projects ORDER BY CreatedAt DESC").ToList();
            }
        }

        public Project GetProjectById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QuerySingleOrDefault<Project>("SELECT * FROM Projects WHERE Id = @Id", new { Id = id });
            }
        }

        public void AddProject(Project project)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("INSERT INTO Projects (ProjectName, IsActive) VALUES (@ProjectName, @IsActive)", project);
            }
        }

        public void UpdateProject(Project project)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("UPDATE Projects SET ProjectName = @ProjectName WHERE Id = @Id", project);
            }
        }

        public void DeleteProject(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("DELETE FROM Projects WHERE Id = @Id", new { Id = id });
            }
        }

        public void UpdateProjectStatus(int id, bool isActive)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("UPDATE Projects SET IsActive = @IsActive WHERE Id = @Id", new { Id = id, IsActive = isActive });
            }
        }
    }
}
