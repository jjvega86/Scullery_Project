using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Scullery.Models
{
    public class Planner
    {
        [Key]
        public int PlannerId { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string SpoonacularUserName { get; set; }
        public string UserHash { get; set; }
        public int PodId { get; set; }

        [NotMapped]
        [BindProperty]
        public int PodExists { get; set; }

        [NotMapped]
        public string PodPassword { get; set; }
        [NotMapped]
        public string PodName { get; set; }

       

        
        

    }
}
