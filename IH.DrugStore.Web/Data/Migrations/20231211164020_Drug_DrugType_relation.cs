using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IH.DrugStore.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class Drug_DrugType_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DrugTypeId",
                table: "Drugs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_DrugTypeId",
                table: "Drugs",
                column: "DrugTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drugs_DrugTypes_DrugTypeId",
                table: "Drugs",
                column: "DrugTypeId",
                principalTable: "DrugTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drugs_DrugTypes_DrugTypeId",
                table: "Drugs");

            migrationBuilder.DropIndex(
                name: "IX_Drugs_DrugTypeId",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "DrugTypeId",
                table: "Drugs");
        }
    }
}
