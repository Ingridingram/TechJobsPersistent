using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechJobsPersistent.Data;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobsPersistent.Controllers
{
    public class EmployerController : Controller
    {

        private JobDbContext jobDbContext;

        public EmployerController(JobDbContext jobDbContext) 
        { 
            this.jobDbContext = jobDbContext; 
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var result = this.jobDbContext.Employers.ToList();
            return View(result);
        }

        public IActionResult Add()
        {
            var vm = new AddEmployerViewModel();
            return View(vm);
        }

        [HttpPost]
        public IActionResult ProcessAddEmployerForm(AddEmployerViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var model = new Employer();
                model.Name = vm.Name;
                model.Location = vm.Location;
                this.jobDbContext.Add(model);
                this.jobDbContext.SaveChanges();
                return Redirect("./Add");
            }
            return BadRequest();
        }

        public IActionResult About(int id)
        {
            var employer = jobDbContext.Employers.Find(id);
            return View(employer);
        }
    }
}
