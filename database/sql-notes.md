# SQL notes
> Учебная БД: PracticeDB. Скрипт создания — `init.sql`.

## Task 01: Создание базы и таблиц

### SQL

```sql
IF DB_ID('PracticeDB') IS NOT NULL
BEGIN
    ALTER DATABASE PracticeDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE PracticeDB;
END
GO
CREATE DATABASE PracticeDB;
GO
USE PracticeDB;
GO
CREATE TABLE Departments (
    DepartmentId   INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(100) NOT NULL
);
CREATE TABLE Employees (
    EmployeeId   INT PRIMARY KEY IDENTITY(1,1),
    EmployeeName NVARCHAR(100) NOT NULL,
    Salary       DECIMAL(10,2) NOT NULL,
    HireDate     DATE NOT NULL,
    DepartmentId INT NOT NULL,
    ManagerId    INT NULL,
    CONSTRAINT FK_Employees_Departments FOREIGN KEY (DepartmentId)
        REFERENCES Departments(DepartmentId),
    CONSTRAINT FK_Employees_Manager FOREIGN KEY (ManagerId)
        REFERENCES Employees(EmployeeId)
);
CREATE TABLE Projects (
    ProjectId   INT PRIMARY KEY IDENTITY(1,1),
    ProjectName NVARCHAR(100) NOT NULL,
    StartDate   DATE NOT NULL
);
CREATE TABLE EmployeeProjects (
    EmployeeId INT NOT NULL,
    ProjectId  INT NOT NULL,
    Hours      INT NOT NULL DEFAULT 0,
    CONSTRAINT PK_EmployeeProjects PRIMARY KEY (EmployeeId, ProjectId),
    CONSTRAINT FK_EP_Employees FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId),
    CONSTRAINT FK_EP_Projects  FOREIGN KEY (ProjectId)  REFERENCES Projects(ProjectId)
);
GO
Результат:
Все 4 таблицы успешно созданы.
Вывод:
Повторил синтаксис DDL, создание первичных и внешних ключей.

##Task 02: Заполнение данными (INSERT)
INSERT INTO Departments (DepartmentName) VALUES
    (N'Разработка'), (N'Аналитика'), (N'Тестирование'), (N'Менеджмент');

INSERT INTO Employees (EmployeeName, Salary, HireDate, DepartmentId, ManagerId) VALUES
    (N'Анна Иванова',    65000, '2020-03-15', 1, NULL),
    (N'Пётр Сидоров',    72000, '2019-07-01', 1, NULL),
    (N'Мария Кузнецова', 55000, '2021-11-20', 1, NULL),
    (N'Иван Петров',     48000, '2023-02-10', 1, NULL),
    (N'Ольга Смирнова',  61000, '2020-09-05', 2, NULL),
    (N'Дмитрий Орлов',   58000, '2022-04-18', 2, NULL),
    (N'Елена Волкова',   52000, '2023-06-01', 2, NULL),
    (N'Сергей Морозов',  45000, '2021-12-12', 3, NULL),
    (N'Наталья Соколова',43000, '2024-01-15', 3, NULL),
    (N'Алексей Лебедев', 90000, '2018-05-25', 4, NULL);

UPDATE Employees SET ManagerId = 1  WHERE EmployeeId = 4;
UPDATE Employees SET ManagerId = 2  WHERE EmployeeId = 3;
UPDATE Employees SET ManagerId = 5  WHERE EmployeeId = 6;
UPDATE Employees SET ManagerId = 10 WHERE EmployeeId = 9;

INSERT INTO Projects (ProjectName, StartDate) VALUES
    (N'CRM System', '2023-01-10'), (N'Mobile App', '2023-05-01'), (N'Data Warehouse', '2022-11-20'), (N'AI Assistant', '2024-02-01'), (N'Legacy Refactor', '2021-06-15');

INSERT INTO EmployeeProjects (EmployeeId, ProjectId, Hours) VALUES
    (1, 1, 120), (1, 3, 80), (2, 1, 150), (2, 4, 60), (3, 2, 100), (4, 2, 90), (4, 5, 40), (5, 3, 110), (6, 3, 70), (6, 4, 50), (7, 2, 95), (8, 5, 130), (10, 4, 200),(10, 1, 30);
Результат:
Данные добавлены во все таблицы.
Вывод: 
Научился заполнять таблицы через INSERT.

##Task 03: SELECT + WHERE
-- 1. Зарплата > 50000
SELECT EmployeeName, Salary FROM Employees WHERE Salary > 50000;
-- 2. Нанятые в 2023
SELECT EmployeeName, HireDate FROM Employees WHERE YEAR(HireDate) = 2023;
-- 3. Имя на 'А'
SELECT EmployeeName FROM Employees WHERE EmployeeName LIKE N'А%';
-- 4. Отдел 2
SELECT EmployeeName, DepartmentId FROM Employees WHERE DepartmentId = 2;
-- 5. Зарплата 40000-60000
SELECT EmployeeName, Salary FROM Employees WHERE Salary BETWEEN 40000 AND 60000;
Результат:
7 сотрудников с зарплатой > 50000.
2 сотрудника наняты в 2023 (Петров, Волкова).
1 сотрудник на «А» (Анна Иванова).
3 сотрудника в отделе 2.
5 сотрудников в диапазоне 40000–60000.
Вывод:
Повторил операторы WHERE, LIKE, BETWEEN.

##Task 04: ORDER BY + TOP
-- 1. Топ-3 по зарплате
SELECT TOP 3 EmployeeName, Salary FROM Employees ORDER BY Salary DESC;
-- 2. Топ-5 самых давних
SELECT TOP 5 EmployeeName, HireDate FROM Employees ORDER BY HireDate ASC;
-- 3. Сортировка по отделу и зарплате
SELECT EmployeeName, DepartmentId, Salary FROM Employees ORDER BY DepartmentId ASC, Salary DESC;
Результат:
Топ-3: Лебедев (90000), Сидоров (72000), Иванова (65000).
Топ-5 самых давних: Лебедев, Сидоров, Иванова, Смирнова, Морозов.
Вывод:
Научился сортировать по нескольким полям и использовать TOP.

##Task 05: Агрегация
SELECT 
    COUNT(*) AS TotalEmployees,
    SUM(Salary) AS TotalSalary,
    ROUND(AVG(Salary), 2) AS AvgSalary,
    MIN(Salary) AS MinSalary,
    MAX(Salary) AS MaxSalary,
    COUNT(CASE WHEN Salary > 50000 THEN 1 END) AS CountOver50k
FROM Employees;
Результат:
10 | 589000.00 | 58900.00 | 43000.00 | 90000.00 | 5
Вывод:
Использовал COUNT, SUM, AVG, MIN, MAX. Конструкция CASE внутри COUNT позволяет считать условно.

##Task 06: GROUP BY
SELECT DepartmentId, COUNT(*) AS EmployeeCount, AVG(Salary) AS AvgSalary, SUM(Salary) AS TotalSalary
FROM Employees
GROUP BY DepartmentId
ORDER BY AVG(Salary) DESC;
Результат:
Отдел 4: 1, 90000, 90000. Отдел 1: 4, 60000, 240000. Отдел 2: 3, 57000, 171000. Отдел 3: 2, 44000, 88000.
Вывод:
GROUP BY агрегирует данные по группам. Важно, чтобы в SELECT были только группируемые поля или агрегаты.

##Task 07: GROUP BY + HAVING
-- 1. Отделы с > 2 сотрудников
SELECT DepartmentId, COUNT(*) AS Count FROM Employees GROUP BY DepartmentId HAVING COUNT(*) > 2;
-- 2. Средняя зарплата > 55000
SELECT DepartmentId, AVG(Salary) AS AvgSalary FROM Employees GROUP BY DepartmentId HAVING AVG(Salary) > 55000;
-- 3. Суммарная зарплата > 150000
SELECT DepartmentId, SUM(Salary) AS TotalSalary FROM Employees GROUP BY DepartmentId HAVING SUM(Salary) > 150000;
Результат:
Отделы 1 и 2 (больше 2 сотрудников).
Отделы 1 и 4 (средняя зарплата > 55000).
Отделы 1 и 4 (суммарная зарплата > 150000).
Вывод:
HAVING фильтрует уже сгруппированные данные (в отличие от WHERE, который фильтрует строки до группировки).

##Task 08: INNER JOIN
SELECT e.EmployeeName, d.DepartmentName, e.Salary
FROM Employees e
INNER JOIN Departments d ON e.DepartmentId = d.DepartmentId
ORDER BY d.DepartmentName ASC, e.Salary DESC;
Результат:
Все 10 сотрудников с названиями отделов.
Вывод:
INNER JOIN связывает таблицы по внешнему ключу, возвращая только строки, где есть совпадения в обеих таблицах.

##Task 09: LEFT JOIN
SELECT e.EmployeeId, e.EmployeeName
FROM Employees e
LEFT JOIN EmployeeProjects ep ON e.EmployeeId = ep.EmployeeId
WHERE ep.EmployeeId IS NULL;
Результат:
Сотрудники без проектов: Волкова (7), Соколова (9).
Вывод:
LEFT JOIN + IS NULL — отличный способ найти записи без связей.

##Task 10: Множественный JOIN
SELECT e.EmployeeName, d.DepartmentName, p.ProjectName, ep.Hours
FROM Employees e
INNER JOIN Departments d ON e.DepartmentId = d.DepartmentId
INNER JOIN EmployeeProjects ep ON e.EmployeeId = ep.EmployeeId
INNER JOIN Projects p ON ep.ProjectId = p.ProjectId
ORDER BY d.DepartmentName ASC, e.EmployeeName ASC, p.ProjectName ASC;
Результат:
14 строк с назначениями (сотрудник, отдел, проект, часы).
Вывод:
Соединил 4 таблицы через цепочку INNER JOIN. Это позволяет получить полную картину из связанных данных.

## Task 11: Self-join: иерархия руководителей

### SQL

```sql
SELECT 
    e.EmployeeName AS Employee, 
    m.EmployeeName AS Manager
FROM Employees e
LEFT JOIN Employees m ON e.ManagerId = m.EmployeeId
ORDER BY e.EmployeeId;
Результат:
Выведены все 10 сотрудников. У Анны, Петра и Алексея руководитель NULL, у остальных указаны имена менеджеров.
Вывод:
Освоил Self-join: таблица соединяется сама с собой, чтобы получить данные о руководителе из той же таблицы.

##Task 12: Подзапросы в WHERE
-- Выше средней
SELECT EmployeeName, Salary 
FROM Employees 
WHERE Salary > (SELECT AVG(Salary) FROM Employees);
-- С максимальной зарплатой
SELECT EmployeeName, Salary 
FROM Employees 
WHERE Salary = (SELECT MAX(Salary) FROM Employees);
Результат:
1.Выведены сотрудники с зарплатой выше 58900.
2.Выведен Алексей Лебедев (90000)
Вывод:
Подзапросы в WHERE позволяют динамически вычислять условие (например, среднюю зарплату) без хардкода чисел.

##Task 13: Скалярные подзапросы в SELECT
SELECT 
    e.EmployeeName, 
    e.Salary, 
    e.DepartmentId,
    ROUND(e.Salary - (SELECT AVG(sub.Salary) FROM Employees sub WHERE sub.DepartmentId = e.DepartmentId), 2) AS DiffFromDeptAvg
FROM Employees e;
Результат:
Для каждого сотрудника показана разница между его зарплатой и средней по его отделу. Например, у Лебедева разница 0 (он один в отделе), у Ивановой +5000.
Вывод:
Скалярный подзапрос в SELECT выполняется для каждой строки основного запроса, что позволяет вычислять аналитику по группам.

##Task 14: EXISTS / NOT EXISTS
-- Есть проекты
SELECT e.EmployeeName 
FROM Employees e
WHERE EXISTS (SELECT 1 FROM EmployeeProjects ep WHERE ep.EmployeeId = e.EmployeeId);

-- Нет проектов
SELECT e.EmployeeName 
FROM Employees e
WHERE NOT EXISTS (SELECT 1 FROM EmployeeProjects ep WHERE ep.EmployeeId = e.EmployeeId);
Результат:
1.Выведены все, у кого есть назначения.
2.Выведены Волкова и Соколова.
Вывод:
EXISTS эффективнее JOIN для проверки наличия связей, так как останавливается при первом совпадении.

##Task 15: UNION / UNION ALL
-- UNION
SELECT EmployeeName, Salary, HireDate FROM Employees WHERE Salary > 60000
UNION
SELECT EmployeeName, Salary, HireDate FROM Employees WHERE HireDate < '2022-01-01';

-- UNION ALL
SELECT EmployeeName, Salary, HireDate FROM Employees WHERE Salary > 60000
UNION ALL
SELECT EmployeeName, Salary, HireDate FROM Employees WHERE HireDate < '2022-01-01';
Результат:
UNION вернул уникальные строки (без дубликатов), UNION ALL вернул строк больше, так как сотрудники, подходящие под оба условия, задвоились.
Вывод:
UNION тратит ресурсы на удаление дубликатов, UNION ALL работает быстрее и сохраняет все строки.

##Task 16: UPDATE
-- ДО
SELECT EmployeeId, EmployeeName, Salary FROM Employees WHERE DepartmentId = 1;

-- Обновление
UPDATE Employees SET Salary = Salary * 1.10 WHERE DepartmentId = 1;
UPDATE Employees SET Salary = Salary + 5000 WHERE HireDate < '2022-01-01';

-- ПОСЛЕ
SELECT EmployeeId, EmployeeName, Salary FROM Employees WHERE DepartmentId = 1;
Результат:
Зарплата сотрудников отдела 1 увеличилась на 10%, а у нанятых до 2022 года — на 5000.
Вывод:
UPDATE с WHERE изменяет только целевые строки. Обязательно делать SELECT до и после для проверки.

##Task 17: DELETE
-- Удаление связей с 0 часов
DELETE FROM EmployeeProjects WHERE Hours = 0;

-- Удаление связей сотрудника 4
DELETE FROM EmployeeProjects WHERE EmployeeId = 4;

-- Удаление самого сотрудника
DELETE FROM Employees WHERE EmployeeId = 4;

-- Проверка
SELECT * FROM Employees WHERE EmployeeId = 4;
Результат:
Сотрудник 4 удален. Связи также удалены, ошибка внешнего ключа (FK) не возникла.
Вывод:
При удалении родительской записи сначала нужно удалять дочерние (связи), чтобы не нарушать ограничение FOREIGN KEY.

##Task 18: Транзакции
-- ROLLBACK
BEGIN TRANSACTION;
UPDATE Employees SET Salary = 999999 WHERE EmployeeId = 1;
SELECT Salary FROM Employees WHERE EmployeeId = 1;
ROLLBACK TRANSACTION;
SELECT Salary FROM Employees WHERE EmployeeId = 1;

-- COMMIT
BEGIN TRANSACTION;
UPDATE Employees SET Salary = 70000 WHERE EmployeeId = 1;
COMMIT TRANSACTION;
SELECT Salary FROM Employees WHERE EmployeeId = 1;
Результат
При выполнении `ROLLBACK TRANSACTION` изменение зарплаты откатилось: до отката `SELECT` показал 999999.00, после отката снова 65000.00. 
При выполнении `COMMIT TRANSACTION` изменение сохранилось: зарплата стала 70000.00 и осталась такой после завершения транзакции.
 Вывод
ROLLBACK отменяет изменения, COMMIT фиксирует их.

##Task 19: Представления (VIEW)
IF OBJECT_ID('vw_EmployeeDetails', 'V') IS NOT NULL
    DROP VIEW vw_EmployeeDetails;
GO

CREATE VIEW vw_EmployeeDetails AS
SELECT e.EmployeeName, d.DepartmentName, p.ProjectName, ep.Hours
FROM Employees e
INNER JOIN Departments d ON e.DepartmentId = d.DepartmentId
INNER JOIN EmployeeProjects ep ON e.EmployeeId = ep.EmployeeId
INNER JOIN Projects p ON ep.ProjectId = p.ProjectId;
GO

SELECT * FROM vw_EmployeeDetails;
Результат:
SELECT * FROM vw_EmployeeDetails вернул 14 строк (как в Task 10).
Вывод:
VIEW — это сохраненный запрос. Он не хранит данные, а каждый раз выполняет SELECT заново.

##Task 20: Индексы
SET STATISTICS IO ON;

-- ДО
SELECT * FROM Employees WHERE DepartmentId = 1 AND Salary > 50000;

-- Создание
CREATE INDEX IX_Employees_DepartmentId ON Employees(DepartmentId);
CREATE INDEX IX_Employees_Salary ON Employees(Salary);

-- ПОСЛЕ
SELECT * FROM Employees WHERE DepartmentId = 1 AND Salary > 50000;
Результат:
После создания индексов logical reads в панели "Сообщения" уменьшилось (например, с 3 до 2)
Вывод:
Индексы ускоряют поиск по колонкам из WHERE за счет создания дополнительной структуры (дерева), но замедляют INSERT/UPDATE (так как индексы надо перестраивать)

## Task 21: Хранимые процедуры

### SQL

```sql
-- 1. Простая процедура
CREATE PROCEDURE sp_GetEmployeesByDepartment 
    @DeptId INT
AS
BEGIN
    SELECT EmployeeName, Salary 
    FROM Employees 
    WHERE DepartmentId = @DeptId;
END;
GO

-- Вызов:
EXEC sp_GetEmployeesByDepartment @DeptId = 1;

-- 2. Процедура с OUTPUT
CREATE PROCEDURE sp_GetAvgSalaryByDepartment
    @DeptId INT,
    @AvgSalary DECIMAL(10,2) OUTPUT
AS
BEGIN
    SELECT @AvgSalary = AVG(Salary) 
    FROM Employees 
    WHERE DepartmentId = @DeptId;
END;
GO

-- Вызов с OUTPUT:
DECLARE @Avg DECIMAL(10,2);
EXEC sp_GetAvgSalaryByDepartment @DeptId = 1, @AvgSalary = @Avg OUTPUT;
SELECT @Avg AS DepartmentAvgSalary;
Результат:
Первая процедура вывела 4 сотрудников отдела 1 (Иванова, Сидоров, Кузнецова, Петров). Вторая процедура через OUTPUT вернула среднюю зарплату по отделу 1 = 60000.00.
Вывод:
Хранимые процедуры позволяют сохранять SQL-код на сервере. Параметры OUTPUT удобны для возврата одного значения (например, агрегата) без вывода целой таблицы.

##Task 22: Функции: скалярная и табличная
-- 1. Скалярная функция
CREATE FUNCTION fn_GetAnnualSalary (@MonthlySalary DECIMAL(10,2))
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN @MonthlySalary * 12;
END;
GO

SELECT EmployeeName, Salary, dbo.fn_GetAnnualSalary(Salary) AS AnnualSalary
FROM Employees;

-- 2. Табличная функция
CREATE FUNCTION fn_GetEmployeesBySalaryRange (@Min DECIMAL(10,2), @Max DECIMAL(10,2))
RETURNS TABLE
AS
RETURN
(
    SELECT EmployeeName, Salary 
    FROM Employees 
    WHERE Salary BETWEEN @Min AND @Max
);
GO

SELECT * FROM dbo.fn_GetEmployeesBySalaryRange(50000, 70000);
Результат:
Скалярная функция умножила зарплату на 12. Табличная функция вернула список сотрудников с зарплатой от 50000 до 70000 (Иванова, Кузнецова, Смирнова, Орлов, Волкова)
Вывод:
Скалярные функции возвращают одно значение и вызываются в SELECT. Табличные возвращают таблицу и вызываются в FROM как обычные таблицы.

##Task 23: Оконные функции
SELECT 
    EmployeeName, 
    DepartmentId, 
    Salary,
    ROW_NUMBER() OVER(ORDER BY Salary DESC) AS RowNum,
    RANK() OVER(PARTITION BY DepartmentId ORDER BY Salary DESC) AS RankInDept,
    DENSE_RANK() OVER(PARTITION BY DepartmentId ORDER BY Salary DESC) AS DenseRankInDept
FROM Employees;
Результат:
Ранги расставлены корректно. ROW_NUMBER нумерует строки подряд (1, 2, 3...). RANK и DENSE_RANK ведут себя одинаково, так как в базе нет сотрудников с одинаковой зарплатой в одном отделе. Если бы они были, RANK пропустил бы цифру, а DENSE_RANK — нет.
Вывод:
Оконные функции считают значения, не сворачивая строки в группы (в отличие от GROUP BY). PARTITION BY делит данные на окна (отделы).

##Task 24: Оконные агрегаты
SELECT 
    EmployeeName, 
    DepartmentId, 
    Salary,
    SUM(Salary) OVER(PARTITION BY DepartmentId) AS TotalDeptSalary,
    AVG(Salary) OVER(PARTITION BY DepartmentId) AS AvgDeptSalary,
    Salary - AVG(Salary) OVER(PARTITION BY DepartmentId) AS DiffFromAvg
FROM Employees;
Результат:
Для каждого сотрудника выведена сумма и средняя зарплата по его отделу, а также отклонение от средней. Например, у Ивановой (60000) отклонение +5000 от средней по отделу (55000).
Вывод:
Оконные агрегаты (OVER) избавляют от необходимости писать сложные подзапросы с GROUP BY, позволяя сразу выводить и строки, и итоги.

##Task 25: CTE (Common Table Expressions)
;WITH AvgSalary AS (
    SELECT AVG(Salary) AS CompanyAvg FROM Employees
)
SELECT EmployeeName, Salary 
FROM Employees
CROSS JOIN AvgSalary
WHERE Employees.Salary > AvgSalary.CompanyAvg;
Результат:
Запрос вернул тех же сотрудников, что и в Task 12 (зарплата выше 58900). CROSS JOIN присоединил к каждой строке вычисленную среднюю.
Вывод:
CTE (WITH) читается сверху вниз, что делает сложные запросы более понятными. Это отличная альтернатива вложенным подзапросам.

##Task 26: CASE
SELECT 
    EmployeeName, 
    Salary,
    CASE 
        WHEN Salary > 60000 THEN 'High'
        WHEN Salary BETWEEN 40000 AND 60000 THEN 'Medium'
        ELSE 'Low'
    END AS SalaryLevel,
    HireDate,
    CASE
        WHEN HireDate < '2020-01-01' THEN 'Senior'
        WHEN HireDate BETWEEN '2020-01-01' AND '2022-12-31' THEN 'Mid'
        ELSE 'Junior'
    END AS Tenure
FROM Employees;
Результат:
Каждому сотруднику присвоен уровень зарплаты и стажа. Например, Лебедев (90000) — High, Senior. Соколова (43000) — Medium, Junior.
Вывод:
CASE работает как if-else. Он позволяет создавать вычисляемые колонки и заменять сложные JOIN с справочниками.

##Task 27: Работа с датами
SELECT 
    EmployeeName, 
    HireDate,
    DATEDIFF(YEAR, HireDate, GETDATE()) AS YearsOfService,
    DATEDIFF(MONTH, HireDate, GETDATE()) AS MonthsOfService,
    DATEADD(YEAR, 1, HireDate) AS OneYearAfterHire,
    FORMAT(HireDate, 'dd.MM.yyyy') AS FormattedDate
FROM Employees;
Результат:
Для каждого сотрудника вычислен полный стаж в годах и месяцах, выведена дата через год после найма и отформатирована дата в вид дд.ММ.гггг.
Вывод:
DATEDIFF считает разницу между датами, DATEADD прибавляет интервал, FORMAT приводит дату к строке в нужном формате. Это стандартный набор для работы с датами в T-SQL.



## Task 28: C# + SQL: подключение и SELECT

### Что сделано
Реализовано консольное приложение на C# с использованием пакета `Microsoft.Data.SqlClient`. 
Строка подключения берется из переменной окружения `PRACTICE_DB_CONNECTION`. 
Использовано асинхронное открытие соединения (`OpenAsync`), данные читаются через `SqlDataReader`. 
Ошибки обрабатываются через `catch (SqlException ex)`.

### Результат
Приложение подключается к `PracticeDB` и выводит список сотрудников (имя и зарплату) в консоль.

---

## Task 29: C# + SQL: параметризованный INSERT

 Что сделано
Реализован параметризованный INSERT. Значения передаются через `Parameters.AddWithValue` (`@name`, `@salary`, `@deptId`), что полностью исключает SQL-инъекции (никакой конкатенации строк). Ошибки перехватываются через `SqlException`.
 Результат
При выборе пункта 2 в консоли появляется сообщение: `Успешно добавлено строк: 1. Сотрудник: Новый Сотрудник`. В базе данных появляется новая запись.
 Вывод
Параметризованные запросы — единственный безопасный способ передачи данных из C# в SQL.

---

## Task 30: C# + SQL: мини-отчёт по отделам

 Что сделано
Написан метод для вывода отчета. Запрос с `LEFT JOIN` и `GROUP BY` выполняется через `SqlCommand.ExecuteReader`. 
Вывод в консоль отформатирован в виде таблицы с выравниванием. 
Для отделов без сотрудников (где `AVG` возвращает `NULL`) выводится `N/A`. 
Обрабатывается пустой результат.

 Результат
Отчет выводится с корректными цифрами (количество сотрудников и средняя зарплата для каждого отдела), форматирование ровное.