namespace CashFlowManagement.Core.Models.DB
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

        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        public int StaffCategory { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }

        public virtual ICollection<Income> Incomes { get; set; }
    }
}