using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleOrder.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    SenderCity = table.Column<string>(type: "text", nullable: false),
                    SenderAddress = table.Column<string>(type: "text", nullable: false),
                    RecipientCity = table.Column<string>(type: "text", nullable: false),
                    RecipientAddress = table.Column<string>(type: "text", nullable: false),
                    Weight = table.Column<decimal>(type: "numeric", nullable: false),
                    ShipmentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
