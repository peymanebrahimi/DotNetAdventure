using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");


public class Author
{
    public int Id { get; set; }
    public Name Name { get; set; }
}

public class Book
{
    public int Id { get; set; }
    public Release Release { get; set; }
}

public class BookAuthor
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public int BookId { get; set; }
    public Author Author { get; set; }
    public Book Book { get; set; }
}

public class Release
{
    public Publication Publication { get; set; }
}

public class Publication
{
}

public class Published : Publication
{
    public DateTime PublishedOn { get; set; }
}

public class AuthorDbContext: DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
}

public class Name
{
    public string First { get; set; }
    public string Last { get; set; }
}