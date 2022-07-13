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
        public string DateOfChanges { get; set; }
        public string ChangedBy { get; set; }
        public string WasChanged { get; set; }
        public string TypeOfChanges { get; set; }

        public Client(string FirstName, 
            string LastName,
            string MiddleName,
            string PhoneNumber,
            string Passport,
            string DateOfChanges,
            string ChangedBy,
            string WasChanged,
            string TypeOfChanges)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.MiddleName = MiddleName;
            this.PhoneNumber = PhoneNumber;
            this.Passport = Passport;
            this.DateOfChanges = DateOfChanges;
            this.ChangedBy = ChangedBy;
            this.WasChanged = WasChanged;
            this.TypeOfChanges = TypeOfChanges;
        }
        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName} {this.MiddleName}";
        }
        public string Serialization()
        {
            return $"{this.FirstName};{this.LastName};{this.MiddleName};{this.PhoneNumber};{this.Passport};{this.DateOfChanges};{this.ChangedBy};{this.WasChanged};{this.TypeOfChanges}";
        }
    }
}
