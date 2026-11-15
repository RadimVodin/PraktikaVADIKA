## Task 01: Создание базы и таблиц

# SQL notes
> Учебная БД: PracticeDB.

## Task 01: Создание базы и таблиц

### Запрос

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


## Task 02: Заполнение данными (INSERT)

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
GO
UPDATE Employees SET ManagerId = 1  WHERE EmployeeId = 4;
UPDATE Employees SET ManagerId = 2  WHERE EmployeeId = 3;
UPDATE Employees SET ManagerId = 5  WHERE EmployeeId = 6;
UPDATE Employees SET ManagerId = 10 WHERE EmployeeId = 9;
GO
INSERT INTO Projects (ProjectName, StartDate) VALUES
    (N'CRM System', '2023-01-10'), (N'Mobile App', '2023-05-01'), (N'Data Warehouse', '2022-11-20'), (N'AI Assistant', '2024-02-01'), (N'Legacy Refactor', '2021-06-15');
GO
INSERT INTO EmployeeProjects (EmployeeId, ProjectId, Hours) VALUES
    (1, 1, 120), (1, 3, 80), (2, 1, 150), (2, 4, 60), (3, 2, 100), (4, 2, 90), (4, 5, 40), (5, 3, 110), (6, 3, 70), (6, 4, 50), (7, 2, 95), (8, 5, 130), (10, 4, 200),(10, 1, 30);
GO


## Task 03: SELECT + WHERE

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
3 сотрудника в отделе 2 (Смирнова, Орлов, Волкова).
5 сотрудников в диапазоне 40000–60000.
Вывод:
Повторил операторы WHERE, LIKE, BETWEEN. Легко фильтровать данные по разным условиям.

## Task 04: ORDER BY + TOP

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
Научился сортировать по нескольким полям и использовать TOP для ограничения вывода.

## Task 05: Агрегация

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

## Task 06: GROUP BY

SELECT DepartmentId, COUNT(*) AS EmployeeCount, AVG(Salary) AS AvgSalary, SUM(Salary) AS TotalSalary
FROM Employees
GROUP BY DepartmentId
ORDER BY AVG(Salary) DESC;
Результат:
Отдел 4: 1, 90000, 90000. Отдел 1: 4, 60000, 240000. Отдел 2: 3, 57000, 171000. Отдел 3: 2, 44000, 88000.
Вывод:
GROUP BY агрегирует данные по группам. Важно, чтобы в SELECT были только группируемые поля или агрегаты.

## Task 07: GROUP BY + HAVING

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

## Task 08: INNER JOIN

SELECT e.EmployeeName, d.DepartmentName, e.Salary
FROM Employees e
INNER JOIN Departments d ON e.DepartmentId = d.DepartmentId
ORDER BY d.DepartmentName ASC, e.Salary DESC;
Результат:
Все 10 сотрудников с названиями отделов.
Вывод:
INNER JOIN связывает таблицы по внешнему ключу, возвращая только строки, где есть совпадения в обеих таблицах.

## Task 09: LEFT JOIN

SELECT e.EmployeeId, e.EmployeeName
FROM Employees e
LEFT JOIN EmployeeProjects ep ON e.EmployeeId = ep.EmployeeId
WHERE ep.EmployeeId IS NULL;
Результат:
Сотрудники без проектов: Волкова (7), Соколова (9)
Вывод:
LEFT JOIN + IS NULL — отличный способ найти записи без связей (например, сотрудников без проектов).

## Task 10: Множественный JOIN

SELECT e.EmployeeName, d.DepartmentName, p.ProjectName, ep.Hours
FROM Employees e
INNER JOIN Departments d ON e.DepartmentId = d.DepartmentId
INNER JOIN EmployeeProjects ep ON e.EmployeeId = ep.EmployeeId
INNER JOIN Projects p ON ep.ProjectId = p.ProjectId
ORDER BY d.DepartmentName ASC, e.EmployeeName ASC, p.ProjectName ASC;
Результат:
14 строк с назначениями (сотрудник, отдел, проект, часы). Например: Иванова (Разработка) — CRM System (120 ч)
Вывод:
Соединил 4 таблицы через цепочку INNER JOIN. Это позволяет получить полную картину из связанных данных.