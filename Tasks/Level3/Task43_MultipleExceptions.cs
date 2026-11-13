using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3;

[TaskInfo(43, 3)]
public sealed class Task43_MultipleExceptions : ITask
{
    public string Title => "Множественные типы исключений, порядок catch";

    public string Description =>
        "Продемонстрировать порядок обработки исключений.";

    public string[] Requirements => new[]
    {
        "Метод, который может бросить FormatException, DivideByZeroException или свой CustomException",
        "Несколько catch-блоков в правильном порядке (от частного к общему)",
        "Продемонстрировать все три сценария",
        "В конце — finally с выводом «завершено»",
        "Объяснить в комментарии, почему порядок catch важен"
    };

    public void Run()
    {
        Console.WriteLine("--- Сценарий 1: FormatException ---");
        try
        {
            CheckValue("abc");
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Поймано FormatException: {ex.Message}");
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine($"Поймано DivideByZeroException: {ex.Message}");
        }
        catch (CustomException ex)
        {
            Console.WriteLine($"Поймано CustomException: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Поймано общее исключение: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Сценарий 1 завершено (finally).\n");
        }

        Console.WriteLine("--- Сценарий 2: DivideByZeroException ---");
        try
        {
            CheckValue("0");
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Поймано FormatException: {ex.Message}");
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine($"Поймано DivideByZeroException: {ex.Message}");
        }
        catch (CustomException ex)
        {
            Console.WriteLine($"Поймано CustomException: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Поймано общее исключение: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Сценарий 2 завершено (finally).\n");
        }

        Console.WriteLine("--- Сценарий 3: CustomException ---");
        try
        {
            CheckValue("-1");
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Поймано FormatException: {ex.Message}");
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine($"Поймано DivideByZeroException: {ex.Message}");
        }
        catch (CustomException ex)
        {
            Console.WriteLine($"Поймано CustomException: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Поймано общее исключение: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Сценарий 3 завершено (finally).");
        }

        /*
         * ПОЧЕМУ ПОРЯДОК CATCH ВАЖЕН:
         * 
         * Блоки catch проверяются сверху вниз. Как только срабатывает первый подходящий,
         * остальные игнорируются. 
         * 
         * Если поставить catch (Exception ex) в самый верх, он поймает ВСЕ исключения,
         * и до специфичных блоков (FormatException, DivideByZero) очередь просто не дойдет.
         * 
         * Поэтому мы всегда ставим исключения от самых конкретных (частных) к самым общим.
         */
    }

    // Метод, который может выбросить разные исключения
    private void CheckValue(string input)
    {
        // 1. Попытка парсинга (может выбросить FormatException)
        int number = int.Parse(input);

        // 2. Деление (может выбросить DivideByZeroException)
        int result = 100 / number;

        // 3. Своя бизнес-логика (может выбросить CustomException)
        if (result < 0)
        {
            throw new CustomException("Результат не может быть отрицательным.");
        }

        Console.WriteLine($"Успех! Результат: {result}");
    }
}

// Свое исключение
public class CustomException : Exception
{
    public CustomException(string message) : base(message)
    {
    }
}