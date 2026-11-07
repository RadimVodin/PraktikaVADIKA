namespace CSharpPracticum.Common;

public static class ConsoleHelper
{
    public static void PrintTaskHeader(ITask task, int number)
    {
        Console.WriteLine("==================================================");
        Console.WriteLine($"Задание {number}. {task.Title}");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("Описание:");
        Console.WriteLine($"  {task.Description}");
        Console.WriteLine();
        Console.WriteLine("Требования:");
        foreach (var req in task.Requirements)
        {
            Console.WriteLine($"  - {req}");
        }
        Console.WriteLine("--------------------------------------------------");
    }

    public static void WaitForEnter()
    {
        Console.WriteLine();
        Console.WriteLine("Нажмите Enter для возврата в меню...");
        Console.ReadLine();
    }
}