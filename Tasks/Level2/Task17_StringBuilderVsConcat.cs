using CSharpPracticum.Common;
using System.Diagnostics;
using System.Text;

namespace CSharpPracticum.Tasks.Level2;

[TaskInfo(17, 2)]
public sealed class Task17_StringBuilderVsConcat : ITask
{
    public string Title => "StringBuilder vs конкатенация (замер)";

    public string Description =>
        "Сравнить производительность конкатенации через + и StringBuilder.";

    public string[] Requirements => new[]
    {
        "Собрать строку из 10 000 чисел двумя способами",
        "Замерить время каждого через Stopwatch",
        "Вывести оба времени и разницу",
        "Объяснить в комментарии, почему StringBuilder быстрее"
    };

    public void Run()
    {
        const int count = 10000;

        // --- Способ 1: Конкатенация через + ---
        Stopwatch sw1 = Stopwatch.StartNew();
        string result1 = "";
        for (int i = 0; i < count; i++)
        {
            result1 += i.ToString();
        }
        sw1.Stop();

        // --- Способ 2: StringBuilder ---
        Stopwatch sw2 = Stopwatch.StartNew();
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < count; i++)
        {
            sb.Append(i);
        }
        string result2 = sb.ToString();
        sw2.Stop();

        // Проверка, что результаты равны
        bool isEqual = result1 == result2;

        Console.WriteLine($"Способ 1 (+): {sw1.ElapsedMilliseconds} мс");
        Console.WriteLine($"Способ 2 (StringBuilder): {sw2.ElapsedMilliseconds} мс");
        Console.WriteLine($"Разница: {sw1.ElapsedMilliseconds - sw2.ElapsedMilliseconds} мс");
        Console.WriteLine($"Результаты равны: {isEqual}");

        /*
         * ПОЧЕМУ STRINGBUILDER БЫСТРЕЕ:
         * 
         * Строки (string) в C# являются неизменяемыми (immutable). 
         * Когда мы делаем result1 += i.ToString(), каждый раз создается 
         * НОВАЯ строка в памяти, куда копируется старое содержимое + новое число.
         * Для 10 000 итераций это означает 10 000 выделений памяти и копирований.
         * 
         * StringBuilder же использует внутренний изменяемый буфер (массив символов).
         * При добавлении новых данных он просто дописывает их в этот буфер.
         * Когда буфер заполняется, его размер увеличивается (обычно в 2 раза), 
         * и происходит копирование, но это случается редко (логарифмически).
         * Именно поэтому StringBuilder в тысячи раз быстрее при больших объемах.
         */
    }
}