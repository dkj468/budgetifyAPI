using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace budgetifyAPI.Migrations
{
    /// <inheritdoc />
    public partial class ExpenseWithUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "transactionid",
                table: "incomes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userid",
                table: "incomes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "transactionid",
                table: "expenses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userid",
                table: "expenses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userid",
                table: "accounttransactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_incomes_transactionid",
                table: "incomes",
                column: "transactionid");

            migrationBuilder.CreateIndex(
                name: "IX_incomes_userid",
                table: "incomes",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_expenses_transactionid",
                table: "expenses",
                column: "transactionid");

            migrationBuilder.CreateIndex(
                name: "IX_expenses_userid",
                table: "expenses",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_accounttransactions_userid",
                table: "accounttransactions",
                column: "userid");

            migrationBuilder.AddForeignKey(
                name: "FK_accounttransactions_users_userid",
                table: "accounttransactions",
                column: "userid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_expenses_accounttransactions_transactionid",
                table: "expenses",
                column: "transactionid",
                principalTable: "accounttransactions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_expenses_users_userid",
                table: "expenses",
                column: "userid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_incomes_accounttransactions_transactionid",
                table: "incomes",
                column: "transactionid",
                principalTable: "accounttransactions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_incomes_users_userid",
                table: "incomes",
                column: "userid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accounttransactions_users_userid",
                table: "accounttransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_expenses_accounttransactions_transactionid",
                table: "expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_expenses_users_userid",
                table: "expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_incomes_accounttransactions_transactionid",
                table: "incomes");

            migrationBuilder.DropForeignKey(
                name: "FK_incomes_users_userid",
                table: "incomes");

            migrationBuilder.DropIndex(
                name: "IX_incomes_transactionid",
                table: "incomes");

            migrationBuilder.DropIndex(
                name: "IX_incomes_userid",
                table: "incomes");

            migrationBuilder.DropIndex(
                name: "IX_expenses_transactionid",
                table: "expenses");

            migrationBuilder.DropIndex(
                name: "IX_expenses_userid",
                table: "expenses");

            migrationBuilder.DropIndex(
                name: "IX_accounttransactions_userid",
                table: "accounttransactions");

            migrationBuilder.DropColumn(
                name: "transactionid",
                table: "incomes");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "incomes");

            migrationBuilder.DropColumn(
                name: "transactionid",
                table: "expenses");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "expenses");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "accounttransactions");
        }
    }
}
