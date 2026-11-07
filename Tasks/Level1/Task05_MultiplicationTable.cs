using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level1;

[TaskInfo(5, 1)]
public sealed class Task05_MultiplicationTable : ITask
{
    public string Title => "Таблица умножения 10×10";

    public string Description =>
        "Вывести таблицу умножения 10×10.";

    public string[] Requirements => new[]
    {
        "Использовать вложенные циклы for",
        "Выровнять столбцы",
        "Заголовки строк/столбцов — не обязательны, но приветствуются"
    };

    public void Run()
    {
        // Вывод заголовка столбцов (для красоты)
        Console.Write("    ");
        for (int j = 1; j <= 10; j++)
        {
            Console.Write($"{j,4}");
        }
        Console.WriteLine();
        Console.WriteLine(new string('-', 44));

        // Вложенные циклы для вывода таблицы
        for (int i = 1; i <= 10; i++)
        {
            // Заголовок строки
            Console.Write($"{i,2} |");

            for (int j = 1; j <= 10; j++)
            {
                // {i * j, 4} означает: вывести результат, выделив под него 4 символа (выравнивание по правому краю)
                Console.Write($"{i * j,4}");
            }
            Console.WriteLine();
        }
    }
}