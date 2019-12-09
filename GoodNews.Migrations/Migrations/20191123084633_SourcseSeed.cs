using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodNews.Migrations.Migrations
{
    public partial class SourcseSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "QuerySelector", "Url" },
                values: new object[] { new Guid("3d08bbba-a424-41be-8b2c-8c7d44668809"), "Новости onliner.by", "Onliner", ".news-text", "https://people.onliner.by/feed" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "QuerySelector", "Url" },
                values: new object[] { new Guid("34cdff13-b882-44fe-a8d9-c7a7182e0a3c"), "Новости s13", "S13", ".js-mediator-article", "http://s13.ru/rss" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "QuerySelector", "Url" },
                values: new object[] { new Guid("b95be1d9-d5c5-46aa-88a7-d6832d08c3fe"), "Новости tut.by", "Tut.by", "#article_body", "https://news.tut.by/rss/all.rss" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("34cdff13-b882-44fe-a8d9-c7a7182e0a3c"));

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("3d08bbba-a424-41be-8b2c-8c7d44668809"));

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("b95be1d9-d5c5-46aa-88a7-d6832d08c3fe"));
        }
    }
}
