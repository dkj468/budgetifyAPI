using Budgetify.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace budgetify.Persistence.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options) : base(options) 
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountTransaction> AccountTransactions { get; set; }
        public DbSet<IncomeType> IncomeTypes { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {                
                entity.SetTableName(entity.GetTableName()?.ToLower());
            }

            modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()).ToList()
                .ForEach(p => p.SetColumnName(p.GetColumnName().ToLower()));

            modelBuilder.Entity<ExpenseType>()
                .HasMany(et => et.ExpenseCategories)
                .WithOne(ec => ec.ExpenseType);
        }
    }
}
