# SQL notes
> Учебная БД: PracticeDB. Скрипт создания — `init.sql`.
> Все запросы выполнялись в SSMS.

## Task 01 — Создание базы и таблиц
### SQL
(Выполнен init.sql)
### Результат
База PracticeDB создана, 4 таблицы, связи настроены.
### Вывод
Внешние ключи (ManagerId ссылается на EmployeeId) — сложно, но понятно.

## Task 02 — INSERT: заполнение данными
### SQL
(Выполнен init.sql)
### Результат
Departments: 4, Employees: 10, Projects: 5, EmployeeProjects: 14.
### Вывод
Использовал множественный INSERT.

## Task 03 — SELECT + WHERE
### SQL
```sql
-- 1. Сотрудники с зарплатой > 50000
SELECT EmployeeName, Salary FROM Employees WHERE Salary > 50000;

-- 2. Сотрудники, нанятые в 2023 году
SELECT EmployeeName, HireDate FROM Employees WHERE YEAR(HireDate) = 2023;

-- 3. Сотрудники, чьё имя начинается на «А»
SELECT EmployeeName FROM Employees WHERE EmployeeName LIKE N'А%';

-- 4. Сотрудники из отдела DepartmentId = 2
SELECT EmployeeName, DepartmentId FROM Employees WHERE DepartmentId = 2;

-- 5. Сотрудники с зарплатой в диапазоне 40000–60000
SELECT EmployeeName, Salary FROM Employees WHERE Salary BETWEEN 40000 AND 60000;