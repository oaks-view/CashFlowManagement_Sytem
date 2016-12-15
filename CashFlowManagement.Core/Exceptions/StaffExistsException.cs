using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Exceptions
{
    public class StaffExistsException:Exception
    {
        public StaffExistsException()
            :base("Staff Duplicate Key, Or Other Errors Occured.")
        {
        }
    }
}
