using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace ConsoleApplication
{
    class Program
    {
        static List<Employees> employees = new List<Employees>();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n");
                Console.WriteLine("Key option:");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. View Employee");
                Console.WriteLine("3. Update Employee");
                Console.WriteLine("4. Delete Employee");
                Console.WriteLine("5. Exit");
                Console.WriteLine("\n");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddEmployee();
                        break;
                    case "2":
                        ViewEmployee();
                        break;
                    case "3":
                        UpdateEmployee();
                        break;
                    case "4":
                        DeleteEmployee();
                        break;
                    case "5":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine("Invalid option. Please try again.");
                        Console.WriteLine("---------------------------------");
                        break;
                }
            }
        }

        #region "METHOD"
        static void AddEmployee()
        {
            Console.WriteLine("Enter employee's id:");
            string id = GetValidString();

            if (IsDuplicateId(id))
            {
                Console.WriteLine("---------------------------------------------------------------------------");
                Console.WriteLine($"Employee with id '{id}' already exists. Duplicate entries are not allowed.");
                Console.WriteLine("---------------------------------------------------------------------------");
                return;
            }

            Console.WriteLine("Enter employee's name:");
            string name = GetValidString();

            Console.WriteLine("Enter employee's Birthdate:");
            DateTime birthdate = GetValidBirthDate();

            Employees NewEmployees = new Employees { EmployeeId = id, FullName = name, BirthDate = birthdate };

            employees.Add(NewEmployees);

            Console.WriteLine("-----------------------------");
            Console.WriteLine("Employee added successfully!");
            Console.WriteLine("-----------------------------");
        }
        
        static void ViewEmployee()
        {
            Console.WriteLine("Employee List:");

            ConsoleTable table = new ConsoleTable("EmployeeId", "FullName", "BirthDate");

            foreach(var emp in employees)
            {
                table.AddRow(emp.EmployeeId, emp.FullName, emp.BirthDate.ToString("dd-MMM-yy"));
            }
            Console.WriteLine(table.ToString());
        }

        static void UpdateEmployee()
        {
            Console.WriteLine("Enter the id of the employee to update:");
            string IdToUpdate = Console.ReadLine();

            Employees EmployeeToUpdate = employees.Find(e => e.EmployeeId == IdToUpdate);
            if(EmployeeToUpdate != null)
            {
                Console.WriteLine("Enter the new fullname for the employee:");
                string NewFullName = GetValidString();

                EmployeeToUpdate.FullName = NewFullName;

                Console.WriteLine("------------------------------");
                Console.WriteLine("Employee updated successfully!");
                Console.WriteLine("------------------------------");
            }
            else
            {
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine($"Employee with id {IdToUpdate} not found.");
                Console.WriteLine("-----------------------------------------");
            }
        }

        static void DeleteEmployee()
        {
            Console.WriteLine("Enter the id of the employee to delete:");
            string IdToDelete = Console.ReadLine();

            Employees EmployeeToDelete = employees.Find(e => e.EmployeeId == IdToDelete);
            if(EmployeeToDelete != null)
            {
                employees.Remove(EmployeeToDelete);
                Console.WriteLine("------------------------------");
                Console.WriteLine("Employee deleted successfully!");
                Console.WriteLine("------------------------------");
            }
            else
            {
                Console.WriteLine($"Employee with id {IdToDelete} not found.");
            }
        }

        #endregion

        #region "VALIDATION"
        static bool IsDuplicateId(string id)
        {
            return employees.Exists(e => e.EmployeeId == id);
        }
        static string GetValidString()
        {
            string input;
            while (true)
            {
                Console.WriteLine("Enter a non-empty string:");
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("-----------------------------------------------");
                    Console.WriteLine("Invalid input. Please enter a non-empty string.");
                    Console.WriteLine("-----------------------------------------------");
                }
            }

            return input;
        }
        static DateTime GetValidBirthDate()
        {
            DateTime birthdate;
            while (true)
            {
                Console.WriteLine("Enter employee's birthdate (yyyy-MM-dd):");
                if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out birthdate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("------------------------------------------------------------------------");
                    Console.WriteLine("Invalid date format. Please enter a valid date in the format yyyy-MM-dd.");
                    Console.WriteLine("------------------------------------------------------------------------");
                }
            }
            return birthdate;
        }

        #endregion
    }

    class Employees
    {
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
