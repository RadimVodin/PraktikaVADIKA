using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

[TaskInfo(21, 2)]
public sealed class Task21_EnumDays : ITask
{
    public string Title => "enum дни недели";

    public string Description =>
        "Описать дни недели через enum и определить тип дня.";

    public string[] Requirements => new[]
    {
        "enum DayOfWeek { Monday, ..., Sunday }",
        "Метод IsWeekend(DayOfWeek day) — true для субботы и воскресенья",
        "Метод GetRussianName(DayOfWeek day) — возвращает название на русском",
        "Продемонстрировать на всех 7 днях"
    };

    // Перечисление дней недели (по умолчанию int: Monday=0, ..., Sunday=6)
    public enum DayOfWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    public void Run()
    {
        Console.WriteLine("Дни недели:");
        Console.WriteLine("--------------------------------------------------");

        // Проходимся по всем значениям enum
        foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
        {
            string name = GetRussianName(day);
            bool weekend = IsWeekend(day);
            string type = weekend ? "Выходной" : "Рабочий";

            Console.WriteLine($"{day,-10} | {name,-12} | {type}");
        }
    }

    // Метод: является ли день выходным
    private bool IsWeekend(DayOfWeek day)
    {
        return day == DayOfWeek.Saturday || day == DayOfWeek.Sunday;
    }

    // Метод: получение русского названия
    private string GetRussianName(DayOfWeek day)
    {
        return day switch
        {
            DayOfWeek.Monday => "Понедельник",
            DayOfWeek.Tuesday => "Вторник",
            DayOfWeek.Wednesday => "Среда",
            DayOfWeek.Thursday => "Четверг",
            DayOfWeek.Friday => "Пятница",
            DayOfWeek.Saturday => "Суббота",
            DayOfWeek.Sunday => "Воскресенье",
            _ => "Неизвестно"
        };
    }
}