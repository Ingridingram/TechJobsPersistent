using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        [HttpGet("/Add")]
        public IActionResult AddJob()
        {
            var allEmployers = context.Employers.ToList();
            var allSkills = context.Skills.ToList();
            AddJobViewModel vm = new AddJobViewModel(allEmployers, allSkills);

            return View(vm);
        }

        public IActionResult ProcessAddJobForm(AddJobViewModel vm, List<String> selectedSkills) 
        {
            if (ModelState.IsValid)
            {
                var model = new Job();
                model.Name = vm.Name;
                model.EmployerId = vm.EmployerId;
                model.JobSkills = new List<JobSkill>();
                foreach (var skill in selectedSkills)
                {
                    var js = new JobSkill();
                    js.Job = model;
                    js.SkillId = int.Parse(skill);
                    model.JobSkills.Add(js);
                }
                this.context.Add(model);
                this.context.SaveChanges();
                return Redirect("/");
            }
            return BadRequest();
        }

        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
