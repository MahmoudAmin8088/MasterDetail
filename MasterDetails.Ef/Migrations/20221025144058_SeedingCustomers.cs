using MasterDetails.Core.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterDetails.Ef.Migrations
{
    public partial class SeedingCustomers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(table: "Customer",
                columns: new[] { "CustomerId", "CustomerName" } ,
                values: new object[] {1,"MahmoudAmin"}
                );
            migrationBuilder.InsertData(table: "Customer",
              columns: new[] { "CustomerId", "CustomerName" },
              values: new object[] { 2, "AhmedMagdy" }
              );
            migrationBuilder.InsertData(table: "Customer",
              columns: new[] { "CustomerId", "CustomerName" },
              values: new object[] { 3, "MohamedMustafa" }
              );
            migrationBuilder.InsertData(table: "Customer",
              columns: new[] { "CustomerId", "CustomerName" },
              values: new object[] { 4, "YossefAdel" }
              );
            migrationBuilder.InsertData(table: "Customer",
              columns: new[] { "CustomerId", "CustomerName" },
              values: new object[] { 5, "EslamEssam" }
              );
            migrationBuilder.InsertData(table: "Customer",
              columns: new[] { "CustomerId", "CustomerName" },
              values: new object[] { 6, "AbdalahOmer" }
              );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From [Customer]");

        }
    }
}
