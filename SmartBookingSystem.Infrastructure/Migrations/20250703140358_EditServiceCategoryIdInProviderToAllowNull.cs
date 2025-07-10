using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBookingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditServiceCategoryIdInProviderToAllowNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Providers_ServiceCategories_ServiceCategoryId",
                table: "Providers");

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceCategoryId",
                table: "Providers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Providers_ServiceCategories_ServiceCategoryId",
                table: "Providers",
                column: "ServiceCategoryId",
                principalTable: "ServiceCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Providers_ServiceCategories_ServiceCategoryId",
                table: "Providers");

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceCategoryId",
                table: "Providers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Providers_ServiceCategories_ServiceCategoryId",
                table: "Providers",
                column: "ServiceCategoryId",
                principalTable: "ServiceCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
