using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodNews.DAL.Migrations
{
    public partial class Comment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId1",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserId1",
                table: "Comments");

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
                name: "UserId1",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "QuerySelector", "Url" },
                values: new object[] { new Guid("b24ff25a-71d4-4e12-965f-7d9565c99dea"), "Новости onliner.by", "Onliner", ".news-text", "https://people.onliner.by/feed" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "QuerySelector", "Url" },
                values: new object[] { new Guid("43e0fbef-726e-4536-98a2-2dca2049b5dd"), "Новости s13", "S13", ".js-mediator-article", "http://s13.ru/rss" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Description", "Name", "QuerySelector", "Url" },
                values: new object[] { new Guid("31d33bdb-20c8-4785-a744-f07530f5b3e7"), "Новости tut.by", "Tut.by", "#article_body", "https://news.tut.by/rss/all.rss" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("31d33bdb-20c8-4785-a744-f07530f5b3e7"));

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("43e0fbef-726e-4536-98a2-2dca2049b5dd"));

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: new Guid("b24ff25a-71d4-4e12-965f-7d9565c99dea"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Comments",
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

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId1",
                table: "Comments",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId1",
                table: "Comments",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
