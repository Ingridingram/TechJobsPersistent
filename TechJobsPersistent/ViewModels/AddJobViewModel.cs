using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechJobsPersistent.Models;

namespace TechJobsPersistent.ViewModels
{
    public class AddJobViewModel
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public List<SelectListItem> Employers = new List<SelectListItem>();

        public List<Skill> Skills { get; set; }

        public int EmployerId { get; set; }

        public AddJobViewModel(List<Employer> allEmployers, List<Skill> allSkills)
        {
            foreach (var e in allEmployers)
            {
                Employers.Add(new SelectListItem{ Value = e.Id.ToString(), Text = e.Name});
            }

            Skills = allSkills;
        }

        public AddJobViewModel() { }
    }
}
