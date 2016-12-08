using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CashFlowManagement.Core.Models.DB.Mappings;

namespace CashFlowManagement.Core.Models.DB
{
    public class CashFlowEntities : DbContext
    {
        static CashFlowEntities()
        {
            Database.SetInitializer<CashFlowEntities>(null);
        }

        public CashFlowEntities()
            : base("name=CashFlowDb")
        {
        }

        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<Income> Incomes { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StaffMap());
            modelBuilder.Configurations.Add(new ExpenseMap());
            modelBuilder.Configurations.Add(new IncomeMap());
        }
    }
}
