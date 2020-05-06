using System;
using System.Collections.Generic;
using System.Text;
namespace GC_Lab11_UML
{
    class Staff : Person
    {
        private string _school;
        private double _pay;

        public string School { get; set; }
        public double Pay { get; set; }

        public Staff(string Name, string Address, string School, double Pay) : base(Name, Address)
        {
            this.School = School;
            this.Pay = Pay;
        }

        public override string ToString()
        {
            string output = $"Staff {Name}, {Address}, {School}, {Pay}.";
            return output;
        }
    }
}
