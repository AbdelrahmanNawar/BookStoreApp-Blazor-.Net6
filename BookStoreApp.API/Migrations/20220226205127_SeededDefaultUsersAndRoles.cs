using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.API.Migrations
{
    public partial class SeededDefaultUsersAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e9ab3a5-9529-4c55-a1b7-a8403877ef39", "893fcdb3-47d2-435a-b718-81598e26aea0", "Administrator", "ADMINSTRATOR" },
                    { "5e87387f-029c-46e4-84f9-f7cf2f497f6f", "b68a68d5-8fcf-4f14-8c4b-4d3284fc80a1", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "6d59e082-5b57-4a85-9ed8-a16bdd14da6a", 0, "9ece3427-9481-496d-803c-98bf4cd2b64b", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEGhuP2s3gLjndHQmB6O1I+huioNWrTyPe0Aay0FDLnypt0vL4aGYHcKBpHyXDNWuSg==", null, false, "c8eb1c6e-262e-44ff-b391-ddabfbf91092", false, "admin@bookstore.com" },
                    { "a7dcaa02-593a-4b26-98b7-568f84a4fc86", 0, "8bbe6a63-dbaa-4258-898f-9d0209146b24", "user@bookstore.com", false, "System", "User ", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEHxtVKSGYoBGN9dLFSXhOyk5GbJ+pccUICgw7VPIejf2OhJxqx05W8kIbg5mbPmAvg==", null, false, "2c2015db-e5b6-4859-ae42-e179e6d24a8a", false, "user@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2e9ab3a5-9529-4c55-a1b7-a8403877ef39", "6d59e082-5b57-4a85-9ed8-a16bdd14da6a" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "5e87387f-029c-46e4-84f9-f7cf2f497f6f", "a7dcaa02-593a-4b26-98b7-568f84a4fc86" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2e9ab3a5-9529-4c55-a1b7-a8403877ef39", "6d59e082-5b57-4a85-9ed8-a16bdd14da6a" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5e87387f-029c-46e4-84f9-f7cf2f497f6f", "a7dcaa02-593a-4b26-98b7-568f84a4fc86" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e9ab3a5-9529-4c55-a1b7-a8403877ef39");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e87387f-029c-46e4-84f9-f7cf2f497f6f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d59e082-5b57-4a85-9ed8-a16bdd14da6a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a7dcaa02-593a-4b26-98b7-568f84a4fc86");
        }
    }
}
