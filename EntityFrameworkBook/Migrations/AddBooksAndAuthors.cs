using System;
using System.Collections.Generic;
using EntityFrameworkBook.Models.Book;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkBook.Migrations;

public class AddBooksAndAuthors : Migration
{
     protected override void Up(MigrationBuilder migrationBuilder)
    {
        
        migrationBuilder.CreateTable(
            name: "Authors",
            columns: table => new
            {
                AuthorId = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(nullable: true),
                WebUrl = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Authors", x => x.AuthorId);
            });


        migrationBuilder.CreateTable(
            name: "Books",
            columns: table => new
            {
                BookId = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(nullable: true),
                Description = table.Column<string>(nullable: true),
                PublishedOn = table.Column<DateTime>(nullable: false),
                AuthorId = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Books", x => x.BookId);
                table.ForeignKey(
                    name: "FK_Books_Authors_AuthorId",
                    column: x => x.AuthorId,
                    principalTable: "Authors",
                    principalColumn: "AuthorId",
                    onDelete: ReferentialAction.Cascade);
            });

    
        var authors = new List<Author>();
        var books = new List<Book>();
        for (int i = 1; i <= 1000; i++) 
        {
            var author = new Author
            {
                Name = $"Author {i}",
                WebUrl = $"http://author{i}.com"
            };
            authors.Add(author);


            for (int j = 1; j <= 1000; j++)
            {
                var book = new Book
                {
                    Title = $"Book {((i - 1) * 1000 + j)}",
                    Description = $"Description for book {((i - 1) * 1000 + j)}",
                    PublishedOn = DateTime.Now,
                    AuthorId = author.AuthorId
                };
                books.Add(book);
            }
        }


        migrationBuilder.InsertData(
            table: "Authors",
            columns: new[] { "Name", "WebUrl" },
            values: authors.Select(a => new object[] { a.Name, a.WebUrl }).ToArray()
        );

        migrationBuilder.InsertData(
            table: "Books",
            columns: new[] { "Title", "Description", "PublishedOn", "AuthorId" },
            values: books.Select(b => new object[] { b.Title, b.Description, b.PublishedOn, b.AuthorId }).ToArray()
        );
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Books");
        migrationBuilder.DropTable(name: "Authors");
    }
}