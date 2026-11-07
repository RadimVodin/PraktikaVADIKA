using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level1;

[TaskInfo(3, 1)]
public sealed class Task03_Calculator : ITask
{
    public string Title => "Калькулятор с делением на ноль";

    public string Description =>
        "Принимать два числа и оператор (+, -, *, /), выводить результат.";

    public string[] Requirements => new[]
    {
        "Оператор обрабатывать через switch",
        "При делении на ноль — вывести сообщение об ошибке, не ронять приложение",
        "Некорректный оператор — вывести «Неизвестная операция»",
        "Ввод чисел — через double.TryParse"
    };

    public void Run()
    {
        Console.Write("Введите первое число: ");
        string? input1 = Console.ReadLine();

        Console.Write("Введите оператор (+, -, *, /): ");
        string? op = Console.ReadLine();

        Console.Write("Введите второе число: ");
        string? input2 = Console.ReadLine();

        if (!double.TryParse(input1, out double num1) || !double.TryParse(input2, out double num2))
        {
            Console.WriteLine("Ошибка: введены некорректные числа.");
            return;
        }

        double result = 0;
        bool validOp = true;

        switch (op)
        {
            case "+":
                result = num1 + num2;
                break;
            case "-":
                result = num1 - num2;
                break;
            case "*":
                result = num1 * num2;
                break;
            case "/":
                if (num2 == 0)
                {
                    Console.WriteLine("Ошибка: деление на ноль невозможно.");
                    return;
                }
                result = num1 / num2;
                break;
            default:
                Console.WriteLine("Неизвестная операция.");
                validOp = false;
                break;
        }

        if (validOp)
        {
            Console.WriteLine($"Результат: {result}");
        }
    }
}