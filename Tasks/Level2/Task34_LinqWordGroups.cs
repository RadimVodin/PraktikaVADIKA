using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpPracticum.Tasks.Level2;

[TaskInfo(34, 2)]
public sealed class Task34_LinqWordGroups : ITask
{
    public string Title => "LINQ: группировка слов по первой букве";

    public string Description =>
        "Сгруппировать слова по первой букве.";

    public string[] Requirements => new[]
    {
        "Взять список слов (10+)",
        "Сгруппировать через LINQ GroupBy(w => w[0])",
        "Отсортировать группы по первой букве",
        "Внутри группы — отсортировать слова",
        "Вывести «Буква: слово1, слово2, ...»"
    };

    public void Run()
    {
        List<string> words = new List<string>
        {
            "яблоко", "арбуз", "банан", "апельсин", "вишня",
            "автомобиль", "баклажан", "ягода", "астра", "береза",
            "виноград", "якорь"
        };

        Console.WriteLine("Исходный список слов:");
        Console.WriteLine(string.Join(", ", words));
        Console.WriteLine();

        // LINQ Запрос
        var groupedWords = words
            .GroupBy(w => char.ToUpper(w[0]))     // Группируем по первой букве (в верхнем регистре)
            .OrderBy(g => g.Key)                  // Сортируем группы по алфавиту (А, Б, В, Я)
            .Select(g => new
            {
                Letter = g.Key,
                Words = g.OrderBy(w => w).ToList() // Внутри группы сортируем слова
            });

        Console.WriteLine("Результат группировки:");
        Console.WriteLine("--------------------------------------------------");

        foreach (var group in groupedWords)
        {
            Console.WriteLine($"Буква '{group.Letter}': {string.Join(", ", group.Words)}");
        }
    }
}