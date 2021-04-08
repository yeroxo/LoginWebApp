using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginWebApp.Migrations
{
    public partial class insertdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var items = new object[][]
            {
                new object[]{"Яблоко",100},
                new object[]{"Дыня",150},
                new object[]{"Сок мультифрукт",250},
                new object[]{"Помидор",45},
                new object[]{"Огурец",30}
            };

            foreach(var item in items)
            {
                migrationBuilder.InsertData(
                    table: "Items",
                    columns: new[] { "Name", "Price" },
                    values: item);
            }

            var orders = new object[][]
            {
                new object[]{ System.DateTime.Now, "101"},
                new object[]{ System.DateTime.Now, "100"},
                new object[]{ System.DateTime.Now, "111"},
                new object[]{ System.DateTime.Now, "001"},
                new object[]{ System.DateTime.Now, "011" }
            };

            foreach (var order in orders)
            {
                migrationBuilder.InsertData(
                    table: "Order",
                    columns: new[] { "OrderDate", "OrderNumber" },
                    values: order);
            }

            var ordersItem = new object[][]
            {
                new object[]{ 1, 1,20},
                new object[]{ 2, 2,30},
                new object[]{ 3, 3,25},
                new object[]{ 4, 4,39},
                new object[]{ 5, 5, 12}
            };

            foreach (var oItem in ordersItem)
            {
                migrationBuilder.InsertData(
                    table: "OrderItems",
                    columns: new[] { "OrdersId", "ItemId","Amount"},
                    values: oItem);
            }


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM OrderItems", true);
            migrationBuilder.Sql("DELETE FROM Items", true);
            migrationBuilder.Sql("DELETE FROM Order", true);
        }
    }
}
