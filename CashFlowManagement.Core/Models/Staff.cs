namespace CashFlowManagement.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Staff
    {
        public Staff()
        {
            Expenses = new HashSet<Expense>();
            Incomes = new HashSet<Income>();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(120)]
        public string Name { get; set; }

        public int StaffCategory { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }

        public virtual ICollection<Income> Incomes { get; set; }
    }
}
