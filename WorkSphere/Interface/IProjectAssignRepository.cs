using System.Collections.Generic;
using WorkSphere.Models;

namespace WorkSphere.Interface
{
    public interface IProjectAssignRepository
    {
        IEnumerable<ProjectAssign> GetAllAssignments();
        void AddOrUpdateAssignment(ProjectAssign assignment);
        ProjectAssign GetAssignmentById(int id);
        void DeleteAssignment(int id);
    }
}
