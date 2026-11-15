using System;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

class Program
{
    
    private static string connectionString = "Server=localhost\\SQLEXPRESS;Database=PracticeDB;Trusted_Connection=True;TrustServerCertificate=True;";

    static async Task Main(string[] args)
    {
        Console.WriteLine("--- Задание 28: C# + SQL SELECT ---");

        
        string envString = Environment.GetEnvironmentVariable("PRACTICE_DB_CONNECTION");
        if (!string.IsNullOrEmpty(envString))
        {
            connectionString = envString;
        }

        string query = "SELECT EmployeeName, Salary FROM Employees";

        try
        {
            // 1. Создаем подключение и открываем его АСИНХРОННО
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                Console.WriteLine("Успешное подключение к базе PracticeDB!\n");

                // 2. Выполняем запрос
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // 3. Читаем данные через SqlDataReader
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        Console.WriteLine($"{"Имя сотрудника",-25} | {"Зарплата",10}");
                        Console.WriteLine(new string('-', 40));

                        while (await reader.ReadAsync())
                        {
                            string name = reader.GetString(0);     // Колонка EmployeeName
                            decimal salary = reader.GetDecimal(1); // Колонка Salary

                            Console.WriteLine($"{name,-25} | {salary,10:C}");
                        }
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            // 4. Обработка ошибок базы данных
            Console.WriteLine($"Ошибка SQL: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Общая ошибка: {ex.Message}");
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadLine();
    }
}
