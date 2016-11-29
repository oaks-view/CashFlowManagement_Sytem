using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Models
{
    class Income
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        void EditIncome(string description)
        {
            this.Description = description;
        }
        void EditIncome(int amount)
        {
            this.Amount = amount;
        }
        void EditIncome(string description, int amount)
        {
            this.Description = description;
            this.Amount = amount;
        }

    }
}
