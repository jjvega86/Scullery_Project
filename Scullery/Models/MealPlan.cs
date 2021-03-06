﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Scullery.Models
{
    public class MealPlan
    {
        [Key]
        public int MealPlanId { get; set; }

        [ForeignKey("PodId")]
        public int PodId { get; set; }
        public Pod Pod { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yy}")]

        public DateTime? StartDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yy}")]

        public DateTime? EndDate { get; set; }

        
    }
}
