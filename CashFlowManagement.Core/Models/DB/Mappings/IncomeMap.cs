using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Models.DB.Mappings
{
    public class IncomeMap:EntityTypeConfiguration<Income>
    {
        public IncomeMap()
        {
            Property(e => e.StaffId).HasColumnName("StaffId");
            Property(e => e.Description).IsRequired();
            Property(e => e.Amount).IsRequired();
            HasKey(e => e.Id);
        }
    }
}
