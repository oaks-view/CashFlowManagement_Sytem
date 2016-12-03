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
        public int Id{ get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public int StaffId { get; set; }
        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }
        private DateTime _dateCreated;

        public Expenditure() { }//moses might remove this
        public Expenditure(string description, int cost, int staffId)
        {
            Description = description;
            Cost = cost;
            _dateCreated = DateTime.Now;
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

        public string ConvertDateToDataBaseFormat()
        {
            return _dateCreated.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

    }
}
