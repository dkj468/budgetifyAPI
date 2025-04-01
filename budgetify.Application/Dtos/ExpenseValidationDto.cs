using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budgetify.Application.Dtos
{
    public class ExpenseValidationDto
    {
        public bool IsAccountExists { get; set; }
        public bool IsExpenseCategoryExists { get; set; }
        public bool IsExpenseTypeExists { get; set; }
    }
}
