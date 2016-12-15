using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Exceptions
{
    public class NoMatchFound:Exception
    {
        public NoMatchFound()
            :base("No Staff record was found")
        {
        }

        public NoMatchFound(string msg)
            :base(msg)
        {
        }
    }
}
