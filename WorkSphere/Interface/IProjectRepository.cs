using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkSphere.Models;

namespace WorkSphere.Interface
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetAllProjects();
        Project GetProjectById(int id);
        void AddProject(Project project);
        void UpdateProject(Project project);
        void DeleteProject(int id);
        void UpdateProjectStatus(int id, bool isActive);
    }
}