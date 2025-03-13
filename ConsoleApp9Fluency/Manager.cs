using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp9Fluency;

public class Manager(AuthorDbContext dbContext)
{
    private async Task ReportAuthors()
    {
        var authorsWithBooks = await dbContext.BookAuthors.QueryAuthorsWithMultiplePublishedBooks();


        foreach (var authorWithBook in authorsWithBooks)
        {
            Author author = authorWithBook.Author;
            int booksCount = authorWithBook.Books.Count();

            var years = authorWithBook.Books
                .Select(x => ((Published) x.Release.Publication).PublishedOn.Year)
                .ToList();

            var (minYear, maxYear) = (years[0], years[0]);

            foreach (var year in years)
            {
                (minYear, maxYear) = (Math.Min(minYear, year), Math.Max(maxYear, year));
            }

            var report = new StringBuilder()
                .Append(author.Name.First).Append(" ").Append(author.Name.Last);

            report = report.Append(" ").Append(booksCount).Append(booksCount > 1 ? " books" : " book");

            report.Append(" ").Append(minYear);
            if (minYear != maxYear)
            {
                report.Append(" - ").Append(maxYear);
            }

            Console.WriteLine(report);
        }
    }
}

static class AuthorQueries
{
    public static async Task<IEnumerable<AuthorWithBooks>> QueryAuthorsWithMultiplePublishedBooks(
        this IQueryable<BookAuthor> bookAuthors) =>
        (await bookAuthors
            .WithBooks(2)
            .Select(x => new AuthorAndBook(x.Author, x.Book))
            .ToListAsync())
        .WherePublished()
        .GroupBy(x => x.Author)
        .Where(g => g.Count() > 1)
        .Select(x => new AuthorWithBooks(x.Key, x.Select(c => c.Book).ToList()));

    public static IQueryable<BookAuthor> WithBooks(
        this IQueryable<BookAuthor> bookAuthors, int atLeast) =>
        atLeast < 2
            ? bookAuthors
            : bookAuthors.Where(x => bookAuthors.Count(o => o.AuthorId == x.Author.Id) >= atLeast);
    
    public static IEnumerable<AuthorAndBook> WherePublished(this IEnumerable<AuthorAndBook> authorAndBooks) =>
        authorAndBooks.Where(x => x.Book.Release.Publication is Published);
}

record AuthorWithBooks(Author Author, List<Book> Books);
record AuthorAndBook(Author Author, Book Book);