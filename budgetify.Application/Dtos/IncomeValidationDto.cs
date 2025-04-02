using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budgetify.Application.Dtos
{
    public class IncomeValidationDto
    {
        public bool IsAccountExists { get; set; }
        public bool IsIncomeTypeExists { get; set; }
    }
}
