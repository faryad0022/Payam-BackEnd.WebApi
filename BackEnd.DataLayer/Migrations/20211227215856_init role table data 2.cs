using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.DataLayer.Migrations
{
    public partial class initroletabledata2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LastUpdateDate" },
                values: new object[] { new DateTime(2021, 12, 28, 1, 28, 55, 291, DateTimeKind.Local).AddTicks(6170), new DateTime(2021, 12, 28, 1, 28, 55, 295, DateTimeKind.Local).AddTicks(9144) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreateDate", "LastUpdateDate" },
                values: new object[] { new DateTime(2021, 12, 28, 1, 28, 55, 296, DateTimeKind.Local).AddTicks(2344), new DateTime(2021, 12, 28, 1, 28, 55, 296, DateTimeKind.Local).AddTicks(2393) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreateDate", "LastUpdateDate" },
                values: new object[] { new DateTime(2021, 12, 28, 1, 28, 55, 296, DateTimeKind.Local).AddTicks(2443), new DateTime(2021, 12, 28, 1, 28, 55, 296, DateTimeKind.Local).AddTicks(2449) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreateDate", "LastUpdateDate" },
                values: new object[] { new DateTime(2021, 12, 28, 1, 28, 55, 296, DateTimeKind.Local).AddTicks(2454), new DateTime(2021, 12, 28, 1, 28, 55, 296, DateTimeKind.Local).AddTicks(2458) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreateDate", "LastUpdateDate" },
                values: new object[] { new DateTime(2021, 12, 28, 1, 28, 55, 296, DateTimeKind.Local).AddTicks(2462), new DateTime(2021, 12, 28, 1, 28, 55, 296, DateTimeKind.Local).AddTicks(2466) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreateDate", "LastUpdateDate" },
                values: new object[] { new DateTime(2021, 12, 28, 1, 28, 55, 296, DateTimeKind.Local).AddTicks(2470), new DateTime(2021, 12, 28, 1, 28, 55, 296, DateTimeKind.Local).AddTicks(2474) });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreateDate", "IsDelete", "LastUpdateDate", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2021, 12, 28, 1, 28, 55, 309, DateTimeKind.Local).AddTicks(69), false, new DateTime(2021, 12, 28, 1, 28, 55, 309, DateTimeKind.Local).AddTicks(148), 1L, 1L },
                    { 2L, new DateTime(2021, 12, 28, 1, 28, 55, 309, DateTimeKind.Local).AddTicks(1888), false, new DateTime(2021, 12, 28, 1, 28, 55, 309, DateTimeKind.Local).AddTicks(1918), 2L, 1L },
                    { 3L, new DateTime(2021, 12, 28, 1, 28, 55, 309, DateTimeKind.Local).AddTicks(1943), false, new DateTime(2021, 12, 28, 1, 28, 55, 309, DateTimeKind.Local).AddTicks(1948), 3L, 1L },
                    { 4L, new DateTime(2021, 12, 28, 1, 28, 55, 309, DateTimeKind.Local).AddTicks(1952), false, new DateTime(2021, 12, 28, 1, 28, 55, 309, DateTimeKind.Local).AddTicks(1956), 4L, 1L },
                    { 5L, new DateTime(2021, 12, 28, 1, 28, 55, 309, DateTimeKind.Local).AddTicks(1960), false, new DateTime(2021, 12, 28, 1, 28, 55, 309, DateTimeKind.Local).AddTicks(1963), 5L, 1L },
                    { 6L, new DateTime(2021, 12, 28, 1, 28, 55, 309, DateTimeKind.Local).AddTicks(1967), false, new DateTime(2021, 12, 28, 1, 28, 55, 309, DateTimeKind.Local).AddTicks(1971), 6L, 1L }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LastUpdateDate" },
                values: new object[] { new DateTime(2021, 12, 28, 1, 28, 55, 298, DateTimeKind.Local).AddTicks(9198), new DateTime(2021, 12, 28, 1, 28, 55, 298, DateTimeKind.Local).AddTicks(9276) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LastUpdateDate" },
                values: new object[] { new DateTime(2021, 12, 28, 0, 40, 49, 154, DateTimeKind.Local).AddTicks(2142), new DateTime(2021, 12, 28, 0, 40, 49, 160, DateTimeKind.Local).AddTicks(1741) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreateDate", "LastUpdateDate" },
                values: new object[] { new DateTime(2021, 12, 28, 0, 40, 49, 160, DateTimeKind.Local).AddTicks(6870), new DateTime(2021, 12, 28, 0, 40, 49, 160, DateTimeKind.Local).AddTicks(6934) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreateDate", "LastUpdateDate" },
                values: new object[] { new DateTime(2021, 12, 28, 0, 40, 49, 160, DateTimeKind.Local).AddTicks(7016), new DateTime(2021, 12, 28, 0, 40, 49, 160, DateTimeKind.Local).AddTicks(7025) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreateDate", "LastUpdateDate" },
                values: new object[] { new DateTime(2021, 12, 28, 0, 40, 49, 160, DateTimeKind.Local).AddTicks(7032), new DateTime(2021, 12, 28, 0, 40, 49, 160, DateTimeKind.Local).AddTicks(7039) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreateDate", "LastUpdateDate" },
                values: new object[] { new DateTime(2021, 12, 28, 0, 40, 49, 160, DateTimeKind.Local).AddTicks(7047), new DateTime(2021, 12, 28, 0, 40, 49, 160, DateTimeKind.Local).AddTicks(7054) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreateDate", "LastUpdateDate" },
                values: new object[] { new DateTime(2021, 12, 28, 0, 40, 49, 160, DateTimeKind.Local).AddTicks(7062), new DateTime(2021, 12, 28, 0, 40, 49, 160, DateTimeKind.Local).AddTicks(7069) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LastUpdateDate" },
                values: new object[] { new DateTime(2021, 12, 28, 0, 40, 49, 163, DateTimeKind.Local).AddTicks(8974), new DateTime(2021, 12, 28, 0, 40, 49, 163, DateTimeKind.Local).AddTicks(9071) });
        }
    }
}
