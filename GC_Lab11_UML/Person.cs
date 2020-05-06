using System;
using System.Collections.Generic;
using System.Text;
namespace GC_Lab11_UML
{
    public class Person
    {
        private string _name;
        private string _address;

        public string Name { get; set; }
        public string Address { get; set; }

        public Person(string Name, string Address)
        {
            this.Name = Name;
            this.Address = Address;
        }
        public Person() { }

        public override string ToString()
        {
            string output = $"Person {Name}, {Address}";
            return output;
        }
    }
}
