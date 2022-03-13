using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingList.Infrastructure.Migrations
{
    public partial class AddingModelBuilderConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListItem_ShoppingLists_ShoppingListId",
                table: "ShoppingListItem");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListItem_ShoppingLists_ShoppingListId",
                table: "ShoppingListItem",
                column: "ShoppingListId",
                principalTable: "ShoppingLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListItem_ShoppingLists_ShoppingListId",
                table: "ShoppingListItem");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListItem_ShoppingLists_ShoppingListId",
                table: "ShoppingListItem",
                column: "ShoppingListId",
                principalTable: "ShoppingLists",
                principalColumn: "Id");
        }
    }
}
