using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(nullable: true),
                    CustomerAdress = table.Column<string>(nullable: true),
                    CustomersDateOfBirth = table.Column<string>(nullable: true),
                    CustomerDebt = table.Column<int>(nullable: false),
                    CustomerActiveLoan = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Paths",
                columns: table => new
                {
                    PathID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PathNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paths", x => x.PathID);
                });

            migrationBuilder.CreateTable(
                name: "MinorCustomers",
                columns: table => new
                {
                    MinorCustomerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinorCustomerName = table.Column<string>(nullable: true),
                    MinorCustomerAdress = table.Column<string>(nullable: true),
                    MinorCustomersDateOfBirth = table.Column<string>(nullable: true),
                    MinorCustomerDebt = table.Column<int>(nullable: false),
                    MinorCustomerNumber = table.Column<int>(nullable: false),
                    CustomerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinorCustomers", x => x.MinorCustomerID);
                    table.ForeignKey(
                        name: "FK_MinorCustomers_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shelves",
                columns: table => new
                {
                    ShelfID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShelfNumber = table.Column<int>(nullable: false),
                    PathID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shelves", x => x.ShelfID);
                    table.ForeignKey(
                        name: "FK_Shelves_Paths_PathID",
                        column: x => x.PathID,
                        principalTable: "Paths",
                        principalColumn: "PathID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookTitle = table.Column<string>(nullable: true),
                    AuthorOfBook = table.Column<string>(nullable: true),
                    ISBN = table.Column<string>(nullable: true),
                    PurchaseYear = table.Column<int>(nullable: false),
                    ConditionOfBook = table.Column<int>(nullable: false),
                    PurchasePrice = table.Column<int>(nullable: false),
                    BookNumber = table.Column<int>(nullable: false),
                    BookActiveLoan = table.Column<bool>(nullable: false),
                    ShelfID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookID);
                    table.ForeignKey(
                        name: "FK_Books_Shelves_ShelfID",
                        column: x => x.ShelfID,
                        principalTable: "Shelves",
                        principalColumn: "ShelfID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    LoanID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(nullable: false),
                    LoanStart = table.Column<DateTime>(nullable: false),
                    LoanEnd = table.Column<DateTime>(nullable: false),
                    BookID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.LoanID);
                    table.ForeignKey(
                        name: "FK_Loans_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loans_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_ShelfID",
                table: "Books",
                column: "ShelfID");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_BookID",
                table: "Loans",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_CustomerID",
                table: "Loans",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_MinorCustomers_CustomerID",
                table: "MinorCustomers",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Shelves_PathID",
                table: "Shelves",
                column: "PathID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "MinorCustomers");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Shelves");

            migrationBuilder.DropTable(
                name: "Paths");
        }
    }
}
