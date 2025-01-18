using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace budgetifyAPI.Migrations
{
    /// <inheritdoc />
    public partial class AllowCreateMasterData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "addedby",
                table: "incometypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userid",
                table: "incometypes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "addedby",
                table: "expensetypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userid",
                table: "expensetypes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "addedby",
                table: "expensecategories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userid",
                table: "expensecategories",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "addedby",
                table: "accounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userid",
                table: "accounts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_incometypes_userid",
                table: "incometypes",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_expensetypes_userid",
                table: "expensetypes",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_expensecategories_userid",
                table: "expensecategories",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_accounts_userid",
                table: "accounts",
                column: "userid");

            migrationBuilder.AddForeignKey(
                name: "FK_accounts_users_userid",
                table: "accounts",
                column: "userid",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_expensecategories_users_userid",
                table: "expensecategories",
                column: "userid",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_expensetypes_users_userid",
                table: "expensetypes",
                column: "userid",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_incometypes_users_userid",
                table: "incometypes",
                column: "userid",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accounts_users_userid",
                table: "accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_expensecategories_users_userid",
                table: "expensecategories");

            migrationBuilder.DropForeignKey(
                name: "FK_expensetypes_users_userid",
                table: "expensetypes");

            migrationBuilder.DropForeignKey(
                name: "FK_incometypes_users_userid",
                table: "incometypes");

            migrationBuilder.DropIndex(
                name: "IX_incometypes_userid",
                table: "incometypes");

            migrationBuilder.DropIndex(
                name: "IX_expensetypes_userid",
                table: "expensetypes");

            migrationBuilder.DropIndex(
                name: "IX_expensecategories_userid",
                table: "expensecategories");

            migrationBuilder.DropIndex(
                name: "IX_accounts_userid",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "addedby",
                table: "incometypes");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "incometypes");

            migrationBuilder.DropColumn(
                name: "addedby",
                table: "expensetypes");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "expensetypes");

            migrationBuilder.DropColumn(
                name: "addedby",
                table: "expensecategories");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "expensecategories");

            migrationBuilder.DropColumn(
                name: "addedby",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "accounts");
        }
    }
}
