using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Scullery.Data.Migrations
{
    public partial class AddedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.CreateTable(
                name: "Pods",
                columns: table => new
                {
                    PodId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FounderUserName = table.Column<string>(nullable: true),
                    PodName = table.Column<string>(nullable: true),
                    PodPassword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pods", x => x.PodId);
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    BudgetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PodId = table.Column<int>(nullable: false),
                    CurrentWeekBudget = table.Column<double>(nullable: false),
                    CurrentWeekSpent = table.Column<double>(nullable: false),
                    CurrentWeekStart = table.Column<DateTime>(nullable: true),
                    CurrentWeekEnd = table.Column<DateTime>(nullable: true),
                    CumulativeBudget = table.Column<double>(nullable: false),
                    CumulativeSpent = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.BudgetId);
                    table.ForeignKey(
                        name: "FK_Budgets_Pods_PodId",
                        column: x => x.PodId,
                        principalTable: "Pods",
                        principalColumn: "PodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KitchenInventories",
                columns: table => new
                {
                    KitchenInventoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PodId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KitchenInventories", x => x.KitchenInventoryId);
                    table.ForeignKey(
                        name: "FK_KitchenInventories_Pods_PodId",
                        column: x => x.PodId,
                        principalTable: "Pods",
                        principalColumn: "PodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealPlans",
                columns: table => new
                {
                    MealPlanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PodId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPlans", x => x.MealPlanId);
                    table.ForeignKey(
                        name: "FK_MealPlans_Pods_PodId",
                        column: x => x.PodId,
                        principalTable: "Pods",
                        principalColumn: "PodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Planners",
                columns: table => new
                {
                    PlannerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityUserId = table.Column<string>(nullable: true),
                    PodId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    SpoonacularUserName = table.Column<string>(nullable: true),
                    UserHash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planners", x => x.PlannerId);
                    table.ForeignKey(
                        name: "FK_Planners_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Planners_Pods_PodId",
                        column: x => x.PodId,
                        principalTable: "Pods",
                        principalColumn: "PodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IngredientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KitchenInventoryId = table.Column<int>(nullable: false),
                    SpoonacularIngredientId = table.Column<int>(nullable: false),
                    IngredientName = table.Column<string>(nullable: true),
                    UnitType = table.Column<string>(nullable: true),
                    UnitQuantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.IngredientId);
                    table.ForeignKey(
                        name: "FK_Ingredients_KitchenInventories_KitchenInventoryId",
                        column: x => x.KitchenInventoryId,
                        principalTable: "KitchenInventories",
                        principalColumn: "KitchenInventoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SavedRecipes",
                columns: table => new
                {
                    SavedRecipeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlannerId = table.Column<string>(nullable: true),
                    PlannerId1 = table.Column<int>(nullable: true),
                    SpoonacularRecipeId = table.Column<int>(nullable: false),
                    ImageURL = table.Column<string>(nullable: true),
                    RecipeName = table.Column<string>(nullable: true),
                    RecipeURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedRecipes", x => x.SavedRecipeId);
                    table.ForeignKey(
                        name: "FK_SavedRecipes_Planners_PlannerId1",
                        column: x => x.PlannerId1,
                        principalTable: "Planners",
                        principalColumn: "PlannerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledMeals",
                columns: table => new
                {
                    ScheduledMealId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedPlannerId = table.Column<int>(nullable: false),
                    PlannerId = table.Column<int>(nullable: true),
                    SavedRecipeId = table.Column<int>(nullable: false),
                    MealPlanId = table.Column<int>(nullable: false),
                    DateOfMeal = table.Column<DateTime>(nullable: true),
                    Slot = table.Column<int>(nullable: false),
                    MealCompleted = table.Column<bool>(nullable: false),
                    Planned = table.Column<bool>(nullable: false),
                    MealType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledMeals", x => x.ScheduledMealId);
                    table.ForeignKey(
                        name: "FK_ScheduledMeals_MealPlans_MealPlanId",
                        column: x => x.MealPlanId,
                        principalTable: "MealPlans",
                        principalColumn: "MealPlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduledMeals_Planners_PlannerId",
                        column: x => x.PlannerId,
                        principalTable: "Planners",
                        principalColumn: "PlannerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduledMeals_SavedRecipes_SavedRecipeId",
                        column: x => x.SavedRecipeId,
                        principalTable: "SavedRecipes",
                        principalColumn: "SavedRecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_PodId",
                table: "Budgets",
                column: "PodId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_KitchenInventoryId",
                table: "Ingredients",
                column: "KitchenInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_KitchenInventories_PodId",
                table: "KitchenInventories",
                column: "PodId");

            migrationBuilder.CreateIndex(
                name: "IX_MealPlans_PodId",
                table: "MealPlans",
                column: "PodId");

            migrationBuilder.CreateIndex(
                name: "IX_Planners_IdentityUserId",
                table: "Planners",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Planners_PodId",
                table: "Planners",
                column: "PodId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SavedRecipes_PlannerId1",
                table: "SavedRecipes",
                column: "PlannerId1");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledMeals_MealPlanId",
                table: "ScheduledMeals",
                column: "MealPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledMeals_PlannerId",
                table: "ScheduledMeals",
                column: "PlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledMeals_SavedRecipeId",
                table: "ScheduledMeals",
                column: "SavedRecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "ScheduledMeals");

            migrationBuilder.DropTable(
                name: "KitchenInventories");

            migrationBuilder.DropTable(
                name: "MealPlans");

            migrationBuilder.DropTable(
                name: "SavedRecipes");

            migrationBuilder.DropTable(
                name: "Planners");

            migrationBuilder.DropTable(
                name: "Pods");

        }
    }
}
