using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Models
{
    interface IExpenditure
    {
        int Id { get; set; }
        string Description { get; set; }
        int Cost { get; set; }
        DateTime DateCreated { get; set; }
        void EditExpenditure(string description);
        void EditExpenditure(int cost);
        void EditExpenditure(string description, int cost);

    }
}
