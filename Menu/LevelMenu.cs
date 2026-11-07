using CSharpPracticum.Common;
using CSharpPracticum.Tasks.Level1;

namespace CSharpPracticum.Menu;

public sealed class LevelMenu
{
    private readonly string _levelTitle;
    private readonly (int Number, Func<ITask> Factory)[] _tasks;

    public LevelMenu(string levelTitle, (int Number, Func<ITask> Factory)[] tasks)
    {
        _levelTitle = levelTitle;
        _tasks = tasks;
    }

    public void Show()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine($"--- {_levelTitle} ---");
            foreach (var (number, factory) in _tasks)
            {
                var t = factory();
                Console.WriteLine($"{number}. {t.Title}");
            }
            Console.WriteLine($"{_tasks.Length + 1}. Назад");
            Console.Write("Выберите задание: ");

            string? input = Console.ReadLine();
            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Некорректный ввод.");
                continue;
            }

            if (choice == _tasks.Length + 1)
                return;

            var found = Array.Find(_tasks, x => x.Number == choice);
            if (found.Factory == null)
            {
                Console.WriteLine("Нет такого задания.");
                continue;
            }

            var task = found.Factory();
            ConsoleHelper.PrintTaskHeader(task, found.Number);

            Console.WriteLine("[запуск решения...]");
            Console.WriteLine();
            try
            {
                task.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении: {ex.Message}");
            }

            ConsoleHelper.WaitForEnter();
        }
    }
}