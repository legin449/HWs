using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW11
{
    class Client
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string Passport { get; set; }

        public Client(string FirstName, string LastName, string MiddleName, string PhoneNumber, string Passport)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.MiddleName = MiddleName;
            this.PhoneNumber = PhoneNumber;
            this.Passport = Passport;
        }
        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName} {this.MiddleName}";
        }
    }
}
