using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodNews.Migrations.Migrations
{
    public partial class AlterToNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Categories_CategoryId",
                table: "News");

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

            migrationBuilder.AlterColumn<double>(
                name: "IndexPositivity",
                table: "News",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "News",
                nullable: true,
                oldClrType: typeof(Guid));

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

            migrationBuilder.AddForeignKey(
                name: "FK_News_Categories_CategoryId",
                table: "News",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Categories_CategoryId",
                table: "News");

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

            migrationBuilder.AlterColumn<float>(
                name: "SentimentRating",
                table: "News",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "News",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_News_Categories_CategoryId",
                table: "News",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
