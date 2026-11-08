using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

[TaskInfo(20, 2)]
public sealed class Task20_Generics : ITask
{
    public string Title => "Обобщения: Swap<T>, Max<T> с IComparable<T>";

    public string Description =>
        "Написать обобщённые методы Swap и Max.";

    public string[] Requirements => new[]
    {
        "Swap<T>(ref T a, ref T b) — меняет значения местами",
        "Max<T>(T a, T b) where T : IComparable<T> — возвращает большее",
        "Продемонстрировать на int, string, double",
        "Swap — без ограничений, Max — с ограничением"
    };

    public void Run()
    {
        // --- Демонстрация на int ---
        int x = 5, y = 10;
        Console.WriteLine($"[int] До Swap: x = {x}, y = {y}");
        Swap(ref x, ref y);
        Console.WriteLine($"[int] После Swap: x = {x}, y = {y}");
        Console.WriteLine($"[int] Max(5, 10) = {Max(5, 10)}\n");

        // --- Демонстрация на string ---
        string s1 = "Apple", s2 = "Banana";
        Console.WriteLine($"[string] До Swap: s1 = {s1}, s2 = {s2}");
        Swap(ref s1, ref s2);
        Console.WriteLine($"[string] После Swap: s1 = {s1}, s2 = {s2}");
        Console.WriteLine($"[string] Max(\"Apple\", \"Banana\") = {Max("Apple", "Banana")}\n");

        // --- Демонстрация на double ---
        double d1 = 3.14, d2 = 2.71;
        Console.WriteLine($"[double] До Swap: d1 = {d1}, d2 = {d2}");
        Swap(ref d1, ref d2);
        Console.WriteLine($"[double] После Swap: d1 = {d1}, d2 = {d2}");
        Console.WriteLine($"[double] Max(3.14, 2.71) = {Max(3.14, 2.71)}");
    }

    // Swap без ограничений (работает с любыми типами)
    private void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }

    // Max с ограничением IComparable<T> (только для типов, которые можно сравнивать)
    private T Max<T>(T a, T b) where T : IComparable<T>
    {
        // Если a больше b, возвращаем a, иначе b
        return a.CompareTo(b) > 0 ? a : b;
    }
}