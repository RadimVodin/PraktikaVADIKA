using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level1;

[TaskInfo(12, 1)]
public sealed class Task12_Palindrome : ITask
{
    public string Title => "Палиндром (без учёта регистра/пробелов)";

    public string Description =>
        "Проверить, является ли строка палиндромом.";

    public string[] Requirements => new[]
    {
        "Игнорировать регистр (ToLower/ToUpper)",
        "Игнорировать пробелы и знаки пунктуации",
        "Не использовать LINQ Reverse",
        "Пустая строка и строка из одного символа — палиндром"
    };

    public void Run()
    {
        Console.Write("Введите строку: ");
        string? input = Console.ReadLine();

        if (input == null)
        {
            input = "";
        }

        bool isPal = IsPalindrome(input);

        Console.WriteLine($"Является ли палиндромом: {isPal}");
    }

    private bool IsPalindrome(string s)
    {
        if (s.Length <= 1) return true;

        // 1. Очищаем строку от всего, кроме букв и цифр, и приводим к нижнему регистру
        string cleanStr = "";
        foreach (char c in s)
        {
            if (char.IsLetterOrDigit(c))
            {
                cleanStr += char.ToLower(c);
            }
        }

        // 2. Проверяем на палиндром с помощью двух указателей (без LINQ Reverse)
        int left = 0;
        int right = cleanStr.Length - 1;

        while (left < right)
        {
            if (cleanStr[left] != cleanStr[right])
            {
                return false;
            }
            left++;
            right--;
        }

        return true;
    }
}