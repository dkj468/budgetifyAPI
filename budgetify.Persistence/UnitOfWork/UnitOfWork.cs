
using budgetify.Application.Repositories;
using budgetify.Persistence.Contexts;

namespace budgetifyAPI.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _ctx;
        public IExpenseRepository expenseRepo { get;  }
        public IIncomeRepository incomeRepo { get; }
        public IAccountRepository accountRepo { get; }
        public  IUserRepository userRepo { get; }
        public ITransactionRepository transactionRepo { get; }

        IExpenseRepository IUnitOfWork.expenseRepo => throw new NotImplementedException();

        IIncomeRepository IUnitOfWork.incomeRepo => throw new NotImplementedException();

        IAccountRepository IUnitOfWork.accountRepo => throw new NotImplementedException();

        IUserRepository IUnitOfWork.userRepo => throw new NotImplementedException();

        ITransactionRepository IUnitOfWork.transactionRepo => throw new NotImplementedException();

        public UnitOfWork (DataContext context, 
                IExpenseRepository expenseRepository,
                IAccountRepository accountRepository, 
                IUserRepository userRepository, 
                ITransactionRepository transactionRepository,
                IIncomeRepository incomeRepository
            )
        {
            _ctx = context;
            expenseRepo = expenseRepository;
            incomeRepo = incomeRepository;
            accountRepo = accountRepository;
            userRepo = userRepository;
            transactionRepo = transactionRepository;
        }

        public async Task BeginTransactionAsync() => await _ctx.Database.BeginTransactionAsync();

        public async Task CommitTransactionAsync() => await _ctx.Database.CommitTransactionAsync();

        public async Task RollbackTransactionAsync() => await _ctx.Database.RollbackTransactionAsync();

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                int result = await _ctx.SaveChangesAsync();
                await CommitTransactionAsync();
                return result;
            }
            catch (Exception ex)
            {
                await RollbackTransactionAsync();
                throw;
            }
        }
    }
}
