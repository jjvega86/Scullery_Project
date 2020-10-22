using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Scullery.Migrations
{
    public partial class SeedingBudgetData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_KitchenInventories_KitchenInventoryId",
                table: "Ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient");

            migrationBuilder.RenameTable(
                name: "Ingredient",
                newName: "Ingredients");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredient_KitchenInventoryId",
                table: "Ingredients",
                newName: "IX_Ingredients_KitchenInventoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "IngredientId");

            migrationBuilder.InsertData(
                table: "Budgets",
                columns: new[] { "BudgetId", "CumulativeBudget", "CumulativeSpent", "CurrentWeekBudget", "CurrentWeekEnd", "CurrentWeekSpent", "CurrentWeekStart", "PodId" },
                values: new object[] { 1, 0.0, 0.0, 200.0, new DateTime(2020, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 100.0, new DateTime(2020, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "Budgets",
                columns: new[] { "BudgetId", "CumulativeBudget", "CumulativeSpent", "CurrentWeekBudget", "CurrentWeekEnd", "CurrentWeekSpent", "CurrentWeekStart", "PodId" },
                values: new object[] { 2, 0.0, 0.0, 200.0, new DateTime(2020, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 75.0, new DateTime(2020, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "Budgets",
                columns: new[] { "BudgetId", "CumulativeBudget", "CumulativeSpent", "CurrentWeekBudget", "CurrentWeekEnd", "CurrentWeekSpent", "CurrentWeekStart", "PodId" },
                values: new object[] { 3, 0.0, 0.0, 200.0, new DateTime(2020, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 185.0, new DateTime(2020, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_KitchenInventories_KitchenInventoryId",
                table: "Ingredients",
                column: "KitchenInventoryId",
                principalTable: "KitchenInventories",
                principalColumn: "KitchenInventoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_KitchenInventories_KitchenInventoryId",
                table: "Ingredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.DeleteData(
                table: "Budgets",
                keyColumn: "BudgetId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Budgets",
                keyColumn: "BudgetId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Budgets",
                keyColumn: "BudgetId",
                keyValue: 3);

            migrationBuilder.RenameTable(
                name: "Ingredients",
                newName: "Ingredient");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_KitchenInventoryId",
                table: "Ingredient",
                newName: "IX_Ingredient_KitchenInventoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient",
                column: "IngredientId");


            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_KitchenInventories_KitchenInventoryId",
                table: "Ingredient",
                column: "KitchenInventoryId",
                principalTable: "KitchenInventories",
                principalColumn: "KitchenInventoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
