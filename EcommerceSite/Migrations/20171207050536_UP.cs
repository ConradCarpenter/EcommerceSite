using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EcommerceSite.Migrations
{
    public partial class UP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "UserPurchased",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
                    ItemNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPurchased", x => new { x.UserID, x.ItemNumber });
                    table.ForeignKey(
                        name: "FK_UserPurchased_Items_ItemNumber",
                        column: x => x.ItemNumber,
                        principalTable: "Items",
                        principalColumn: "ItemNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPurchased_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPurchased_ItemNumber",
                table: "UserPurchased",
                column: "ItemNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPurchased");

        }
    }
}
