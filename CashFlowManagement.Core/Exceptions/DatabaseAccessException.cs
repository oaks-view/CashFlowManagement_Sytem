using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Exceptions
{
    public class DatabaseAccessException:Exception
    {
        public DatabaseAccessException()
            :base("Errors Encountered while attempting Database Operation")
        {
        }

        public DatabaseAccessException(string outerException, Exception innerException)
            :base(outerException,innerException)//moses this ought to cause an issue here
        {
        }
    }
}
