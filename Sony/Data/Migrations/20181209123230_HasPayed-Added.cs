using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Sony.Data.Migrations
{
    public partial class HasPayedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasPayed",
                table: "Invoices",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "InsertedById",
                table: "Invoices",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InsertedById",
                table: "Invoices",
                column: "InsertedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AspNetUsers_InsertedById",
                table: "Invoices",
                column: "InsertedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AspNetUsers_InsertedById",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_InsertedById",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "HasPayed",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InsertedById",
                table: "Invoices");
        }
    }
}
