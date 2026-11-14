using CSharpPracticum.Common;
using System;
using System.Collections.Generic;

namespace CSharpPracticum.Tasks.Level3;

[TaskInfo(45, 3)]
public sealed class Task45_Library : ITask
{
    public string Title => "Мини-проект «Библиотека» + юнит-тесты";

    public string Description => "Классы библиотеки + тесты.";

    public string[] Requirements => new[]
    {
        "Классы: Book (Title, Author, IsAvailable), Reader (Name, BorrowedBooks), Library (выдача/возврат)",
        "Логика: выдача книги читателю, возврат, лимит (не больше 3 книг)",
        "Обработка: книгу уже выдали, лимит превышен — бросать исключения",
        "Написать 5+ юнит-тестов (xUnit/NUnit)",
        "Все тесты должны проходить"
    };

    public void Run()
    {
        var library = new Library();
        var book1 = new Book("Война и мир", "Толстой");
        var book2 = new Book("Преступление и наказание", "Достоевский");
        var book3 = new Book("Мастер и Маргарита", "Булгаков");
        var book4 = new Book("Мертвые души", "Гоголь");

        library.AddBook(book1);
        library.AddBook(book2);
        library.AddBook(book3);
        library.AddBook(book4);

        var reader = new Reader("Иван");

        Console.WriteLine("--- Выдача книг ---");
        library.BorrowBook(reader, book1);
        library.BorrowBook(reader, book2);
        library.BorrowBook(reader, book3);

        Console.WriteLine($"У читателя {reader.Name} книг: {reader.BorrowedBooks.Count}");

        try
        {
            Console.WriteLine("\n--- Попытка выдать 4-ю книгу (должен быть лимит) ---");
            library.BorrowBook(reader, book4);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        try
        {
            Console.WriteLine("\n--- Попытка выдать уже выданную книгу ---");
            library.BorrowBook(new Reader("Петр"), book1);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        Console.WriteLine("\n--- Возврат книги ---");
        library.ReturnBook(reader, book1);
        Console.WriteLine($"У читателя {reader.Name} книг после возврата: {reader.BorrowedBooks.Count}");
    }
}

public class Book
{
    public string Title { get; }
    public string Author { get; }
    public bool IsAvailable { get; set; } = true;

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }
}

public class Reader
{
    public string Name { get; }
    public List<Book> BorrowedBooks { get; } = new List<Book>();

    public Reader(string name)
    {
        Name = name;
    }
}

public class Library
{
    private readonly List<Book> _books = new List<Book>();

    public void AddBook(Book book)
    {
        _books.Add(book);
    }

    public void BorrowBook(Reader reader, Book book)
    {
        if (!book.IsAvailable)
            throw new InvalidOperationException("Книга уже выдана.");

        if (reader.BorrowedBooks.Count >= 3)
            throw new InvalidOperationException("Превышен лимит книг (максимум 3).");

        book.IsAvailable = false;
        reader.BorrowedBooks.Add(book);
    }

    public void ReturnBook(Reader reader, Book book)
    {
        if (reader.BorrowedBooks.Contains(book))
        {
            book.IsAvailable = true;
            reader.BorrowedBooks.Remove(book);
        }
    }
}