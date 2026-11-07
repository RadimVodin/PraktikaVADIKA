using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level1;

[TaskInfo(1, 1)]
public sealed class Task01_HelloWorld : ITask
{
    public string Title => "Hello, World + текущая дата";

    public string Description =>
        "Вывести приветствие и текущую дату.";

    public string[] Requirements => new[]
    {
        "Использовать Console.WriteLine",
        "Дату получить через DateTime.Now и отформатировать как dd.MM.yyyy",
        "Вывести две строки: приветствие и дату"
    };

    public void Run()
    {
        Console.WriteLine("Hello, World!");
        string today = DateTime.Now.ToString("dd.MM.yyyy");
        Console.WriteLine($"Сегодня: {today}");
    }
}