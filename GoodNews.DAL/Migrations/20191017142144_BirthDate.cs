using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodNews.DAL.Migrations
{
    public partial class BirthDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("60a783bf-803b-4e60-b352-9bfdada085f7"));

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("7f6355ab-7190-4261-af70-8e2d511552cf"));

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("c30bdceb-3045-4aa0-a4ce-2bca0c03b718"));

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "Url" },
                values: new object[] { new Guid("7c3f8174-695a-4e1f-8ef4-3d32e8cd31c0"), "Новости onliner.by", "Onliner", "https://people.onliner.by/feed" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "Url" },
                values: new object[] { new Guid("b1a80a06-1aca-4801-a7fc-e98cdf1b4a18"), "Новости s13", "S13", "http://s13.ru/rss" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "Url" },
                values: new object[] { new Guid("2edb4e25-076b-4955-ada3-c4aec77b1442"), "Новости tut.by", "Tut.by", "https://news.tut.by/rss/all.rss" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("2edb4e25-076b-4955-ada3-c4aec77b1442"));

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("7c3f8174-695a-4e1f-8ef4-3d32e8cd31c0"));

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("b1a80a06-1aca-4801-a7fc-e98cdf1b4a18"));

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "Url" },
                values: new object[] { new Guid("7f6355ab-7190-4261-af70-8e2d511552cf"), "Новости onliner.by", "Onliner", "https://people.onliner.by/feed" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "Url" },
                values: new object[] { new Guid("60a783bf-803b-4e60-b352-9bfdada085f7"), "Новости s13", "S13", "http://s13.ru/rss" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "Url" },
                values: new object[] { new Guid("c30bdceb-3045-4aa0-a4ce-2bca0c03b718"), "Новости tut.by", "Tut,by", "https://news.tut.by/rss/all.rss" });
        }
    }
}
