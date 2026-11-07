using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level1;

[TaskInfo(8, 1)]
public sealed class Task08_FizzBuzz : ITask
{
    public string Title => "FizzBuzz 1–100";

    public string Description =>
        "Вывести числа от 1 до 100 с заменами.";

    public string[] Requirements => new[]
    {
        "Кратно 3 → Fizz",
        "Кратно 5 → Buzz",
        "Кратно 15 → FizzBuzz",
        "Иначе — само число",
        "Порядок проверок: сначала 15, потом 3 и 5"
    };

    public void Run()
    {
        for (int i = 1; i <= 100; i++)
        {
            // Порядок проверок строго по требованию: сначала 15, потом 3, потом 5
            if (i % 15 == 0)
            {
                Console.WriteLine("FizzBuzz");
            }
            else if (i % 3 == 0)
            {
                Console.WriteLine("Fizz");
            }
            else if (i % 5 == 0)
            {
                Console.WriteLine("Buzz");
            }
            else
            {
                Console.WriteLine(i);
            }
        }
    }
}