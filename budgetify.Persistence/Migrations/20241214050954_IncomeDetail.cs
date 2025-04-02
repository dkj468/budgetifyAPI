using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace budgetifyAPI.Migrations
{
    /// <inheritdoc />
    public partial class IncomeDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "category",
                table: "accounttransactions");

            migrationBuilder.CreateTable(
                name: "incometypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_incometypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "incomes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    amount = table.Column<double>(type: "double precision", nullable: false),
                    incometypeid = table.Column<int>(type: "integer", nullable: false),
                    accountid = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    datecreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    dateupdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_incomes", x => x.id);
                    table.ForeignKey(
                        name: "FK_incomes_accounts_accountid",
                        column: x => x.accountid,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_incomes_incometypes_incometypeid",
                        column: x => x.incometypeid,
                        principalTable: "incometypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_incomes_accountid",
                table: "incomes",
                column: "accountid");

            migrationBuilder.CreateIndex(
                name: "IX_incomes_incometypeid",
                table: "incomes",
                column: "incometypeid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "incomes");

            migrationBuilder.DropTable(
                name: "incometypes");

            migrationBuilder.AddColumn<int>(
                name: "category",
                table: "accounttransactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
