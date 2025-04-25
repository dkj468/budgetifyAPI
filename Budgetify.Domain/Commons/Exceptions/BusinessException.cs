using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetify.Domain.Commons.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException (string message) : base (message)
        {
            
        }
    }
}
