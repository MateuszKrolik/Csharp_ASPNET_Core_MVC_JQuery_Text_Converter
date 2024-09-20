using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TextConverterApp.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversions_Users_UserModelId",
                table: "Conversions");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserModelId",
                table: "Conversions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversions_Users_UserModelId",
                table: "Conversions",
                column: "UserModelId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversions_Users_UserModelId",
                table: "Conversions");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserModelId",
                table: "Conversions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversions_Users_UserModelId",
                table: "Conversions",
                column: "UserModelId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
