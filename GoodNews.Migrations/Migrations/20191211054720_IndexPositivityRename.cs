using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodNews.Migrations.Migrations
{
    public partial class IndexPositivityRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("73124e55-88dd-4c38-b737-85fd4276e6c3"));

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("7c7a34a9-8461-4cfc-af65-a2f6f66a73bf"));

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("99294381-aa53-4cbf-b844-f80d7aae676a"));

            migrationBuilder.RenameColumn(
                name: "IndexPositivity",
                table: "News",
                newName: "SentimentRating");

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "QuerySelector", "Url" },
                values: new object[] { new Guid("82710e08-eb54-48fc-90fc-c0dac9ab6369"), "Новости onliner.by", "Onliner", ".news-text", "https://people.onliner.by/feed" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "QuerySelector", "Url" },
                values: new object[] { new Guid("dbc12a3d-318c-43a6-a464-828435f7ffc7"), "Новости s13", "S13", ".js-mediator-article", "http://s13.ru/rss" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "QuerySelector", "Url" },
                values: new object[] { new Guid("7a2dc6ad-1afb-4cf6-9554-cbe891a12cad"), "Новости tut.by", "Tut.by", "#article_body", "https://news.tut.by/rss/all.rss" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("7a2dc6ad-1afb-4cf6-9554-cbe891a12cad"));

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("82710e08-eb54-48fc-90fc-c0dac9ab6369"));

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("dbc12a3d-318c-43a6-a464-828435f7ffc7"));

            migrationBuilder.RenameColumn(
                name: "SentimentRating",
                table: "News",
                newName: "IndexPositivity");

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "QuerySelector", "Url" },
                values: new object[] { new Guid("7c7a34a9-8461-4cfc-af65-a2f6f66a73bf"), "Новости onliner.by", "Onliner", ".news-text", "https://people.onliner.by/feed" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "QuerySelector", "Url" },
                values: new object[] { new Guid("73124e55-88dd-4c38-b737-85fd4276e6c3"), "Новости s13", "S13", ".js-mediator-article", "http://s13.ru/rss" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "QuerySelector", "Url" },
                values: new object[] { new Guid("99294381-aa53-4cbf-b844-f80d7aae676a"), "Новости tut.by", "Tut.by", "#article_body", "https://news.tut.by/rss/all.rss" });
        }
    }
}
