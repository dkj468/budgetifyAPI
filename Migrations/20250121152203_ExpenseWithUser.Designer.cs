﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using budgetifyAPI.Data;

#nullable disable

namespace budgetifyAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20250121152203_ExpenseWithUser")]
    partial class ExpenseWithUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("budgetifyAPI.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AddedBy")
                        .HasColumnType("integer")
                        .HasColumnName("addedby");

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric")
                        .HasColumnName("balance");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text")
                        .HasColumnName("imageurl");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("userid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("accounts");
                });

            modelBuilder.Entity("budgetifyAPI.Models.AccountTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AccountId")
                        .HasColumnType("integer")
                        .HasColumnName("accountid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric")
                        .HasColumnName("amount");

                    b.Property<decimal>("ClosingBalance")
                        .HasColumnType("numeric")
                        .HasColumnName("closingbalance");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("datecreated");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dateupdated");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("userid");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("UserId");

                    b.ToTable("accounttransactions");
                });

            modelBuilder.Entity("budgetifyAPI.Models.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AccountId")
                        .HasColumnType("integer")
                        .HasColumnName("accountid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric")
                        .HasColumnName("amount");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("datecreated");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dateupdated");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("ExpenseCategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("expensecategoryid");

                    b.Property<int>("ExpenseTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("expensetypeid");

                    b.Property<int>("TransactionId")
                        .HasColumnType("integer")
                        .HasColumnName("transactionid");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("userid");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("ExpenseCategoryId");

                    b.HasIndex("ExpenseTypeId");

                    b.HasIndex("TransactionId");

                    b.HasIndex("UserId");

                    b.ToTable("expenses");
                });

            modelBuilder.Entity("budgetifyAPI.Models.ExpenseCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AddedBy")
                        .HasColumnType("integer")
                        .HasColumnName("addedby");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("ExpenseTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("expensetypeid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("userid");

                    b.HasKey("Id");

                    b.HasIndex("ExpenseTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("expensecategories");
                });

            modelBuilder.Entity("budgetifyAPI.Models.ExpenseType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AddedBy")
                        .HasColumnType("integer")
                        .HasColumnName("addedby");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("userid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("expensetypes");
                });

            modelBuilder.Entity("budgetifyAPI.Models.Income", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AccountId")
                        .HasColumnType("integer")
                        .HasColumnName("accountid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric")
                        .HasColumnName("amount");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("datecreated");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dateupdated");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("IncomeTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("incometypeid");

                    b.Property<int>("TransactionId")
                        .HasColumnType("integer")
                        .HasColumnName("transactionid");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("userid");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("IncomeTypeId");

                    b.HasIndex("TransactionId");

                    b.HasIndex("UserId");

                    b.ToTable("incomes");
                });

            modelBuilder.Entity("budgetifyAPI.Models.IncomeType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AddedBy")
                        .HasColumnType("integer")
                        .HasColumnName("addedby");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("userid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("incometypes");
                });

            modelBuilder.Entity("budgetifyAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("displayname");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phonenumber");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("budgetifyAPI.Models.Account", b =>
                {
                    b.HasOne("budgetifyAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("budgetifyAPI.Models.AccountTransaction", b =>
                {
                    b.HasOne("budgetifyAPI.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("budgetifyAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("User");
                });

            modelBuilder.Entity("budgetifyAPI.Models.Expense", b =>
                {
                    b.HasOne("budgetifyAPI.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("budgetifyAPI.Models.ExpenseCategory", "ExpenseCategory")
                        .WithMany()
                        .HasForeignKey("ExpenseCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("budgetifyAPI.Models.ExpenseType", "ExpenseType")
                        .WithMany()
                        .HasForeignKey("ExpenseTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("budgetifyAPI.Models.AccountTransaction", "Transaction")
                        .WithMany()
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("budgetifyAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("ExpenseCategory");

                    b.Navigation("ExpenseType");

                    b.Navigation("Transaction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("budgetifyAPI.Models.ExpenseCategory", b =>
                {
                    b.HasOne("budgetifyAPI.Models.ExpenseType", "ExpenseType")
                        .WithMany("ExpenseCategories")
                        .HasForeignKey("ExpenseTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("budgetifyAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("ExpenseType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("budgetifyAPI.Models.ExpenseType", b =>
                {
                    b.HasOne("budgetifyAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("budgetifyAPI.Models.Income", b =>
                {
                    b.HasOne("budgetifyAPI.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("budgetifyAPI.Models.IncomeType", "IncomeType")
                        .WithMany()
                        .HasForeignKey("IncomeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("budgetifyAPI.Models.AccountTransaction", "Transaction")
                        .WithMany()
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("budgetifyAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("IncomeType");

                    b.Navigation("Transaction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("budgetifyAPI.Models.IncomeType", b =>
                {
                    b.HasOne("budgetifyAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("budgetifyAPI.Models.ExpenseType", b =>
                {
                    b.Navigation("ExpenseCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
