using CSharpPracticum.Common;
using System;
using System.Threading.Tasks;

namespace CSharpPracticum.Tasks.Level3;

[TaskInfo(40, 3)]
public sealed class Task40_AsyncAwait : ITask
{
    public string Title => "async/await + обработка исключений";

    public string Description =>
        "Асинхронный метод с эмуляцией «скачивания».";

    public string[] Requirements => new[]
    {
        "async Task<string> DownloadAsync(string url) — внутри Task.Delay(1000)",
        "Имитировать ошибку: если url пустой — бросить ArgumentException",
        "Вызвать через await внутри try/catch",
        "Продемонстрировать успешный и ошибочный вызов"
    };

    public void Run()
    {
        Console.WriteLine("--- Тест 1: Успешный вызов ---");
        try
        {
            // Ожидаем 1 секунду, затем получаем результат
            string result1 = DownloadAsync("https://example.com").GetAwaiter().GetResult();
            Console.WriteLine($"Результат: {result1}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        Console.WriteLine("\n--- Тест 2: Ошибочный вызов ---");
        try
        {
            string result2 = DownloadAsync("").GetAwaiter().GetResult();
            Console.WriteLine($"Результат: {result2}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Поймано ожидаемое исключение: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Поймано другое исключение: {ex.Message}");
        }
    }

    // Асинхронный метод
    private async Task<string> DownloadAsync(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException("URL не может быть пустым.");
        }

        // Эмуляция долгой работы (1 секунда)
        await Task.Delay(1000);

        return $"Данные с {url} успешно загружены.";
    }
}