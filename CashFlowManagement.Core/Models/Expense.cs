namespace CashFlowManagement.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Expenses")]
    public class Expense
    {
        public Expense() { }
        public Expense(string description, int cost, string staffId)
        {
            Description = description;
            Cost = cost;
            StaffId = staffId;
            DateCreated = DateTime.Now;
        }
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Description { get; set; }

        public int Cost { get; set; }

        public string StaffId { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
