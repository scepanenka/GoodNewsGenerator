using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodNews.Migrations.Migrations
{
    public partial class SentimentRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("1ead68d1-4040-46f9-9fc9-d4324c7cc3e7"));

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("6324afd1-2e0a-4ef7-95f9-eaec4fd3f49b"));

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("f6640d51-e294-4cb5-aa70-aebd8392c074"));

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "QuerySelector", "Url" },
                values: new object[] { new Guid("9e4d666e-2404-4b7e-8a6e-833c05298a3b"), "Новости onliner.by", "Onliner", ".news-text", "https://people.onliner.by/feed" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "QuerySelector", "Url" },
                values: new object[] { new Guid("1d76760d-62f8-4c3e-bba7-9b865c4a4680"), "Новости s13", "S13", ".js-mediator-article", "http://s13.ru/rss" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "QuerySelector", "Url" },
                values: new object[] { new Guid("c392e735-c68e-4265-ac60-33cb973c6add"), "Новости tut.by", "Tut.by", "#article_body", "https://news.tut.by/rss/all.rss" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("1d76760d-62f8-4c3e-bba7-9b865c4a4680"));

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("9e4d666e-2404-4b7e-8a6e-833c05298a3b"));

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("c392e735-c68e-4265-ac60-33cb973c6add"));

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "QuerySelector", "Url" },
                values: new object[] { new Guid("1ead68d1-4040-46f9-9fc9-d4324c7cc3e7"), "Новости onliner.by", "Onliner", ".news-text", "https://people.onliner.by/feed" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "QuerySelector", "Url" },
                values: new object[] { new Guid("6324afd1-2e0a-4ef7-95f9-eaec4fd3f49b"), "Новости s13", "S13", ".js-mediator-article", "http://s13.ru/rss" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "QuerySelector", "Url" },
                values: new object[] { new Guid("f6640d51-e294-4cb5-aa70-aebd8392c074"), "Новости tut.by", "Tut.by", "#article_body", "https://news.tut.by/rss/all.rss" });
        }
    }
}
