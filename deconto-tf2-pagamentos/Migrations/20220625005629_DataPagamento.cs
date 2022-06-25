using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace deconto_tf2_pagamentos.Migrations
{
    public partial class DataPagamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataPagamento",
                table: "Pagamentos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Pago",
                table: "Pagamentos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataPagamento",
                table: "Pagamentos");

            migrationBuilder.DropColumn(
                name: "Pago",
                table: "Pagamentos");
        }
    }
}
