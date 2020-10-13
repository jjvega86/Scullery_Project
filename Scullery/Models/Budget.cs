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

        public double CurrentWeekBudget { get; set; }
        public double CurrentWeekSpent { get; set; }
        public DateTime? CurrentWeekStart { get; set; }
        public DateTime? CurrentWeekEnd { get; set; }
        public double CumulativeBudget { get; set; }
        public double CumulativeSpent { get; set; }

    }
}
