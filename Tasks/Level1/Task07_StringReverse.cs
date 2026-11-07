using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level1;

[TaskInfo(7, 1)]
public sealed class Task07_StringReverse : ITask
{
    public string Title => "Реверс строки без LINQ";

    public string Description =>
        "Перевернуть введённую строку.";

    public string[] Requirements => new[]
    {
        "Не использовать Array.Reverse и LINQ",
        "Реализовать через цикл for от конца к началу",
        "Пустая строка и строка из одного символа — обрабатываются корректно"
    };

    public void Run()
    {
        Console.Write("Введите строку: ");
        string? input = Console.ReadLine();

        if (input == null)
        {
            input = "";
        }

        string reversed = ReverseString(input);

        Console.WriteLine($"Результат: \"{reversed}\"");
    }

    private string ReverseString(string s)
    {
        // Если строка пустая или из одного символа, возвращаем её же
        if (s.Length <= 1)
            return s;

        char[] result = new char[s.Length];

        // Идем циклом от конца строки к началу
        for (int i = 0; i < s.Length; i++)
        {
            result[i] = s[s.Length - 1 - i];
        }

        // Собираем символы обратно в строку
        return new string(result);
    }
}