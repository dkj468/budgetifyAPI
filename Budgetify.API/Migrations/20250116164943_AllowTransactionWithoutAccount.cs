using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace budgetifyAPI.Migrations
{
    /// <inheritdoc />
    public partial class AllowTransactionWithoutAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accounttransactions_accounts_accountid",
                table: "accounttransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_expenses_accounts_accountid",
                table: "expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_incomes_accounts_accountid",
                table: "incomes");

            migrationBuilder.AlterColumn<int>(
                name: "accountid",
                table: "incomes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "accountid",
                table: "expenses",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "accountid",
                table: "accounttransactions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_accounttransactions_accounts_accountid",
                table: "accounttransactions",
                column: "accountid",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_expenses_accounts_accountid",
                table: "expenses",
                column: "accountid",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_incomes_accounts_accountid",
                table: "incomes",
                column: "accountid",
                principalTable: "accounts",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accounttransactions_accounts_accountid",
                table: "accounttransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_expenses_accounts_accountid",
                table: "expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_incomes_accounts_accountid",
                table: "incomes");

            migrationBuilder.AlterColumn<int>(
                name: "accountid",
                table: "incomes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "accountid",
                table: "expenses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "accountid",
                table: "accounttransactions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_accounttransactions_accounts_accountid",
                table: "accounttransactions",
                column: "accountid",
                principalTable: "accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_expenses_accounts_accountid",
                table: "expenses",
                column: "accountid",
                principalTable: "accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_incomes_accounts_accountid",
                table: "incomes",
                column: "accountid",
                principalTable: "accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
