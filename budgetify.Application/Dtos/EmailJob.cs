using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budgetify.Application.Dtos
{
    public class EmailJob
    {
        public string To {  get; set; }        
        public string Subject { get; set; }
        public string VerificationCode { get; set; }
    }
}
