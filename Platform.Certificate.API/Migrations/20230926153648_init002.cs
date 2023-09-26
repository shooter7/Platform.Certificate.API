using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Certificate.API.Migrations
{
    public partial class init002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Certificates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Certificates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Certificates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                table: "Certificates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Certificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$hVHVl5q6ilYXjFhHWRbaY.9G7Dk2DrOqHy.K0t1s1FuD34aRjD7qi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "ExpireDate",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Certificates");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$jAiuSucYnfns3xjEh6IwLuUJmHwQKG7XCuB56BLwIrOH8l9CVi1he");
        }
    }
}
