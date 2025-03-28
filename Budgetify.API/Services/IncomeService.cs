using budgetifyAPI.Dtos;
using budgetifyAPI.Enums;
using budgetifyAPI.Models;
using budgetifyAPI.Repository.Incomes;
using budgetifyAPI.Repository.Users;

namespace budgetifyAPI.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIncomeRepository _incomeRepo;
        private readonly ILogger<IIncomeService> _logger;
        private readonly IUserRepository _userRepo;
        public IncomeService(IUnitOfWork unitOfWork, ILogger<IncomeService> logger,IIncomeRepository incomeRepository, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _incomeRepo = incomeRepository;
            _userRepo = userRepository;
            _logger = logger;
        }

        private AccountTransaction CreateTransaction(Account account, Income income)
        {

            var ThisTransaction = new AccountTransaction
            {
                Type = TransactionType.Income,
                AccountId = income.AccountId,
                Amount = income.Amount,
                Description = income.Description,
                ClosingBalance = account.Balance + income.Amount,
                UserId = _userRepo.User.Id
            };

            return ThisTransaction;
        }

        public async Task<IncomeDto> CreateIncome(CreateIncomeDto income)
        {
            var account = await _unitOfWork.accountRepo.GetAccountById(income.AccountId);
            if (account == null)
            {
                throw new BadHttpRequestException($"No account found with given id: {income.AccountId}");
            }
            var incomeType = await _unitOfWork.incomeRepo.GetIncomeTypeById(income.IncomeTypeId);
            if (incomeType == null)
            {
                throw new BadHttpRequestException($"No income type found with given id: {income.IncomeTypeId}");
            }
            var ThisIncome = new Income
            {
                IncomeTypeId = income.IncomeTypeId,
                Amount = income.Amount,
                AccountId = income.AccountId,
                Description = income.Description,
                UserId = _userRepo.User.Id

            };
            var ThisTransaction = CreateTransaction(account, ThisIncome);
            await _unitOfWork.transactionRepo.CreateNewTransaction(ThisTransaction, false);
            // update account balance --- account is being tracked in context
            account.Balance = account.Balance + income.Amount;
            ThisIncome.Transaction = ThisTransaction;
            await _unitOfWork.incomeRepo.CreateIncome (ThisIncome);
            await _unitOfWork.SaveChangesAsync();

            return new IncomeDto
            {
                Id = ThisIncome.Id,
                IncomeTypeId = ThisIncome.IncomeTypeId,
                Amount = ThisIncome.Amount,
                AccountId = account.Id,
                Account = account.Description,
                DateCreated = ThisIncome.DateCreated,
                DateUpdated = ThisIncome.DateUpdated,
                Description = ThisIncome.Description,
                UserId = _userRepo.User.Id,
                Transaction = new TransactionDto
                {
                    Id = ThisTransaction.Id,
                    AccountId = account.Id,
                    AccountName = account.Name,
                    Amount = ThisIncome.Amount,
                    ClosingBalance = ThisTransaction.ClosingBalance,
                    DateCreated = ThisTransaction.DateCreated,
                    DateUpdated = ThisTransaction.DateUpdated,
                    Description = ThisTransaction.Description,
                    Type = TransactionType.Income.ToString()
                }

            };
        }

        public async Task<ICollection<IncomeType>> GetAllIncomeType()
        {
            return await _incomeRepo.GetAllIncomeType();
        }

        public async Task<IncomeType> CreateIncomeType(CreateIncomeTypeDto incomeType)
        {

            var newIncomeType = new IncomeType
            {
                Name = incomeType.Name,
                Description = incomeType.Description,
                AddedBy = AddedBy.User, // AddedBy enum
                UserId = _userRepo.User.Id,
            };
            await _incomeRepo.CreateIncomeType(newIncomeType);

            return new IncomeType
            {
                Id = newIncomeType.Id,
                Name = newIncomeType.Name,
                Description = newIncomeType.Description,
            };
        }
    }
}
