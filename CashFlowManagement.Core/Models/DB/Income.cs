namespace CashFlowManagement.Core.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Income
    {
        public Income() { }
        public Income(string description, int amount, int staffId)
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

        public int StaffId { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
