using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Models.DB.Mappings
{
    public class ExpenseMap: EntityTypeConfiguration<Expense>
    {
        public ExpenseMap()
        {
            Property(e => e.StaffId).HasColumnName("StaffId");
            Property(e => e.Description).IsRequired();
            Property(e => e.Cost).IsRequired();
            HasKey(e => e.Id);
        }
    }
}
