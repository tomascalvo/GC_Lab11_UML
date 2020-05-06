using System;
using System.Collections.Generic;
using System.Text;
namespace GC_Lab11_UML
{
    class Student : Person
    {
        private string _program;
        private int _year;
        private double _fee;

        public string Program { get; set; }
        public int Year { get; set; }
        public double Fee { get; set; }

        public Student(string Name, string Address, string Program, int Year, double Fee) : base(Name, Address)
        {
            this.Program = Program;
            this.Year = Year;
            this.Fee = Fee;
        }

        public override string ToString()
        {
            string output = $"Student {Name}, {Address}, {Program}, {Year}, {Fee}.";
            return output;
        }
    }
}
