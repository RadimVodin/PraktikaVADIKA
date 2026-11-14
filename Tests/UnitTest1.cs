namespace Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {

    }
}
using Xunit;
using CSharpPracticum.Tasks.Level3;

namespace Tests;

public class LibraryTests
{
    [Fact]
    public void BorrowBook_Success()
    {
        var library = new Library();
        var book = new Book("Test", "Author");
        var reader = new Reader("Ivan");
        library.AddBook(book);

        library.BorrowBook(reader, book);

        Assert.False(book.IsAvailable);
        Assert.Single(reader.BorrowedBooks);
    }

    [Fact]
    public void BorrowBook_AlreadyBorrowed_ThrowsException()
    {
        var library = new Library();
        var book = new Book("Test", "Author");
        var reader1 = new Reader("Ivan");
        var reader2 = new Reader("Petr");
        library.AddBook(book);

        library.BorrowBook(reader1, book);

        Assert.Throws<InvalidOperationException>(() => library.BorrowBook(reader2, book));
    }

    [Fact]
    public void BorrowBook_LimitExceeded_ThrowsException()
    {
        var library = new Library();
        var reader = new Reader("Ivan");
        var books = new[] {
            new Book("1", "A"), new Book("2", "A"), new Book("3", "A"), new Book("4", "A")
        };
        foreach (var b in books) library.AddBook(b);

        library.BorrowBook(reader, books[0]);
        library.BorrowBook(reader, books[1]);
        library.BorrowBook(reader, books[2]);

        Assert.Throws<InvalidOperationException>(() => library.BorrowBook(reader, books[3]));
    }

    [Fact]
    public void ReturnBook_Success()
    {
        var library = new Library();
        var book = new Book("Test", "Author");
        var reader = new Reader("Ivan");
        library.AddBook(book);

        library.BorrowBook(reader, book);
        library.ReturnBook(reader, book);

        Assert.True(book.IsAvailable);
        Assert.Empty(reader.BorrowedBooks);
    }

    [Fact]
    public void ReturnBook_NotBorrowed_DoesNothing()
    {
        var library = new Library();
        var book = new Book("Test", "Author");
        var reader = new Reader("Ivan");

        library.ReturnBook(reader, book);

        Assert.True(book.IsAvailable);
        Assert.Empty(reader.BorrowedBooks);
    }
}