using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level1;

[TaskInfo(9, 1)]
public sealed class Task09_ArrayStats : ITask
{
    public string Title => "Массивы: min/max/sum/avg";

    public string Description =>
        "Для массива чисел найти минимум, максимум, сумму и среднее.";

    public string[] Requirements => new[]
    {
        "Заполнить массив случайными числами 1–100, размер 20",
        "Реализовать без LINQ (Min/Max/Sum/Average запрещены)",
        "Оформить 4 отдельных метода",
        "Пустой массив — обработать (вернуть ошибку или 0, но не падать)"
    };

    public void Run()
    {
        Random rnd = new Random();
        int[] numbers = new int[20];
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = rnd.Next(1, 101); // от 1 до 100 включительно
        }

        Console.WriteLine("Массив: " + string.Join(", ", numbers));
        Console.WriteLine();

        Console.WriteLine($"Минимум: {FindMin(numbers)}");
        Console.WriteLine($"Максимум: {FindMax(numbers)}");
        Console.WriteLine($"Сумма: {FindSum(numbers)}");

        // Проверка на пустой массив для среднего арифметического
        if (numbers.Length == 0)
            Console.WriteLine("Среднее: 0 (массив пуст)");
        else
            Console.WriteLine($"Среднее: {FindAverage(numbers):F2}");

        Console.WriteLine();
        Console.WriteLine("--- Проверка на пустом массиве ---");
        int[] empty = Array.Empty<int>();
        Console.WriteLine($"Мин: {FindMin(empty)}, Макс: {FindMax(empty)}, Сумма: {FindSum(empty)}");
    }

    private int FindMin(int[] arr)
    {
        if (arr.Length == 0) return 0; // Защита от пустого массива
        int min = arr[0];
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] < min) min = arr[i];
        }
        return min;
    }

    private int FindMax(int[] arr)
    {
        if (arr.Length == 0) return 0;
        int max = arr[0];
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] > max) max = arr[i];
        }
        return max;
    }

    private long FindSum(int[] arr)
    {
        if (arr.Length == 0) return 0;
        long sum = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            sum += arr[i];
        }
        return sum;
    }

    private double FindAverage(int[] arr)
    {
        if (arr.Length == 0) return 0;
        // Вызываем наш собственный метод суммы, запрещенные LINQ-методы не используем
        return (double)FindSum(arr) / arr.Length;
    }
}