using CSharpPracticum.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpPracticum.Tasks.Level3;

[TaskInfo(42, 3)]
public sealed class Task42_Cancellation : ITask
{
    public string Title => "CancellationToken — отмена через 2 сек";

    public string Description =>
        "Реализовать долгую операцию с поддержкой отмены.";

    public string[] Requirements => new[]
    {
        "async Task LongOperationAsync(CancellationToken token) — цикл с Task.Delay(500, token) 10 раз",
        "Создать CancellationTokenSource, запустить операцию, отменить через 2 секунды",
        "Обработать OperationCanceledException",
        "Вывести «операция отменена»"
    };

    public void Run()
    {
        Console.WriteLine("Запуск долгой операции (10 шагов по 500 мс)...\n");

        // Создаем источник токена отмены
        using CancellationTokenSource cts = new CancellationTokenSource();

        // Запускаем операцию
        Task task = LongOperationAsync(cts.Token);

        // Ждем 2 секунды и отменяем операцию
        Thread.Sleep(2000);
        Console.WriteLine("\n[Главный поток]: Прошло 2 секунды. Отправляю сигнал отмены...\n");
        cts.Cancel();

        try
        {
            // Ожидаем завершения задачи, чтобы поймать исключение
            task.GetAwaiter().GetResult();
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("[Главный поток]: Поймано OperationCanceledException.");
            Console.WriteLine("Статус: операция отменена.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Главный поток]: Произошла другая ошибка: {ex.Message}");
        }
    }

    private async Task LongOperationAsync(CancellationToken token)
    {
        for (int i = 1; i <= 10; i++)
        {
            // Проверяем, не попросили ли нас отменить операцию
            token.ThrowIfCancellationRequested();

            // Имитация работы (500 мс). Если придет отмена во время ожидания — выбросится исключение.
            await Task.Delay(500, token);

            Console.WriteLine($"Шаг {i} выполнен.");
        }

        Console.WriteLine("\nОперация успешно завершена (все 10 шагов).");
    }
}