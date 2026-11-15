using System;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

class Program
{
    // Строка подключения
    private static string connectionString = "Server=localhost\\SQLEXPRESS;Database=PracticeDB;Trusted_Connection=True;TrustServerCertificate=True;";

    static async Task Main(string[] args)
    {
        // Проверка переменной окружения (по заданию)
        string envString = Environment.GetEnvironmentVariable("PRACTICE_DB_CONNECTION");
        if (!string.IsNullOrEmpty(envString))
        {
            connectionString = envString;
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("--- МЕНЮ C# + SQL (Задания 28, 29, 30) ---");
            Console.WriteLine("1. Задание 28: Вывести список сотрудников (SELECT)");
            Console.WriteLine("2. Задание 29: Добавить нового сотрудника (INSERT)");
            Console.WriteLine("3. Задание 30: Отчет по отделам (GROUP BY)");
            Console.WriteLine("0. Выход");
            Console.Write("\nВыберите пункт: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await Task28_Select();
                    break;
                case "2":
                    await Task29_Insert();
                    break;
                case "3":
                    await Task30_Report();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadLine();
        }
    }

    // ========================================================
    // ЗАДАНИЕ 28: SELECT
    // ========================================================
    static async Task Task28_Select()
    {
        Console.WriteLine("\n--- Список сотрудников ---");
        string query = "SELECT EmployeeName, Salary FROM Employees";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    Console.WriteLine($"{"Имя сотрудника",-25} | {"Зарплата",10}");
                    Console.WriteLine(new string('-', 40));

                    while (await reader.ReadAsync())
                    {
                        string name = reader.GetString(0);
                        decimal salary = reader.GetDecimal(1);
                        Console.WriteLine($"{name,-25} | {salary,10:C}");
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Ошибка SQL: {ex.Message}");
        }
    }

    // ========================================================
    // ЗАДАНИЕ 29: Параметризованный INSERT
    // ========================================================
    static async Task Task29_Insert()
    {
        Console.WriteLine("\n--- Добавление сотрудника ---");

        string newName = "Новый Сотрудник";
        decimal newSalary = 50000m;
        int deptId = 1;

        string query = @"INSERT INTO Employees (EmployeeName, Salary, HireDate, DepartmentId) 
                         VALUES (@name, @salary, GETDATE(), @deptId);";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", newName);
                    command.Parameters.AddWithValue("@salary", newSalary);
                    command.Parameters.AddWithValue("@deptId", deptId);

                    int rows = await command.ExecuteNonQueryAsync();
                    Console.WriteLine($"Успешно добавлено строк: {rows}. Сотрудник: {newName}");
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Ошибка базы данных (SqlException): {ex.Message}");
        }
    }

    // ========================================================
    // ЗАДАНИЕ 30: Мини-отчет по отделам
    // ========================================================
    static async Task Task30_Report()
    {
        Console.WriteLine("\n--- Отчет по отделам ---");

        string query = @"
            SELECT d.DepartmentName, COUNT(e.EmployeeId) AS EmpCount, AVG(e.Salary) AS AvgSalary 
            FROM Departments d 
            LEFT JOIN Employees e ON d.DepartmentId = e.DepartmentId 
            GROUP BY d.DepartmentName";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    // Вывод заголовка таблицы с выравниванием
                    Console.WriteLine($"{"Отдел",-15} | {"Сотрудников",-12} | {"Средняя зп",-12}");
                    Console.WriteLine(new string('-', 45));

                    bool hasRows = false;
                    while (await reader.ReadAsync())
                    {
                        hasRows = true;
                        string dept = reader.GetString(0);
                        int count = reader.GetInt32(1);

                        // Обработка NULL: если в отделе 0 сотрудников, AVG вернет NULL
                        string avgSalary = reader.IsDBNull(2) ? "N/A" : reader.GetDecimal(2).ToString("C");

                        Console.WriteLine($"{dept,-15} | {count,-12} | {avgSalary,-12}");
                    }

                    if (!hasRows)
                        Console.WriteLine("Нет данных для отображения.");
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Ошибка БД: {ex.Message}");
        }
    }
}