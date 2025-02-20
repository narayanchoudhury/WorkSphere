using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using WorkSphere.Interface;
using WorkSphere.Models;

public class ProjectAssignRepository : IProjectAssignRepository
{
    private readonly string _connectionString;

    public ProjectAssignRepository()
    {
        _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    }

    public IEnumerable<ProjectAssign> GetAllAssignments()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            return connection.Query<ProjectAssign>("SELECT * FROM ProjectAssignments ORDER BY CreatedAt DESC").ToList();
        }
    }

    public void AddOrUpdateAssignment(ProjectAssign assignment)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            if (assignment.Id == 0)
            {
                string sql = "INSERT INTO ProjectAssignments (ProjectId, AssignedUserIds, FromDateTime, ToDateTime) " +
                             "VALUES (@ProjectId, @AssignedUserIds, @FromDateTime, @ToDateTime)";
                connection.Execute(sql, assignment);
            }
            else
            {
                string sql = "UPDATE ProjectAssignments SET ProjectId = @ProjectId, AssignedUserIds = @AssignedUserIds, " +
                             "FromDateTime = @FromDateTime, ToDateTime = @ToDateTime WHERE Id = @Id";
                connection.Execute(sql, assignment);
            }
        }
    }

    public ProjectAssign GetAssignmentById(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            return connection.QueryFirstOrDefault<ProjectAssign>("SELECT * FROM ProjectAssignments WHERE Id = @Id", new { Id = id });
        }
    }

    public void DeleteAssignment(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Execute("DELETE FROM ProjectAssignments WHERE Id = @Id", new { Id = id });
        }
    }
}
