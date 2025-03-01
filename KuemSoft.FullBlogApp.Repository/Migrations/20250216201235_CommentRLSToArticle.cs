using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KuemSoft.FullBlogApp.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CommentRLSToArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("40b63cd3-d9d4-4e47-906f-ea4564b4d827"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 2, 16, 23, 12, 35, 216, DateTimeKind.Local).AddTicks(7610), new DateTime(2025, 2, 16, 23, 12, 35, 216, DateTimeKind.Local).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("c1b57612-0f59-4d8d-956e-07e40fc7734a"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 2, 16, 23, 12, 35, 216, DateTimeKind.Local).AddTicks(7614), new DateTime(2025, 2, 16, 23, 12, 35, 216, DateTimeKind.Local).AddTicks(7615) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aa8504d6-2b32-4e89-8ec0-2f4ebe57074b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d5d68f20-c6c8-4b1a-a115-20cca281c77c", "AQAAAAIAAYagAAAAEMsRRdpJuZT3CaXJY0imywRZx990sx5XH5hNiPV6muMwgeFP6xbUPwUliNwCoKUWDw==", "b4b371b4-a68e-4fcb-8601-1ede932ef517" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f19cc326-05f2-4305-ad39-f4e0645aeca0"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e94c3e45-e89e-48be-a828-ad61de342d13", "AQAAAAIAAYagAAAAECyz7LFvO9ZtWtWnt3O/tO02Ta4eT3JxZSxxWLwAsoGJNZA609oGR0zPznZB+ni2Ow==", "3bf22c8c-0df0-4cf3-b03a-2361f337b9ca" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6647b6ef-b52b-4ef8-abb1-f32360323bd6"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 2, 16, 23, 12, 35, 216, DateTimeKind.Local).AddTicks(8307), new DateTime(2025, 2, 16, 23, 12, 35, 216, DateTimeKind.Local).AddTicks(8308) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b6a67185-45c5-4707-af76-1b55ed3c3b6a"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 2, 16, 23, 12, 35, 216, DateTimeKind.Local).AddTicks(8303), new DateTime(2025, 2, 16, 23, 12, 35, 216, DateTimeKind.Local).AddTicks(8305) });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "AppUserId", "ArticleId", "CreatedBy", "CreatedDate", "IsActive", "IsDeleted", "ModifiedBy", "ModifiedDate", "Text" },
                values: new object[,]
                {
                    { new Guid("6d34db5e-b9d5-458f-be25-0de3e05b9cac"), new Guid("aa8504d6-2b32-4e89-8ec0-2f4ebe57074b"), new Guid("40b63cd3-d9d4-4e47-906f-ea4564b4d827"), "Adminastrator", new DateTime(2025, 2, 16, 23, 12, 35, 216, DateTimeKind.Local).AddTicks(8849), true, false, "Adminastrator", new DateTime(2025, 2, 16, 23, 12, 35, 216, DateTimeKind.Local).AddTicks(8849), "Harika bir makale, ama bu yorum bir test yorumudur." },
                    { new Guid("f2292884-2b04-43c4-a11d-45dbf39b244e"), new Guid("f19cc326-05f2-4305-ad39-f4e0645aeca0"), new Guid("c1b57612-0f59-4d8d-956e-07e40fc7734a"), "Adminastrator", new DateTime(2025, 2, 16, 23, 12, 35, 216, DateTimeKind.Local).AddTicks(8854), true, false, "Adminastrator", new DateTime(2025, 2, 16, 23, 12, 35, 216, DateTimeKind.Local).AddTicks(8855), "Kötü bir makale, anlamak mümkün değildir., ama bu yorum ikinci test yorumudur." }
                });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("10b0d58b-8155-48f7-a334-977513ec67d0"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 2, 16, 23, 12, 35, 216, DateTimeKind.Local).AddTicks(9420), new DateTime(2025, 2, 16, 23, 12, 35, 216, DateTimeKind.Local).AddTicks(9420) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("8183e35a-277b-4c1f-8a66-d75f68b80bf5"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 2, 16, 23, 12, 35, 216, DateTimeKind.Local).AddTicks(9416), new DateTime(2025, 2, 16, 23, 12, 35, 216, DateTimeKind.Local).AddTicks(9417) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("9679a96b-e6e6-44a5-b04e-20c80d70bd4b"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 2, 16, 23, 12, 35, 216, DateTimeKind.Local).AddTicks(9413), new DateTime(2025, 2, 16, 23, 12, 35, 216, DateTimeKind.Local).AddTicks(9414) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("6d34db5e-b9d5-458f-be25-0de3e05b9cac"));

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("f2292884-2b04-43c4-a11d-45dbf39b244e"));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("40b63cd3-d9d4-4e47-906f-ea4564b4d827"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 1, 23, 16, 38, 1, 920, DateTimeKind.Local).AddTicks(3619), new DateTime(2025, 1, 23, 16, 38, 1, 920, DateTimeKind.Local).AddTicks(3620) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("c1b57612-0f59-4d8d-956e-07e40fc7734a"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 1, 23, 16, 38, 1, 920, DateTimeKind.Local).AddTicks(3624), new DateTime(2025, 1, 23, 16, 38, 1, 920, DateTimeKind.Local).AddTicks(3624) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aa8504d6-2b32-4e89-8ec0-2f4ebe57074b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8b50dc02-e098-439a-a01c-16d502de1de3", "AQAAAAIAAYagAAAAEOidCUAqC28NyjD+3FzoHCbpqy7FkBJmO/JDQVDOOR4+jBITnSKztx/dTeAXGFSs/Q==", "8676467c-ba0a-4ad0-a409-f3744a553559" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f19cc326-05f2-4305-ad39-f4e0645aeca0"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d580df44-db58-4557-8d2b-71302f26bb4c", "AQAAAAIAAYagAAAAEDLoZevpAx4H+YuK/GPWnczovpbDfB5LkFZ/fTFZOorb2KRb1/e07e/pR0bkAOZMBg==", "70603d67-c73d-4118-b986-050f3da4fb1d" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6647b6ef-b52b-4ef8-abb1-f32360323bd6"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 1, 23, 16, 38, 1, 920, DateTimeKind.Local).AddTicks(4595), new DateTime(2025, 1, 23, 16, 38, 1, 920, DateTimeKind.Local).AddTicks(4641) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b6a67185-45c5-4707-af76-1b55ed3c3b6a"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 1, 23, 16, 38, 1, 920, DateTimeKind.Local).AddTicks(4590), new DateTime(2025, 1, 23, 16, 38, 1, 920, DateTimeKind.Local).AddTicks(4593) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("10b0d58b-8155-48f7-a334-977513ec67d0"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 1, 23, 16, 38, 1, 920, DateTimeKind.Local).AddTicks(5166), new DateTime(2025, 1, 23, 16, 38, 1, 920, DateTimeKind.Local).AddTicks(5167) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("8183e35a-277b-4c1f-8a66-d75f68b80bf5"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 1, 23, 16, 38, 1, 920, DateTimeKind.Local).AddTicks(5163), new DateTime(2025, 1, 23, 16, 38, 1, 920, DateTimeKind.Local).AddTicks(5164) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("9679a96b-e6e6-44a5-b04e-20c80d70bd4b"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 1, 23, 16, 38, 1, 920, DateTimeKind.Local).AddTicks(5159), new DateTime(2025, 1, 23, 16, 38, 1, 920, DateTimeKind.Local).AddTicks(5160) });
        }
    }
}
