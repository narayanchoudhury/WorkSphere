using System;
using System.Web.Mvc;
using WorkSphere.Repository;
using WorkSphere.Interface;
using WorkSphere.Models;
using System.Collections.Generic;
using WorkSphere.Repositories;

public class ProjectAssignController : Controller
{
    private readonly IProjectAssignRepository _projectAssignRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;

    public ProjectAssignController()
    {
        _projectAssignRepository = new ProjectAssignRepository();
        _projectRepository = new ProjectRepository();
        _userRepository = new UserRepository();
    }

    public ActionResult Index()
    {
        ViewBag.Projects = _projectRepository.GetAllProjects();
        ViewBag.Users = _userRepository.GetAllUsers();
        return View();
    }

    [HttpPost]
    public JsonResult AddOrUpdate(ProjectAssign assignment)
    {
        _projectAssignRepository.AddOrUpdateAssignment(assignment);
        return Json(new { success = true });
    }

    public JsonResult GetAssignmentById(int id)
    {
        var assignment = _projectAssignRepository.GetAssignmentById(id);
        return Json(assignment, JsonRequestBehavior.AllowGet);
    }
    public JsonResult GetAllAssignments()
    {
        var assignments = _projectAssignRepository.GetAllAssignments();
        return Json(assignments, JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    public JsonResult DeleteAssignment(int id)
    {
        _projectAssignRepository.DeleteAssignment(id);
        return Json(new { success = true });
    }
}
