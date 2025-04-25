using budgetify.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budgetify.Application.Services
{
    public interface IEmailService
    {
        public Task SendEmailAsync(EmailJob emailJob);
    }
}
