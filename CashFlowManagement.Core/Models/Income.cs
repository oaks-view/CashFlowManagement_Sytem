namespace CashFlowManagement.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Income
    {
        public Income() { }
        public Income(string description, int amount, string staffId)
        {
            Description = description;
            Amount = amount;
            StaffId = staffId;
            DateCreated = DateTime.Now;
        }
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Description { get; set; }

        public int Amount { get; set; }

        public string StaffId { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
