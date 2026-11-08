using CSharpPracticum.Common;
using System.IO;

namespace CSharpPracticum.Tasks.Level2;

[TaskInfo(31, 2)]
public sealed class Task31_DisposableFileWriter : ITask
{
    public string Title => "IDisposable + using";

    public string Description =>
        "Реализовать IDisposable для класса, работающего с файлом.";

    public string[] Requirements => new[]
    {
        "Класс FileWriter с полем StreamWriter",
        "Реализовать Dispose() (закрыть StreamWriter)",
        "Продемонстрировать использование через using",
        "Дополнительно: продемонстрировать using-декларацию (C# 8+, using var)"
    };

    public void Run()
    {
        string filePath = "test_file.txt";

        Console.WriteLine("--- Демонстрация 1: Классический блок using ---");
        using (FileWriter writer = new FileWriter(filePath))
        {
            writer.WriteLine("Привет из классического using!");
            writer.WriteLine("Вторая строка.");
        } // Здесь автоматически вызывается Dispose()

        Console.WriteLine("Файл успешно записан и закрыт (блок using).\n");

        Console.WriteLine("--- Демонстрация 2: Using-декларация (C# 8+) ---");
        // Создаем в отдельном блоке, чтобы Dispose сработал до чтения файла
        using (var writer2 = new FileWriter(filePath, append: true))
        {
            writer2.WriteLine("Привет из using-декларации!");
            writer2.WriteLine("Четвертая строка.");
        }

        Console.WriteLine("Файл успешно дописан (using var).\n");

        Console.WriteLine($"Проверка: читаем содержимое файла {filePath}:");
        Console.WriteLine("--------------------------------------------------");
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
            // Удаляем файл после проверки
            File.Delete(filePath);
        }
        Console.WriteLine("--------------------------------------------------");
    }
}

// Класс, реализующий IDisposable
public class FileWriter : IDisposable
{
    private StreamWriter? _writer;
    private bool _disposed = false;

    public FileWriter(string filePath, bool append = false)
    {
        // Открываем файл с доступом FileShare.Read, чтобы другие процессы (чтение) не падали
        FileStream fs = new FileStream(filePath, append ? FileMode.Append : FileMode.Create, FileAccess.Write, FileShare.Read);
        _writer = new StreamWriter(fs);
    }

    public void WriteLine(string text)
    {
        if (_disposed) throw new ObjectDisposedException(nameof(FileWriter));
        _writer?.WriteLine(text);
    }

    // Реализация Dispose()
    public void Dispose()
    {
        if (_disposed) return;

        if (_writer != null)
        {
            _writer.Dispose(); // Закрывает поток и сохраняет данные на диск
            _writer = null;
        }

        _disposed = true;
    }
}