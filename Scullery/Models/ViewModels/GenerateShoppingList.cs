using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Scullery.Models.ViewModels
{
    public class GenerateShoppingList
    {
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yy}")]
        public DateTime Start { get; set; } = DateTime.Now.Date;

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yy}")]
        public DateTime End { get; set; } = DateTime.Now.Date;

        public string User { get; set; }

        public string Hash { get; set; }

        public string StartString { get; set; }

        public string EndString { get; set; }
    }
}
