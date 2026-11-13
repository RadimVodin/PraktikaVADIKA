using CSharpPracticum.Common;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CSharpPracticum.Tasks.Level3;

[TaskInfo(41, 3)]
public sealed class Task41_WhenAll : ITask
{
    public string Title => "Task.WhenAll — параллельно 10 задач";

    public string Description =>
        "Запустить 10 задач параллельно и сравнить с последовательным выполнением.";

    public string[] Requirements => new[]
    {
        "Метод Task<int> WorkAsync(int id) — Task.Delay(500), возвращает id",
        "Последовательный запуск 10 задач в цикле с await",
        "Параллельный запуск через Task.WhenAll",
        "Замерить время обоих вариантов через Stopwatch",
        "Вывести оба времени и объяснить разницу"
    };

    public void Run()
    {
        const int taskCount = 10;

        // --- Вариант 1: Последовательное выполнение ---
        Console.WriteLine("--- Последовательное выполнение ---");
        Stopwatch sw1 = Stopwatch.StartNew();
        for (int i = 0; i < taskCount; i++)
        {
            // Ждем завершения каждой задачи перед запуском следующей
            int result = WorkAsync(i).GetAwaiter().GetResult();
            Console.WriteLine($"Задача {i} завершена, результат: {result}");
        }
        sw1.Stop();
        Console.WriteLine($"Последовательное время: {sw1.ElapsedMilliseconds} мс\n");

        // --- Вариант 2: Параллельное выполнение ---
        Console.WriteLine("--- Параллельное выполнение (Task.WhenAll) ---");
        Stopwatch sw2 = Stopwatch.StartNew();

        // Создаем массив задач, но НЕ ждем их по одной
        Task<int>[] tasks = new Task<int>[taskCount];
        for (int i = 0; i < taskCount; i++)
        {
            tasks[i] = WorkAsync(i);
        }

        // Ждем завершения ВСЕХ задач сразу
        int[] results = Task.WhenAll(tasks).GetAwaiter().GetResult();

        sw2.Stop();
        Console.WriteLine($"Все задачи завершены. Результаты: {string.Join(", ", results)}");
        Console.WriteLine($"Параллельное время: {sw2.ElapsedMilliseconds} мс");

        /*
         * ОБЪЯСНЕНИЕ РАЗНИЦЫ:
         * 
         * 1. Последовательное выполнение: мы запускаем задачу, ждем её 500 мс, 
         *    затем запускаем следующую. Для 10 задач это займет ~5000 мс (5 секунд).
         * 
         * 2. Параллельное выполнение (Task.WhenAll): мы запускаем все 10 задач 
         *    одновременно. Каждая "спит" 500 мс, но они делают это параллельно. 
         *    Поэтому общее время будет ~500 мс (в 10 раз быстрее).
         */
    }

    private async Task<int> WorkAsync(int id)
    {
        // Эмуляция полезной работы (задержка 500 мс)
        await Task.Delay(500);
        return id;
    }
}