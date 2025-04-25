using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetify.Domain.Commons.Exceptions
{
    public class NotFoundException : BusinessException
    {
        public NotFoundException(string entity, object key) :
               base ($"{entity} with {key} not found") {}
    }
}
