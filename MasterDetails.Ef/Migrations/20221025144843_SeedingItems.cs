using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterDetails.Ef.Migrations
{
    public partial class SeedingItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(table: "items",
               columns: new[] { "ItemId", "Name", "Price" },
               values: new object[] { 1, "Chicken Tenders",50.0 }
               );
            migrationBuilder.InsertData(table: "items",
              columns: new[] { "ItemId", "Name", "Price" },
              values: new object[] { 2, "Chicken Tenders w/ Onion", 60.0 }
              );
            migrationBuilder.InsertData(table: "items",
               columns: new[] { "ItemId", "Name", "Price" },
               values: new object[] { 3, "Chicken Tenders w/ Fries w/Cola", 80.0 }
               );
           
            migrationBuilder.InsertData(table: "items",
               columns: new[] { "ItemId", "Name", "Price" },
               values: new object[] { 4, "Grilled Cheese Sandwich", 60.0 }
               );
            migrationBuilder.InsertData(table: "items",
                          columns: new[] { "ItemId", "Name", "Price" },
                          values: new object[] { 5, "Grilled Cheese Sandwich w/ Onion", 70.0 }
                          );
            migrationBuilder.InsertData(table: "items",
               columns: new[] { "ItemId", "Name", "Price" },
               values: new object[] { 6, "Grilled Cheese Sandwich w/ Fries", 90.0 }
               );
            migrationBuilder.InsertData(table: "items",
                          columns: new[] { "ItemId", "Name", "Price" },
                          values: new object[] { 7, "Lettuce and Tomato Burger", 40.0 }
                          );
            migrationBuilder.InsertData(table: "items",
                        columns: new[] { "ItemId", "Name", "Price" },
                        values: new object[] { 8, "Soup", 40.0 }
               );
            migrationBuilder.InsertData(table: "items",
                          columns: new[] { "ItemId", "Name", "Price" },
                          values: new object[] { 9, "Onion Rings", 10.0}
                          );
            migrationBuilder.InsertData(table: "items",
                        columns: new[] { "ItemId", "Name", "Price" },
                        values: new object[] { 10, "Fries", 20.0}
               );
            migrationBuilder.InsertData(table: "items",
                          columns: new[] { "ItemId", "Name", "Price" },
                          values: new object[] { 11, "Fries With cheese", 30.0 }
                          );
            migrationBuilder.InsertData(table: "items",
               columns: new[] { "ItemId", "Name", "Price" },
               values: new object[] { 12, "Sweet Potato Fries", 30.0 }
               );
            migrationBuilder.InsertData(table: "items",
              columns: new[] { "ItemId", "Name", "Price" },
              values: new object[] { 13, "Tea", 20.0 }
              );
            migrationBuilder.InsertData(table: "items",
              columns: new[] { "ItemId", "Name", "Price" },
              values: new object[] { 14, "CoFFe", 30.0 }
              );
            migrationBuilder.InsertData(table: "items",
             columns: new[] { "ItemId", "Name", "Price" },
             values: new object[] { 15, "Can", 25.0 }
             );
            migrationBuilder.InsertData(table: "items",
             columns: new[] { "ItemId", "Name", "Price" },
             values: new object[] { 16, "Water", 15.0 }
             );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From [items]");
        }
    }
}
