using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Models
{
    public interface IIncome
    {
        int Id { get; set; }
        DateTime Date {get;set;}
        string Description { get; set; }
        int Amount { get; set; }
        void EditIncome(string description);
        void EditIncome(int cost);
        void EditIncome(string description, int cost);

    }
}
