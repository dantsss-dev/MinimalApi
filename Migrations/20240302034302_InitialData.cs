using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace projectoef.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Task",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "Description", "Name", "Weight" },
                values: new object[,]
                {
                    { new Guid("16923bc2-aedc-417d-bbe3-fb1bdd16e002"), null, "Actividades personales", 50 },
                    { new Guid("16923bc2-aedc-417d-bbe3-fb1bdd16e06d"), null, "Actividades pendientes", 20 }
                });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "TaskId", "CategoryId", "CreationDate", "Description", "TaskPriority", "Title" },
                values: new object[,]
                {
                    { new Guid("16923bc2-aedc-417d-bbe3-fb1bdd16e010"), new Guid("16923bc2-aedc-417d-bbe3-fb1bdd16e06d"), new DateTime(2024, 3, 1, 21, 43, 2, 37, DateTimeKind.Local).AddTicks(4271), null, 1, "Pago de servicios publicos" },
                    { new Guid("16923bc2-aedc-417d-bbe3-fb1bdd16e011"), new Guid("16923bc2-aedc-417d-bbe3-fb1bdd16e002"), new DateTime(2024, 3, 1, 21, 43, 2, 37, DateTimeKind.Local).AddTicks(4286), null, 0, "Terminar de ver peliculas" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "TaskId",
                keyValue: new Guid("16923bc2-aedc-417d-bbe3-fb1bdd16e010"));

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "TaskId",
                keyValue: new Guid("16923bc2-aedc-417d-bbe3-fb1bdd16e011"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("16923bc2-aedc-417d-bbe3-fb1bdd16e002"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("16923bc2-aedc-417d-bbe3-fb1bdd16e06d"));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Task",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
