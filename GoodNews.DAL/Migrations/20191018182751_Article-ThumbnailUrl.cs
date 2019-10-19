using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodNews.DAL.Migrations
{
    public partial class ArticleThumbnailUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("aaadaafd-32b7-47e6-82cf-fac02a87d9b4"));

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("ecb34afb-4994-444b-922f-8c6115b53db7"));

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("ecf0db23-9732-406c-9c19-c57a01d1a421"));

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl",
                table: "News",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "QuerySelector", "Url" },
                values: new object[] { new Guid("22636049-ebed-44a9-aa36-267aa89d19bb"), "Новости onliner.by", "Onliner", ".news-text", "https://people.onliner.by/feed" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "QuerySelector", "Url" },
                values: new object[] { new Guid("3d841b00-cbaa-496a-a275-cc3619d54b42"), "Новости s13", "S13", ".js-mediator-article", "http://s13.ru/rss" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "QuerySelector", "Url" },
                values: new object[] { new Guid("915cee91-b054-487c-9c22-51f3158ba6f8"), "Новости tut.by", "Tut.by", "#article_body", "https://news.tut.by/rss/all.rss" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("22636049-ebed-44a9-aa36-267aa89d19bb"));

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("3d841b00-cbaa-496a-a275-cc3619d54b42"));

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("915cee91-b054-487c-9c22-51f3158ba6f8"));

            migrationBuilder.DropColumn(
                name: "ThumbnailUrl",
                table: "News");

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "QuerySelector", "Url" },
                values: new object[] { new Guid("ecf0db23-9732-406c-9c19-c57a01d1a421"), "Новости onliner.by", "Onliner", ".news-text", "https://people.onliner.by/feed" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "QuerySelector", "Url" },
                values: new object[] { new Guid("aaadaafd-32b7-47e6-82cf-fac02a87d9b4"), "Новости s13", "S13", ".js-mediator-article", "http://s13.ru/rss" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "QuerySelector", "Url" },
                values: new object[] { new Guid("ecb34afb-4994-444b-922f-8c6115b53db7"), "Новости tut.by", "Tut.by", "#article_body", "https://news.tut.by/rss/all.rss" });
        }
    }
}
