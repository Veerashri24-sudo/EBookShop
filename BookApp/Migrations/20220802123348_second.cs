using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookApp.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "Billing");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Billing",
                newName: "TransactionId");

            migrationBuilder.AlterColumn<int>(
                name: "Total_Payment",
                table: "Billing",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CardExpiryDate",
                table: "Billing",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardExpiryDate",
                table: "Billing");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "Billing",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "User_Id",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Total_Payment",
                table: "Billing",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Billing",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
