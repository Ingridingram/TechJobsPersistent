using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechJobsPersistent.ViewModels
{
    public class AddEmployerViewModel
    {
        [Required (ErrorMessage = "Name it")]
        public string Name { get; set; }

        [Required (ErrorMessage = "Where's it at?")]
        public string Location { get; set; }
    }
}
