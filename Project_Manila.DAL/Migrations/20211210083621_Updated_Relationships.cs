using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_Manila.DAL.Migrations
{
    public partial class Updated_Relationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Address_ShippingAddressId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "ShippingAddressId",
                table: "Order",
                newName: "ShippingAddressAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_ShippingAddressId",
                table: "Order",
                newName: "IX_Order_ShippingAddressAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Address_ShippingAddressAddressId",
                table: "Order",
                column: "ShippingAddressAddressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Address_ShippingAddressAddressId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "ShippingAddressAddressId",
                table: "Order",
                newName: "ShippingAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_ShippingAddressAddressId",
                table: "Order",
                newName: "IX_Order_ShippingAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Address_ShippingAddressId",
                table: "Order",
                column: "ShippingAddressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
