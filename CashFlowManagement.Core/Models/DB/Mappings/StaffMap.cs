using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Models.DB.Mappings
{
    public class StaffMap: EntityTypeConfiguration<Staff>
    {
        public StaffMap()
        {
            HasKey(e => e.Id);
            Property(e => e.Username).IsRequired();
            Property(e => e.FirstName).IsRequired();
            Property(e => e.LastName).IsRequired();
            Property(e => e.StaffCategory).IsRequired();

            HasMany(e => e.Incomes)
                .WithRequired(e => e.Staff)
                .WillCascadeOnDelete();

            HasMany(e => e.Expenses)
                .WithRequired(e => e.Staff)
                .WillCascadeOnDelete();
        }
    }
}
