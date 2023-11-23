using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;

namespace SalesWebMVC.Controllers
{
    public class DepartmenstsController : Controller
    {
        public IActionResult Index()
        {
            List<Department> departments = new()
            {
                new Department { Id = 1, Name = "video game" }
            };
            return View(departments);
        }
    }
}
