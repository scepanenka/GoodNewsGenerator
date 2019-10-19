using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodNews.DAL.Migrations
{
    public partial class SourceQuerySelector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("36ee4e33-bd40-4da1-84e7-03b49234ef32"));

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("540c8860-7028-4d1f-8c6c-8971920f6a7c"));

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("71d8b2b1-17bc-4948-a20f-270abe72de23"));

            migrationBuilder.AddColumn<string>(
                name: "QuerySelector",
                table: "Sources",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "QuerySelector",
                table: "Sources");

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "Url" },
                values: new object[] { new Guid("71d8b2b1-17bc-4948-a20f-270abe72de23"), "Новости onliner.by", "Onliner", "https://people.onliner.by/feed" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "Url" },
                values: new object[] { new Guid("540c8860-7028-4d1f-8c6c-8971920f6a7c"), "Новости s13", "S13", "http://s13.ru/rss" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "Url" },
                values: new object[] { new Guid("36ee4e33-bd40-4da1-84e7-03b49234ef32"), "Новости tut.by", "Tut.by", "https://news.tut.by/rss/all.rss" });
        }
    }
}
