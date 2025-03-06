using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuemSoft.FullBlogApp.Repository.Migrations
{
    /// <inheritdoc />
    public partial class asddasds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleTags_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArticleTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("40b63cd3-d9d4-4e47-906f-ea4564b4d827"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 3, 3, 13, 27, 10, 144, DateTimeKind.Local).AddTicks(2954), new DateTime(2025, 3, 3, 13, 27, 10, 144, DateTimeKind.Local).AddTicks(2955) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("c1b57612-0f59-4d8d-956e-07e40fc7734a"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 3, 3, 13, 27, 10, 144, DateTimeKind.Local).AddTicks(2959), new DateTime(2025, 3, 3, 13, 27, 10, 144, DateTimeKind.Local).AddTicks(2960) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aa8504d6-2b32-4e89-8ec0-2f4ebe57074b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "85c37a01-bad7-4c04-be95-9519489d3c26", "AQAAAAIAAYagAAAAEO8whnZX7XfqjPzi+EDzKyAbObIPAs3ES8mJqUrIinsksV9thGNSomSmVAhdIELzcQ==", "c647950c-3b92-435e-a352-3ad14ae3639f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f19cc326-05f2-4305-ad39-f4e0645aeca0"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "44178ebb-580c-4a44-9a6c-5c4753e20c6e", "AQAAAAIAAYagAAAAELN/4ftUR7RFfB4sRemuLaoBxVzzZ44k45k1MNsAisgMQKBcaYv1EiGNMn5afwAlQA==", "85cac726-6ec1-4eb6-910a-0df1eae3df05" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6647b6ef-b52b-4ef8-abb1-f32360323bd6"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 3, 3, 13, 27, 10, 144, DateTimeKind.Local).AddTicks(3739), new DateTime(2025, 3, 3, 13, 27, 10, 144, DateTimeKind.Local).AddTicks(3740) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b6a67185-45c5-4707-af76-1b55ed3c3b6a"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 3, 3, 13, 27, 10, 144, DateTimeKind.Local).AddTicks(3734), new DateTime(2025, 3, 3, 13, 27, 10, 144, DateTimeKind.Local).AddTicks(3736) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("6d34db5e-b9d5-458f-be25-0de3e05b9cac"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 3, 3, 13, 27, 10, 144, DateTimeKind.Local).AddTicks(4290), new DateTime(2025, 3, 3, 13, 27, 10, 144, DateTimeKind.Local).AddTicks(4291) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("f2292884-2b04-43c4-a11d-45dbf39b244e"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 3, 3, 13, 27, 10, 144, DateTimeKind.Local).AddTicks(4294), new DateTime(2025, 3, 3, 13, 27, 10, 144, DateTimeKind.Local).AddTicks(4295) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("10b0d58b-8155-48f7-a334-977513ec67d0"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 3, 3, 13, 27, 10, 144, DateTimeKind.Local).AddTicks(4822), new DateTime(2025, 3, 3, 13, 27, 10, 144, DateTimeKind.Local).AddTicks(4822) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("8183e35a-277b-4c1f-8a66-d75f68b80bf5"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 3, 3, 13, 27, 10, 144, DateTimeKind.Local).AddTicks(4820), new DateTime(2025, 3, 3, 13, 27, 10, 144, DateTimeKind.Local).AddTicks(4820) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("9679a96b-e6e6-44a5-b04e-20c80d70bd4b"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 3, 3, 13, 27, 10, 144, DateTimeKind.Local).AddTicks(4817), new DateTime(2025, 3, 3, 13, 27, 10, 144, DateTimeKind.Local).AddTicks(4817) });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTags_ArticleId",
                table: "ArticleTags",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTags_TagId",
                table: "ArticleTags",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleTags");

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

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("6d34db5e-b9d5-458f-be25-0de3e05b9cac"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 2, 16, 23, 12, 35, 216, DateTimeKind.Local).AddTicks(8849), new DateTime(2025, 2, 16, 23, 12, 35, 216, DateTimeKind.Local).AddTicks(8849) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("f2292884-2b04-43c4-a11d-45dbf39b244e"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 2, 16, 23, 12, 35, 216, DateTimeKind.Local).AddTicks(8854), new DateTime(2025, 2, 16, 23, 12, 35, 216, DateTimeKind.Local).AddTicks(8855) });

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
    }
}
