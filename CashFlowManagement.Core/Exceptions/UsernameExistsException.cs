using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Exceptions
{
    public class UsernameExistsException:Exception
    {
        public UsernameExistsException()
            :base("Staff with this username already Exists.")
        {
        }
    }
}
