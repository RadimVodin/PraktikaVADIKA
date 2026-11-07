using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level1;

[TaskInfo(13, 1)]
public sealed class Task13_WordCount : ITask
{
    public string Title => "Счётчик вхождений слов в тексте (Dictionary)";

    public string Description =>
        "Посчитать, сколько раз каждое слово встречается в тексте.";

    public string[] Requirements => new[]
    {
        "Использовать Dictionary<string, int>",
        "Разбить текст на слова (по пробелам и знакам пунктуации)",
        "Привести слова к нижнему регистру",
        "Вывести пары «слово → количество», отсортированные по убыванию количества"
    };

    public void Run()
    {
        // Тестовый текст
        string text = "Вот дом, Который построил Джек. А это пшеница, Которая в темном чулане хранится В доме, Который построил Джек.";

        Console.WriteLine($"Исходный текст: {text}\n");

        var wordCounts = new Dictionary<string, int>();

        // Разбиваем по пробелам и знакам пунктуации (упрощенно - заменяем знаки на пробелы)
        char[] separators = new char[] { ' ', '.', ',', '!', '?', ';', ':', '-', '\n', '\r' };
        string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        foreach (string word in words)
        {
            string cleanWord = word.ToLower(); // Приводим к нижнему регистру

            if (wordCounts.ContainsKey(cleanWord))
            {
                wordCounts[cleanWord]++;
            }
            else
            {
                wordCounts[cleanWord] = 1;
            }
        }

        // Сортируем по убыванию количества.
        // ВАЖНО: Поскольку мы на базовом уровне, используем ручную сортировку (без LINQ OrderByDescending)
        var sortedWords = wordCounts.ToList();
        for (int i = 0; i < sortedWords.Count - 1; i++)
        {
            for (int j = i + 1; j < sortedWords.Count; j++)
            {
                if (sortedWords[j].Value > sortedWords[i].Value)
                {
                    var temp = sortedWords[i];
                    sortedWords[i] = sortedWords[j];
                    sortedWords[j] = temp;
                }
            }
        }

        Console.WriteLine("Результат (слово -> количество):");
        foreach (var pair in sortedWords)
        {
            Console.WriteLine($"{pair.Key,-12} : {pair.Value}");
        }
    }
}