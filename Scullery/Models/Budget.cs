using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Scullery.Models
{
    public class Budget
    {
        [Key]
        public int BudgetId { get; set; }
        
        [ForeignKey("PodId")]
        public int PodId { get; set; }
        public Pod Pod { get; set; }

        [Display(Name = "Enter your budget amount!")]
        public double CurrentWeekBudget { get; set; }
        public double CurrentWeekSpent { get; set; }

        [Display(Name = "When do you want your budget to start?")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public DateTime? CurrentWeekStart { get; set; } = DateTime.Today;

        [Display(Name = "When will your budget end?")]

        public DateTime? CurrentWeekEnd { get; set; } = DateTime.Today;

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public double CumulativeBudget { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public double CumulativeSpent { get; set; }

       

    }
}
