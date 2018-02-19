using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Bangazon.Data.Migrations
{
    public partial class productTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "LocalDelivery",
                table: "Product",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "LocalDelivery",
                table: "Product");
        }
    }
}
