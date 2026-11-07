using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level1;

[TaskInfo(6, 1)]
public sealed class Task06_DigitSum : ITask
{
    public string Title => "Сумма цифр числа (строка + арифметика)";

    public string Description =>
        "Найти сумму цифр введённого числа (например, 1234 -> 10).";

    public string[] Requirements => new[]
    {
        "Способ 1 (строка): число -> ToString() -> пройти по символам -> int.Parse каждой цифры",
        "Способ 2 (арифметика): цикл while (n > 0) { sum += n % 10; n /= 10; }",
        "Оба способа оформить отдельными методами",
        "Отрицательные числа — брать модуль"
    };

    public void Run()
    {
        Console.Write("Введите целое число: ");
        string? input = Console.ReadLine();

        if (!long.TryParse(input, out long number))
        {
            Console.WriteLine("Ошибка: введено не целое число.");
            return;
        }

        long sum1 = SumByString(number);
        long sum2 = SumByArithmetic(number);

        Console.WriteLine($"Способ 1 (строка): {sum1}");
        Console.WriteLine($"Способ 2 (арифметика): {sum2}");
    }

    // Способ 1: через строку
    private long SumByString(long n)
    {
        n = Math.Abs(n); // Берем модуль для отрицательных чисел
        string str = n.ToString();
        long sum = 0;

        foreach (char c in str)
        {
            // Преобразуем символ в строку, затем в число
            sum += int.Parse(c.ToString());
            // Или можно так: sum += c - '0';
        }

        return sum;
    }

    // Способ 2: через арифметику
    private long SumByArithmetic(long n)
    {
        n = Math.Abs(n); // Берем модуль
        long sum = 0;

        while (n > 0)
        {
            sum += n % 10; // Добавляем последнюю цифру
            n /= 10;       // Убираем последнюю цифру
        }

        return sum;
    }
}