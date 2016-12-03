using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Models
{
    public class IncomeEntity
    {
        public int Id { get; set; }
        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int StaffId { get; set; }
        private DateTime _dateCreated;

        public IncomeEntity(string description, int amount, int staffId)
        {
            Description = description;
            Amount = amount;
            StaffId = staffId;
            _dateCreated = DateTime.Now;
        }

        public IncomeEntity() { }
        public void EditIncome(string description)
        {
            this.Description = description;
        }
        public void EditIncome(int amount)
        {
            this.Amount = amount;
        }
        public void EditIncome(string description, int amount)
        {
            this.Description = description;
            this.Amount = amount;
        }

    }
}
