using CashFlowManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core
{
    public class Expenditure:IExpenditure
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                _dateCreated = DateTime.Today;
            }
        }
        public string Description { get; set; }
        public int Cost { get; set; }
        private DateTime _dateCreated;

        public Expenditure() { }//moses might remove this
        public Expenditure(string description, int cost)
        {
            Description = description;
            Cost = cost;
        }

        public DateTime GetDate()
        {
            return _dateCreated;
        }

        public void EditExpenditure(string description, int cost)
        {
            this.Description = description;
            this.Cost = cost;
        }

        public void EditExpenditure(int cost)
        {
            this.Cost = cost;
        }

        public void EditExpenditure(string description)
        {
            this.Description = description;
        }

    }
}
