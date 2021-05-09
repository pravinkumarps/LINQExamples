using System;
using System.Linq;
using System.Collections.Generic;

namespace LINQExamples_1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = Data.GetEmployees();
            List<Department> departments = Data.GetDepartments();

            // Method syntax
            //var result = employees.Select(e => new
            //{
            //    FullName = $"{e.FirstName} {e.LastName}",
            //    AnnualSalary = e.AnnualSalary
            //}).Where(x => x.AnnualSalary > 50000);

            //foreach (var item in result)
            //{
            //    Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,10}");
            //}

            // Query syntax
            //var result = from emp in employees
            //             where emp.AnnualSalary >= 50000
            //             select new
            //             {
            //                 FullName = $"{emp.FirstName} {emp.LastName}",
            //                 AnnualSalary = emp.AnnualSalary
            //             };

            //var result = from emp in employees.GetHighSalEmps()
            //             select new
            //             {
            //                 FullName = $"{emp.FirstName } {emp.LastName}",
            //                 AnnualSalary = emp.AnnualSalary
            //             };
            //employees.Add(new Employee {
            //     FirstName="Penu", LastName="Plava", AnnualSalary=90000.3m, DepartmentId=1, Id=20, IsManager=true
            //});

            //// This shows that LINQ is executing in deferred or lazy loading
            //foreach (var item in result)
            //{
            //    Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,10}");
            //}

            //// join operation - Method syntax
            //var result = departments.Join(employees,
            //    dep => dep.Id,
            //    emp => emp.DepartmentId,
            //    (dep, emp) => new 
            //    {
            //        FullName = $"{emp.FirstName } {emp.LastName}",
            //        AnnualSalary = emp.AnnualSalary,
            //        DepartmentName = dep.LongName
            //    });

            //foreach (var item in result)
            //{
            //    Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,10} {item.DepartmentName, -25}");
            //}

            // join operation (inner join) - query syntax
            //var result = from dep in departments
            //             join emp in employees
            //             on dep.Id equals emp.DepartmentId
            //             select new
            //             {
            //                 FullName = $"{emp.FirstName } {emp.LastName}",
            //                 AnnualSalary = emp.AnnualSalary,
            //                 DepartmentName = dep.LongName
            //             };

            //foreach (var item in result)
            //{
            //    Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,10} {item.DepartmentName,-25}");
            //}

            // Group join opertaion (left outer join on department and employees - Method syntax
            //var result = departments.GroupJoin(employees,
            //    dep => dep.Id,
            //    emp => emp.DepartmentId,
            //    (dep, empGroup) => new
            //    {
            //        DepartmentName= dep.LongName,
            //        Employees= empGroup
            //    });
            //foreach (var item in result)
            //{
            //    Console.WriteLine($"{item.DepartmentName}");
            //    foreach (var emp in item.Employees)
            //    {
            //        Console.WriteLine($"\t{emp.FirstName}, {emp.LastName}");
            //    }
            //}

            // Group join (left outer join ) - Query syntax
            var result = from dep in departments
                         join emp in employees
                         on dep.Id equals emp.DepartmentId
                         into employeeGrp
                         select new
                         {
                             Employees= employeeGrp,
                             DeptName= dep.LongName
                         };

            foreach (var item in result)
            {
                Console.WriteLine($"{item.DeptName}");
                foreach (var emp in item.Employees)
                {
                    Console.WriteLine($"\t{emp.FirstName}, {emp.LastName}");
                }
            }
        }
    }

    public static class Extenstions
    {
        public static IEnumerable<Employee> GetHighSalEmps(this IEnumerable<Employee> employees)
        {
            foreach (var emp in employees)
            {
                Console.WriteLine($"Accessing employee - {emp.FirstName} {emp.LastName}");
                if (emp.AnnualSalary > 50000)
                {
                    yield return emp;
                }
            }
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AnnualSalary { get; set; }
        public bool IsManager { get; set; }
        public int DepartmentId { get; set; }
    }

    public class Department
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
    }

    public static class Data
    {
        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            Employee employee = new Employee
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Jones",
                AnnualSalary = 60000.3m,
                IsManager = true,
                DepartmentId = 1
            };
            employees.Add(employee);
            employee = new Employee
            {
                Id = 2,
                FirstName = "Sarah",
                LastName = "Jameson",
                AnnualSalary = 80000.1m,
                IsManager = true,
                DepartmentId = 2
            };
            employees.Add(employee);
            employee = new Employee
            {
                Id = 3,
                FirstName = "Douglas",
                LastName = "Roberts",
                AnnualSalary = 40000.2m,
                IsManager = false,
                DepartmentId = 2
            };
            employees.Add(employee);
            employee = new Employee
            {
                Id = 4,
                FirstName = "Jane",
                LastName = "Stevens",
                AnnualSalary = 30000.2m,
                IsManager = false,
                DepartmentId = 2
            };
            employees.Add(employee);

            return employees;
        }

        public static List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();

            Department department = new Department
            {
                Id = 1,
                ShortName = "HR",
                LongName = "Human Resources"
            };
            departments.Add(department);
            department = new Department
            {
                Id = 2,
                ShortName = "FN",
                LongName = "Finance"
            };
            departments.Add(department);
            department = new Department
            {
                Id = 3,
                ShortName = "TE",
                LongName = "Technology"
            };
            departments.Add(department);

            return departments;
        }
    }
}
