using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SantoSabor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoMeal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Meal_MealId",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meal",
                table: "Meal");

            migrationBuilder.RenameTable(
                name: "Meal",
                newName: "Meals");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meals",
                table: "Meals",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Meals_MealId",
                table: "OrderItem",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "MealId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Meals_MealId",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meals",
                table: "Meals");

            migrationBuilder.RenameTable(
                name: "Meals",
                newName: "Meal");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meal",
                table: "Meal",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Meal_MealId",
                table: "OrderItem",
                column: "MealId",
                principalTable: "Meal",
                principalColumn: "MealId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
