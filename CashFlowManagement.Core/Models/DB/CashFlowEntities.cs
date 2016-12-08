namespace CashFlowManagement.Core.Models.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class CashFlowEntities : DbContext
    {
        public CashFlowEntities()
            : base("name=CashFlowDb")
        {
        }

        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<Income> Incomes { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Staff>()
                .Property(x => x.Username).IsRequired();

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Expenses)
                .WithRequired(e => e.Staff)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Incomes)
                .WithRequired(e => e.Staff)
                .WillCascadeOnDelete(false);
        }
    }
}
