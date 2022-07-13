using System;

namespace Homework7
{
    class Program
    {
        static void Main()
        {
            string path = @"data.txt";
            Console.WriteLine("1. Add new employee\n" +
                "2. List all employees\n" +
                "3. List employee by ID\n" +
                "4.Remove by ID\n" +
                "5.Change by ID\n" +
                "6.Order date by descending\n"+
                "or 'exit' to close program");
            Repo repo = new Repo();
            repo.RepoPath(path);
            Employee temp = new Employee();
            string id;
            string answer = "";
            List<string> info;

            while (answer != "exit")
            {
                Console.WriteLine("Action?");
                answer = Console.ReadLine();
                switch (answer)
                {
                    case "1":
                        //Adding new worker
                        info = Functions.GetInfo();
                        Employee employee = new Employee("", DateTime.Now.ToString("dd.MM.yyyy HH-mm-ss"), info[0], info[1], info[2], info[3], info[4]);
                        repo.AppendWorker(employee);
                        break;
                    case "2":
                        //Print db to console
                        repo.PrintDbToConsole();
                        break;
                    case "3":
                        //Search worker by ID
                        Console.WriteLine("find ID?");
                        id = Console.ReadLine();
                        temp = repo.GetEmployeeByID(id).Keys.ToArray()[0];
                        Console.WriteLine(temp.Print());
                        break;
                    case "4":
                        //Delete worker by ID
                        Console.WriteLine("ID for delete?");
                        id = Console.ReadLine();
                        repo.RemoveByID(id);
                        break;
                    case "5":
                        //Change info about worker by ID
                        Console.WriteLine("ID for change?");
                        id = Console.ReadLine();
                        info = Functions.GetInfo();
                        temp = new Employee(id, "date", info[0], info[1], info[2], info[3], info[4]);
                        repo.ChangeByID(id, temp);
                        break;
                    case "6":
                        repo.SortDB();
                        break;
                    default: break;
                }
            }
            Console.WriteLine("Done\ndb location: " + Path.GetFullPath(path));
            Console.ReadKey();
        }
    }
}