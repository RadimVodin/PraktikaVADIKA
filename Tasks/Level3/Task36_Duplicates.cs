using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpPracticum.Tasks.Level3;

[TaskInfo(36, 3)]
public sealed class Task36_Duplicates : ITask
{
    public string Title => "Дубликаты в List (HashSet + GroupBy)";

    public string Description =>
        "Найти дубликаты в списке чисел двумя способами.";

    public string[] Requirements => new[]
    {
        "Способ 1: через HashSet<int> — при добавлении проверять Contains",
        "Способ 2: через LINQ GroupBy(x => x).Where(g => g.Count() > 1)",
        "Оба способа оформить отдельными методами",
        "Продемонстрировать на списке с повторяющимися значениями"
    };

    public void Run()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 2, 4, 5, 5, 6, 1, 7, 8, 8 };

        Console.WriteLine("Исходный список: " + string.Join(", ", numbers));
        Console.WriteLine();

        var dups1 = FindDuplicatesHashSet(numbers);
        Console.WriteLine("Дубликаты (HashSet): " + string.Join(", ", dups1));

        var dups2 = FindDuplicatesLinq(numbers);
        Console.WriteLine("Дубликаты (LINQ):    " + string.Join(", ", dups2));

        // Сортируем списки, чтобы сравнить их без учета порядка
        dups1.Sort();
        dups2.Sort();

        Console.WriteLine($"\nРезультаты совпадают: {dups1.SequenceEqual(dups2)}");
    }

    private List<int> FindDuplicatesHashSet(List<int> list)
    {
        HashSet<int> seen = new HashSet<int>();
        HashSet<int> duplicates = new HashSet<int>();

        foreach (int item in list)
        {
            if (seen.Contains(item))
            {
                duplicates.Add(item);
            }
            else
            {
                seen.Add(item);
            }
        }

        return duplicates.ToList();
    }

    private List<int> FindDuplicatesLinq(List<int> list)
    {
        return list
            .GroupBy(x => x)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();
    }
}