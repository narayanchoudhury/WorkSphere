using System.Web.Mvc;
using System.Collections.Generic;
using WorkSphere.Interface;
using WorkSphere.Models;
using WorkSphere.Repository;

public class ProjectController : Controller
{
    private readonly IProjectRepository _projectRepository;

    public ProjectController()
    {
        _projectRepository = new ProjectRepository();
    }

    public ActionResult Index()
    {
        return View();
    }

    public JsonResult GetAllProjects()
    {
        var projects = _projectRepository.GetAllProjects();
        return Json(projects, JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    public JsonResult AddOrUpdate(Project project)
    {
        if (project.Id == 0)
        {
            project.IsActive = true;
            _projectRepository.AddProject(project);
        }
        else
        {
            _projectRepository.UpdateProject(project);
        }
        return Json(new { success = true });
    }

    public JsonResult GetProjectById(int id)
    {
        var project = _projectRepository.GetProjectById(id);
        return Json(project, JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    public JsonResult DeleteProject(int id)
    {
        _projectRepository.DeleteProject(id);
        return Json(new { success = true });
    }

    [HttpPost]
    public JsonResult UpdateStatus(int id, bool isActive)
    {
        _projectRepository.UpdateProjectStatus(id, isActive);
        return Json(new { success = true });
    }
}
