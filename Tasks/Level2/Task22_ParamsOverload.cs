using CSharpPracticum.Common;
using System.Collections.Generic;

namespace CSharpPracticum.Tasks.Level2;

[TaskInfo(22, 2)]
public sealed class Task22_ParamsOverload : ITask
{
    public string Title => "params-метод + перегрузка";

    public string Description =>
        "Написать метод суммы с params и его перегрузку.";

    public string[] Requirements => new[]
    {
        "Sum(params int[] numbers) — сумма произвольного числа чисел",
        "Перегрузка Sum(int[] numbers) — та же логика для массива",
        "Продемонстрировать: Sum(1, 2, 3), Sum(new[] {1, 2, 3}), Sum()",
        "Пустой вызов — вернуть 0"
    };

    public void Run()
    {
        // Вызов 1: с отдельными аргументами (работает благодаря params)
        int res1 = Sum(1, 2, 3);
        Console.WriteLine($"Sum(1, 2, 3) = {res1}");

        // Вызов 2: с массивом (тут сработает перегрузка Sum(int[]))
        int res2 = Sum(new[] { 1, 2, 3 });
        Console.WriteLine($"Sum(new[] {{1, 2, 3}}) = {res2}");

        // Вызов 3: пустой вызов (params позволяет вызвать без аргументов)
        int res3 = Sum();
        Console.WriteLine($"Sum() = {res3}");

        // Проверка, что результаты 1 и 2 равны
        Console.WriteLine($"Результаты res1 и res2 равны: {res1 == res2}");
    }

    // Перегрузка 1: Метод с params (принимает переменное число аргументов)
    private int Sum(params int[] numbers)
    {
        if (numbers == null || numbers.Length == 0) return 0;

        int sum = 0;
        foreach (int n in numbers)
        {
            sum += n;
        }
        return sum;
    }

    // Перегрузка 2: Метод, принимающий массив напрямую.
    // ВАЖНО: В C# params int[] и int[] для компилятора одно и то же.
    // Чтобы избежать ошибки CS0111, перегрузка принимает List<int>.
    private int Sum(List<int> numbers)
    {
        if (numbers == null || numbers.Count == 0) return 0;

        int sum = 0;
        for (int i = 0; i < numbers.Count; i++)
        {
            sum += numbers[i];
        }
        return sum;
    }
}