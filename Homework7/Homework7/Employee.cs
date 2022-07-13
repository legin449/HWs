using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework7
{
    public struct Employee
    {
        //ID
        //Дата и время добавления записи
        //Дата и время добавления записи
        //Ф.И.О.
        //Возраст
        //Рост
        //Дата рождения
        //Место рождения
        public Employee(string id, string dateOfCreation, string name, string age, string height, string dateOfBirth, string placeOfBirth)
        {
            this.id = id;
            this.dateOfCreation = dateOfCreation;
            this.name = name;
            this.age = age;
            this.height = height;
            this.dateOfBirth = dateOfBirth;
            this.placeOfBirth = placeOfBirth;
        }

        public string id { get; set; }
        public string dateOfCreation { get; set; }
        public string name { get; set; }
        public string age { get; set; }
        public string height { get; set; }
        public string dateOfBirth { get; set; }
        public string placeOfBirth { get; set; }

        public string Print()
        {
            return $"{this.id} {this.dateOfCreation,15} {this.name,15} {this.age,15} {this.height,15} {this.dateOfBirth,15} {this.placeOfBirth,15}";
        }
        public string Write()
        {
            return $"{this.id}#{this.dateOfCreation}#{this.name}#{this.age}#{this.height}#{this.dateOfBirth}#{this.placeOfBirth}";
        }
        public string Date()
        {
            return this.dateOfCreation;
        }
        
    }
}
