using CSharpPracticum.Common;
using System.Collections.Generic;

namespace CSharpPracticum.Tasks.Level3;

[TaskInfo(39, 3)]
public sealed class Task39_YieldGenerator : ITask
{
    public string Title => "yield return — генератор чётных чисел";

    public string Description =>
        "Реализовать генератор чётных чисел через yield return.";

    public string[] Requirements => new[]
    {
        "Метод IEnumerable<int> GetEvenNumbers(int max) — возвращает чётные числа до max",
        "Реализовать именно через yield return, без промежуточного списка",
        "Продемонстрировать работу в foreach",
        "Дополнительно: показать ленивость — вывести сообщение внутри генератора и убедиться, что оно печатается по одному"
    };

    public void Run()
    {
        int max = 10;
        Console.WriteLine($"Запрашиваем чётные числа до {max}:\n");

        // Ленивое выполнение: код внутри GetEvenNumbers начнет работать только сейчас,
        // когда мы запустим цикл foreach.
        foreach (int evenNumber in GetEvenNumbers(max))
        {
            Console.WriteLine($"Получено число: {evenNumber}");
        }

        Console.WriteLine("\nГенератор завершил работу.");
    }

    // Генератор через yield return
    private IEnumerable<int> GetEvenNumbers(int max)
    {
        for (int i = 0; i <= max; i++)
        {
            if (i % 2 == 0)
            {
                // Это сообщение печатается ТОЛЬКО в момент, когда foreach запрашивает следующее число.
                // Это и есть доказательство ленивости (lazy evaluation).
                Console.WriteLine($"  [Генератор]: готовлю число {i}");

                yield return i; // Возвращаем число и "замираем" до следующего запроса
            }
        }
    }
}