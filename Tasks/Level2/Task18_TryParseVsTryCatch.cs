using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

[TaskInfo(18, 2)]
public sealed class Task18_TryParseVsTryCatch : ITask
{
    public string Title => "TryParse vs try/catch";

    public string Description =>
        "Сравнить два способа обработки некорректного ввода числа.";

    public string[] Requirements => new[]
    {
        "Способ 1: int.TryParse — вернуть bool + значение через out",
        "Способ 2: int.Parse внутри try/catch — ловить FormatException",
        "Оба оформить отдельными методами",
        "Продемонстрировать на корректном и некорректном вводе"
    };

    public void Run()
    {
        string[] testInputs = { "123", "abc", "42.5", "" };

        foreach (string input in testInputs)
        {
            Console.WriteLine($"Входная строка: \"{input}\"");

            // Способ 1: TryParse
            bool success1 = TryParseMethod(input, out int result1);
            Console.WriteLine($"  TryParse:  {(success1 ? result1.ToString() : "Ошибка парсинга")}");

            // Способ 2: try/catch
            try
            {
                int result2 = ParseWithTryCatch(input);
                Console.WriteLine($"  try/catch: {result2}");
            }
            catch (FormatException)
            {
                Console.WriteLine($"  try/catch: Ошибка парсинга (пойман FormatException)");
            }
            catch (OverflowException)
            {
                Console.WriteLine($"  try/catch: Ошибка (пойман OverflowException — число слишком большое)");
            }

            Console.WriteLine();
        }
    }

    // Метод 1: Безопасный парсинг через TryParse
    private bool TryParseMethod(string input, out int value)
    {
        return int.TryParse(input, out value);
    }

    // Метод 2: Парсинг через try/catch
    private int ParseWithTryCatch(string input)
    {
        // Если input пустой или null, int.Parse кинет ArgumentNullException, 
        // но по условию мы ловим FormatException. 
        // Для чистоты эксперимента проверим, что не пустой.
        if (string.IsNullOrWhiteSpace(input))
            throw new FormatException("Пустая строка");

        return int.Parse(input);
    }
}