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
                keyValue: new Guid("1d76760d-62f8-4c3e-bba7-9b865c4a4680"));

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("9e4d666e-2404-4b7e-8a6e-833c05298a3b"));

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("c392e735-c68e-4265-ac60-33cb973c6add"));

            migrationBuilder.DropColumn(
                name: "IndexPositivity",
                table: "News");

            migrationBuilder.AddColumn<double>(
                name: "SentimentRating",
                table: "News",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "SentimentRating",
                table: "News");

            migrationBuilder.AddColumn<float>(
                name: "IndexPositivity",
                table: "News",
                nullable: true);

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
    }
}
