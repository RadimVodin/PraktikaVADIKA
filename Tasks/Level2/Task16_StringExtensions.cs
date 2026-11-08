using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

[TaskInfo(16, 2)]
public sealed class Task16_StringExtensions : ITask
{
    public string Title => "Методы расширения для string";

    public string Description =>
        "Написать extension-методы для string.";

    public string[] Requirements => new[]
    {
        "Реализовать в статическом классе StringExtensions",
        "Методы: IsPalindrome(), WordCount(), ReverseString()",
        "Методы должны быть public static с this string первым параметром",
        "Продемонстрировать вызов на 2–3 строках"
    };

    public void Run()
    {
        string text1 = "А роза упала на лапу Азора";
        string text2 = "Hello world, this is C#!";
        string text3 = "abc";

        Console.WriteLine($"Строка 1: \"{text1}\"");
        Console.WriteLine($"  Реверс: \"{text1.ReverseString()}\"");
        Console.WriteLine($"  Палиндром: {text1.IsPalindrome()}");
        Console.WriteLine($"  Количество слов: {text1.WordCount()}");

        Console.WriteLine($"\nСтрока 2: \"{text2}\"");
        Console.WriteLine($"  Реверс: \"{text2.ReverseString()}\"");
        Console.WriteLine($"  Палиндром: {text2.IsPalindrome()}");
        Console.WriteLine($"  Количество слов: {text2.WordCount()}");

        Console.WriteLine($"\nСтрока 3: \"{text3}\"");
        Console.WriteLine($"  Реверс: \"{text3.ReverseString()}\"");
    }
}

// Класс с методами расширения
public static class StringExtensions
{
    // Реверс строки
    public static string ReverseString(this string str)
    {
        char[] arr = str.ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }

    // Проверка на палиндром (без учета регистра и знаков)
    public static bool IsPalindrome(this string str)
    {
        string clean = "";
        foreach (char c in str)
        {
            if (char.IsLetterOrDigit(c))
                clean += char.ToLower(c);
        }

        int left = 0;
        int right = clean.Length - 1;
        while (left < right)
        {
            if (clean[left] != clean[right]) return false;
            left++; right--;
        }
        return true;
    }

    // Подсчет слов (разделители - пробелы и знаки)
    public static int WordCount(this string str)
    {
        if (string.IsNullOrWhiteSpace(str)) return 0;

        char[] separators = { ' ', '.', ',', '!', '?', ';', ':', '-' };
        string[] words = str.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        return words.Length;
    }
}