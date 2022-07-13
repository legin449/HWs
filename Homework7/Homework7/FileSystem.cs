using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework7
{
    public class Repo
    {
        private Employee[] employees;
        string path;
        int index = 0;
        string[] titles;
        /// <summary>
        /// Init repo with path
        /// </summary>
        /// <param name="path"></param>
        public void RepoPath(string path)
        {
            this.path = path;
            if (!File.Exists(path))
            {
                
                File.AppendAllText(path, "ID#Дата добавления записи#ФИО#Возраст#Рост#Дата рождения#Место рождения");
                
            }
            this.titles = new string[0];
            this.employees = new Employee[1];
            this.Load();
        }
        /// <summary>
        /// resize array of workers if index outside of bounds
        /// </summary>
        /// <param name="Flag"></param>
        public void Resize(bool Flag)
        {
            Array.Resize(ref this.employees, this.employees.Length * 2);
        }
        /// <summary>
        /// Add worker to array
        /// </summary>
        /// <param name="employee"></param>
        public void Add(Employee employee)
        {
            Resize(this.index >= employees.Length);
            employees[this.index] = employee;
            this.index++;
        }
        /// <summary>
        /// Load workers to array
        /// </summary>
        public void Load()
        {
            using (StreamReader sr = new StreamReader(this.path))
            {
                titles = sr.ReadLine().Split('#');


                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split('#');

                    Add(new Employee(args[0], args[1], args[2], args[3], args[4], args[5], args[6]));
                }
            }
        }
        /// <summary>
        /// Print DB to console
        /// </summary>
        public void PrintDbToConsole()
        {
            Console.WriteLine($"{this.titles[0],1} {this.titles[1],3} {this.titles[2],3} {this.titles[3],3} {this.titles[4],3} {this.titles[5],3} {this.titles[6],3}");

            for (int i = 0; i < index; i++)
            {

                Console.WriteLine(this.employees[i].Print());
            }
        }
        /// <summary>
        /// Add new worker to DB
        /// </summary>
        /// <param name="employee"></param>
        public void AppendWorker(Employee employee)
        {
            try
            {
                int index = Convert.ToInt32(File.ReadAllLines(this.path).ToList()[-1].Split("#")[0])+1;
            }
            catch (Exception)
            {
                int index = 1;
            }
            employee.id = Convert.ToString(index);
            Add(employee);
            File.AppendAllText(this.path, "\n"+employee.Write());
        }
        /// <summary>
        /// Getting concrete worker by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Dictionary: key - worker, value - index of worker</returns>
        public Dictionary<Employee,int> GetEmployeeByID(string id)
        {
            int count = 0;
            Employee emp = new Employee();
            Dictionary<Employee, int> empInfo = new Dictionary<Employee,int>();

            foreach (Employee worker in this.employees)
            {
                if (Convert.ToString(worker.id) == id)
                {
                    emp = worker;
                    break;
                }
                count++;
            }
            empInfo.Add(emp, count);
            return empInfo;
        }
        /// <summary>
        /// Remove worker by id
        /// </summary>
        /// <param name="id"></param>
        public void RemoveByID(string id)
        {
            int count = 0;
            List<String> lines = File.ReadAllLines(this.path).ToList();

            foreach (string line in lines)
            {
                if (line.Split("#")[0].Contains(id))
                {
                    break;
                }
                count++;
            }
            lines.RemoveAt(count);
            File.WriteAllText(this.path, String.Join("\n",lines));
            //Reload DB
            this.employees = new Employee[1];
            this.index = 0;
            Load();
        }
        /// <summary>
        /// Change info about worker by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="temp"></param>
        public void ChangeByID(string id, Employee temp)
        {
            Dictionary<Employee, int> empInfo = GetEmployeeByID(id);
            Employee emp = empInfo.Keys.ToArray()[0];
            int index;
            empInfo.TryGetValue(emp, out index);
            if (temp.name == "")
            {
                temp.name = emp.name;
            }
            if (temp.age == "")
            {
                temp.age = emp.age;
            }
            if (temp.height == "")
            {
                temp.height = emp.height;
            }
            if (temp.dateOfBirth == "")
            {
                temp.dateOfBirth = emp.dateOfBirth;
            }
            if (temp.placeOfBirth == "")
            {
                temp.placeOfBirth = emp.placeOfBirth;
            }
            temp.id = emp.id;
            temp.dateOfCreation = emp.dateOfCreation;
            emp = temp;
            List<Employee> listWorkers = this.employees.ToList();
            listWorkers[index] = temp;

            //Replace with new DB
            File.Delete(this.path);
            File.AppendAllText(this.path,
                ($"{this.titles[0],1}#{this.titles[1],3}#{this.titles[2],3}#{this.titles[3],3}#{this.titles[4],3}#{this.titles[5],3}#{this.titles[6],3}"));

            foreach (Employee worker in listWorkers)
            {
                if (worker.id == null)
                {
                    break;
                }
                File.AppendAllText(this.path, "\n" + worker.Write());
            }
            //Reload DB
            this.employees = new Employee[1];
            this.index = 0;
            Load();
        }
        /// <summary>
        /// Order date by descending
        /// </summary>
        public void SortDB()
        {
            string sortedPath = (Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path) + "_sorted.txt"));
            Employee[] workers = this.employees;
            File.AppendAllText(sortedPath, 
                ($"{this.titles[0],1}#{this.titles[1],3}#{this.titles[2],3}#{this.titles[3],3}#{this.titles[4],3}#{this.titles[5],3}#{this.titles[6],3}"));

            try
            {
                File.Delete(sortedPath);
            }
            catch (Exception)
            {

            }

            foreach (Employee worker in workers.OrderByDescending(x => x.dateOfCreation))
            {
                if (worker.id == null)
                {
                    break;
                }
                File.AppendAllText(sortedPath, "\n" + worker.Write());
            }
            Console.WriteLine("Sorted db location: "+ sortedPath);
        }
    }
}
