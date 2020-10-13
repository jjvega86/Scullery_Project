﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Scullery.Models
{
    public class Pod
    {
        [Key]
        public int PodId { get; set; }

        [ForeignKey("UserName")]
        public string FounderUserName { get; set; }
        public Planner Planner { get; set; }

        public string PodName { get; set; }
        public string PodPassword { get; set; }

    }
}
