﻿using budgetifyAPI.Enums;
using budgetifyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace budgetifyAPI.Data
{
    public static class InitializeDatabase
    {
        private static async Task CreateAccounts(DataContext context)
        {
            if (await context.Accounts.AnyAsync()) return;
            var accounts = new List<Account>
            {                
                new Account { 
                    Name="Cash", 
                    Description="Cash account", 
                    Balance=0, 
                    ImageUrl = "https://res.cloudinary.com/dnvc3clgi/image/upload/v1734427264/budgetify/cash.png",
                    AddedBy=AddedBy.System, 
                    UserId = null
                }
            };
            context.Accounts.AddRange(accounts);
            await context.SaveChangesAsync();
        }

        private static async Task CreateIncomeTypes(DataContext context)
        {
            if (await context.IncomeTypes.AnyAsync()) return;
            var incomeTypes = new List<IncomeType>
            {
                new IncomeType {Name = "Initial balance", Description = "Initial Balance", AddedBy=AddedBy.System, UserId = null}

            };
            context.IncomeTypes.AddRange(incomeTypes);
            await context.SaveChangesAsync();
        }

        private static async Task CreateExpenseTypes(DataContext context)
        {
            if (await context.ExpenseTypes.AnyAsync()) return;
            var expenseTypes = new List<ExpenseType>
            {
                new ExpenseType {Name = "Housing", Description = "Expenses like rent, electricity bill",
                        ExpenseCategories = new List<ExpenseCategory> 
                        {
                            new ExpenseCategory {Name = "Rent", Description = "House rent"},
                            new ExpenseCategory {Name = "Electricity", Description = "Electricity bill"},
                            new ExpenseCategory {Name = "Internet", Description = "Internet Bill"},
                            new ExpenseCategory {Name = "House Maid", Description = "Expense for house maid"},
                            new ExpenseCategory {Name = "Baby nanny", Description = "Expenses for baby nanny"}
                        }
                },
                new ExpenseType {Name = "Food", Description = "Expenses for grocery, vegetables, fruits", 
                        ExpenseCategories = new List<ExpenseCategory>
                        {
                            new ExpenseCategory {Name = "Vegetables", Description = "Expenses for vegetables"},
                            new ExpenseCategory {Name = "Fruits", Description = "Expenses for fruits"},
                            new ExpenseCategory {Name = "Grocery", Description = "Expense for grocery"},
                            new ExpenseCategory {Name = "Office Tea", Description = "Office Tea"}
                        }
                },
                new ExpenseType { Name="Transportaion", Description="Expenses for fuel, train taxi",
                        ExpenseCategories = new List<ExpenseCategory>
                        {
                            new ExpenseCategory {Name = "Fuel", Description = "Bike petrol"},
                            new ExpenseCategory {Name = "Train Fare", Description = "Train fare"},
                            new ExpenseCategory {Name = "Bus Fare", Description = "Bus Fare"},
                            new ExpenseCategory {Name = "Cab/Auto/Taxi", Description = "taxi fare"},

                        }
                },
                new ExpenseType { Name="Entertainment", Description="Expenses for movie, club",
                        ExpenseCategories = new List<ExpenseCategory>
                        {
                            new ExpenseCategory {Name = "Party", Description = "Party"},
                            new ExpenseCategory {Name = "Movie", Description = "Movie"}

                        }
                },
                new ExpenseType { Name="Insurance", Description="Expenses for term insurance, health insurance",
                        ExpenseCategories = new List<ExpenseCategory>
                        {
                            new ExpenseCategory {Name = "Mediclaim", Description = "Mediclaim premium"},
                            new ExpenseCategory {Name = "Term Insurance", Description = "Term Insurance"}

                        }
                },
                new ExpenseType { Name="Personal loan", Description="Money given to friends, relative",
                        ExpenseCategories = new List<ExpenseCategory>
                        {
                            new ExpenseCategory {Name = "Money Lent", Description = "Money given to friends"},

                        }
                }
            };
            context.ExpenseTypes.AddRange(expenseTypes);
            await context.SaveChangesAsync();
        }
        

        public static async Task Initialize(DataContext context)
        {
            //await CreateAccounts(context);
            //await CreateExpenseTypes(context);
            //await CreateIncomeTypes(context);

        }
    }
}
