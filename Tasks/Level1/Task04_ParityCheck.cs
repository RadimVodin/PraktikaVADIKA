using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level1;

[TaskInfo(4, 1)]
public sealed class Task04_ParityCheck : ITask
{
    public string Title => "Чётность числа (два способа)";

    public string Description =>
        "Определить, чётное ли введённое число.";

    public string[] Requirements => new[]
    {
        "Способ 1: через % 2 == 0",
        "Способ 2: через тернарный оператор ? :",
        "Оба способа реализовать отдельными методами",
        "Вывести результат обоих способов"
    };

    public void Run()
    {
        Console.Write("Введите целое число: ");
        string? input = Console.ReadLine();

        if (!int.TryParse(input, out int number))
        {
            Console.WriteLine("Ошибка: введено не целое число.");
            return;
        }

        // Вызываем оба метода и выводим результаты
        Console.WriteLine($"Способ 1 (через %): {IsEvenModulo(number)}");
        Console.WriteLine($"Способ 2 (через тернарный): {IsEvenTernary(number)}");
    }

    // Способ 1: через остаток от деления
    private bool IsEvenModulo(int n)
    {
        return n % 2 == 0;
    }

    // Способ 2: через тернарный оператор
    private bool IsEvenTernary(int n)
    {
        return (n % 2 == 0) ? true : false;
    }
}