using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Scullery.Models.ViewModels
{
    public class BudgetExpenseAmount
    {
        [Display(Name = "Enter expense amount!")]

        public double ExpenseAmount { get; set; }
    }
}
