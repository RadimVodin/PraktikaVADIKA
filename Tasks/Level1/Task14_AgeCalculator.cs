using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level1;

[TaskInfo(14, 1)]
public sealed class Task14_AgeCalculator : ITask
{
    public string Title => "Возраст по дате рождения";

    public string Description =>
        "По дате рождения вычислить точный возраст в годах, месяцах и днях.";

    public string[] Requirements => new[]
    {
        "Ввод даты через DateTime.TryParse",
        "Учесть, что день рождения в этом году мог ещё не наступить",
        "Вывести «X лет, Y месяцев, Z дней»",
        "Некорректная дата — сообщение об ошибке, не падать"
    };

    public void Run()
    {
        Console.Write("Введите дату рождения (например, 15.05.1990): ");
        string? input = Console.ReadLine();

        if (!DateTime.TryParse(input, out DateTime birthDate))
        {
            Console.WriteLine("Ошибка: некорректный формат даты.");
            return;
        }

        DateTime today = DateTime.Today;

        if (birthDate > today)
        {
            Console.WriteLine("Ошибка: дата рождения не может быть в будущем.");
            return;
        }

        // Вычисляем возраст
        int years = today.Year - birthDate.Year;
        int months = today.Month - birthDate.Month;
        int days = today.Day - birthDate.Day;

        // Корректировка, если день рождения в этом году еще не наступил
        if (days < 0)
        {
            months--;
            // Берем количество дней в предыдущем месяце
            DateTime prevMonth = today.AddMonths(-1);
            days += DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
        }

        if (months < 0)
        {
            years--;
            months += 12;
        }

        Console.WriteLine($"Ваш возраст: {years} лет, {months} месяцев, {days} дней.");
    }
}