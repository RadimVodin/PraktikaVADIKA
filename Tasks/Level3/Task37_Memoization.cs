using CSharpPracticum.Common;
using System.Diagnostics;

namespace CSharpPracticum.Tasks.Level3;

[TaskInfo(37, 3)]
public sealed class Task37_Memoization : ITask
{
    public string Title => "Мемоизация Фибоначчи через Dictionary";

    public string Description =>
        "Реализовать мемоизированную функцию Фибоначчи.";

    public string[] Requirements => new[]
    {
        "Обычная рекурсивная версия (для сравнения на маленьком N)",
        "Мемоизированная версия с Dictionary<int, long>",
        "Замерить время обеих на N = 30–35 через Stopwatch",
        "Вывести оба времени и разницу"
    };

    // Словарь для хранения уже вычисленных значений
    private Dictionary<int, long> _cache = new Dictionary<int, long>();

    public void Run()
    {
        int n = 35; // 35 — оптимально для замера разницы

        Console.WriteLine($"Вычисление Fibonacci({n})...\n");

        // --- Обычная рекурсия ---
        Stopwatch sw1 = Stopwatch.StartNew();
        long res1 = FibSlow(n);
        sw1.Stop();

        Console.WriteLine($"Обычная рекурсия: {res1} (заняло {sw1.ElapsedMilliseconds} мс)");

        // --- Мемоизация ---
        _cache.Clear(); // Очищаем кэш перед чистым замером
        Stopwatch sw2 = Stopwatch.StartNew();
        long res2 = FibMemo(n);
        sw2.Stop();

        Console.WriteLine($"Мемоизация:       {res2} (заняло {sw2.ElapsedMilliseconds} мс)");

        Console.WriteLine($"\nРазница во времени: {sw1.ElapsedMilliseconds - sw2.ElapsedMilliseconds} мс");
        Console.WriteLine($"Результаты совпадают: {res1 == res2}");
    }

    // Обычная рекурсия (очень медленная)
    private long FibSlow(int n)
    {
        if (n <= 1) return n;
        return FibSlow(n - 1) + FibSlow(n - 2);
    }

    // Мемоизированная рекурсия (быстрая)
    private long FibMemo(int n)
    {
        if (n <= 1) return n;

        // Если уже считали, возвращаем из кэша
        if (_cache.ContainsKey(n))
        {
            return _cache[n];
        }

        // Иначе считаем и сохраняем в кэш
        long result = FibMemo(n - 1) + FibMemo(n - 2);
        _cache[n] = result;
        return result;
    }
}