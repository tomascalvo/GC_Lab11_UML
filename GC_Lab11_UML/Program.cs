using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace GC_Lab11_UML
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>
            {
                new Student("Alexandra Buchage", "1224 Alameda Dr.", "C# .NET",2020, 10000.00 ),
                new Staff("Stephen Stephenson", "217 Eldorado St.", "Grand Circus",  75000.00),
                new Student("Tomás Calvo", "567 Baskin St.", "C# .NET", 2020, 9000.00),
                new Staff("Joe Exotic", "100 Abuso Promenade", "Tigerland",  15000.00),
                new Student("Karen Karenina", "226 Tolstoy Ct.", "Front End", 2016, 8000.00 ),
                new Staff("Parthenon Huxley", "10009 Ambrosia Ct.", "Grand Circus", 75000.00)
            };
            Console.WriteLine("Welcome to UML.");
            PrintPersonell(people);
            bool loop = true;
            while (loop)
            {
                AddPerson(people);
                loop = AskYesOrNo("Would you like to add another person to the list?");
            }
            Console.WriteLine("Thank you for using UML. Goodbye.");
        }

        public static void PrintPersonell(List<Person> people)
        {
            Console.WriteLine("\nPersonell List: \n");
            foreach (Person person in people)
            {
                Console.WriteLine(person.ToString());
            }
            Console.WriteLine("");
        }

        public static void AddPerson(List<Person> people)
        {
            Console.WriteLine("Would you like to add a student or a staff member?");
            try
            {
                if (ValidationLoop("\"student\"", "or \"staff\"", @"([Ss][Tt][Uu][Dd][Ee][Nn][Tt])|([Ss][Tt][Aa]([Ff]){2})", out string response))
                {
                    if (response.ToLower().Trim() == "student")
                    {
                        ValidationLoop("student", "name", @"^(([A-Z]{1}[a-z]{0,29}-?)\s?){1,4}", out string name);
                        ValidationLoop("student", "address", @"\d+[ ](?:[A-Za-z0-9.-]+[ ]?)+(?:Avenue|Lane|Road|Boulevard|Drive|Street|Ave|Dr|Rd|Blvd|Ln|St)\.?,?\s((?:N|S|E|W|NE|SE|NW|SW)(\.?,?)\s)?(?:[A-Z][a-z.-]+[ ]?)+,?\s((Alabama|Alaska|Arizona|Arkansas|California|Colorado|Connecticut|Delaware|Florida|Georgia|Hawaii|Idaho|Illinois|Indiana|Iowa|Kansas|Kentucky|Louisiana|Maine|Maryland|Massachusetts|Michigan|Minnesota|Mississippi|Missouri|Montana|Nebraska|Nevada|New Hampshire|New Jersey|New Mexico|New York|North Carolina|North Dakota|Ohio|Oklahoma|Oregon|Pennsylvania|Rhode Island|South Carolina|South Dakota|Tennessee|Texas|Utah|Vermont|Virginia|Washington|West Virginia|Wisconsin|Wyoming)\s)?((AL|AK|AS|AZ|AR|CA|CO|CT|DE|DC|FM|FL|GA|GU|HI|ID|IL|IN|IA|KS|KY|LA|ME|MH|MD|MA|MI|MN|MS|MO|MT|NE|NV|NH|NJ|NM|NY|NC|ND|MP|OH|OK|OR|PW|PA|PR|RI|SC|SD|TN|TX|UT|VT|VI|VA|WA|WV|WI|WY)\s)?\b\d{5}(?:-\d{4})?\b", out string address);
                        //ValidateName("student ", out string name);
                        //ValidateAddress("student ", out string address);
                        ValidationLoop("student", "program", @"\w+", out string program);
                        ValidationLoop("student", "year", @"(19)\d{2}|(20)\d{2}", out int year);
                        //int year;
                        ValidationLoop("student", "fee", out double fee);
                        //double fee;
                        Person newStudent = new Student (name, address, program, year, fee);
                        people.Add(newStudent);
                    }
                    if (response.ToLower().Trim() == "staff")
                    {
                        ValidationLoop("staff", "name", @"^(([A-Z]{1}[a-z]{0,29}-?)\s?){1,4}", out string name);
                        ValidationLoop("staff", "address", @"\d+[ ](?:[A-Za-z0-9.-]+[ ]?)+(?:Avenue|Lane|Road|Boulevard|Drive|Street|Ave|Dr|Rd|Blvd|Ln|St)\.?,?\s(?:[A-Z][a-z.-]+[ ]?)+,?\s((Alabama|Alaska|Arizona|Arkansas|California|Colorado|Connecticut|Delaware|Florida|Georgia|Hawaii|Idaho|Illinois|Indiana|Iowa|Kansas|Kentucky|Louisiana|Maine|Maryland|Massachusetts|Michigan|Minnesota|Mississippi|Missouri|Montana|Nebraska|Nevada|New Hampshire|New Jersey|New Mexico|New York|North Carolina|North Dakota|Ohio|Oklahoma|Oregon|Pennsylvania|Rhode Island|South Carolina|South Dakota|Tennessee|Texas|Utah|Vermont|Virginia|Washington|West Virginia|Wisconsin|Wyoming)\s)?((AL|AK|AS|AZ|AR|CA|CO|CT|DE|DC|FM|FL|GA|GU|HI|ID|IL|IN|IA|KS|KY|LA|ME|MH|MD|MA|MI|MN|MS|MO|MT|NE|NV|NH|NJ|NM|NY|NC|ND|MP|OH|OK|OR|PW|PA|PR|RI|SC|SD|TN|TX|UT|VT|VI|VA|WA|WV|WI|WY)\s)?\b\d{5}(?:-\d{4})?\b", out string address);
                        ValidationLoop("staff", "school", @"\w+", out string school);
                        ValidationLoop("staff", "pay", out double pay);
                        Staff newStaff = new Staff(name, address, school, pay);
                        people.Add(newStaff);
                    }
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            PrintPersonell(people);
        }

        public static bool AskYesOrNo(string question)
        {
            bool loop = true;
            int counter = 0;
            while (loop && counter < 3)
            {
                Console.WriteLine(question);
                string response = Console.ReadLine().ToLower();
                Regex yesTrue = new Regex(@"(y(es)?)|(t(rue)?)");
                Regex noFalse = new Regex(@"(n(o)?)|(f(alse)?)");
                try
                {
                    if (yesTrue.IsMatch(response))
                    {
                        loop = false;
                        return true;
                    }
                    if (noFalse.IsMatch(response))
                    {
                        loop = false;
                        return false;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid entry. {2 - counter} attempts remaining.");
                        counter++;
                    }
                }
                catch (FormatException e)
                { 
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Response attempts exhausted.");
                }
            }
            return false;
        }

        public static bool ValidateWRegEx(string valueDescription, string regExString, string input)
        {
            Regex regEx = new Regex($@"{regExString}");

            if (regEx.IsMatch(input))
            {
                //Console.WriteLine($"{input} is a {valueDescription}.");
                return true;
            }
            else
            {
                //Console.WriteLine($"{input} is not a {valueDescription}.");
                return false;
            }
        }

        public static bool ValidationLoop(string valueModifier, string valueDescription, string regEx, out string input)
        {
            bool valid = false;
            int counter = 0;
            while (!valid && counter < 3)
            {
                Console.WriteLine($"Enter {valueModifier} {valueDescription}: ");
                input = Console.ReadLine().Trim();
                if (ValidateWRegEx(valueDescription, regEx, input))
                {
                    valid = true;
                    return valid;
                }
                else
                {
                    Console.WriteLine($"Invalid entry. {2 - counter} attempts remaining.");
                    counter++;
                }
            }
            input = null;
            return false;
        }

        public static bool ValidationLoop(string valueModifier, string valueDescription, out int result)
        {
            bool valid = false;
            int counter = 0;
            while (!valid && counter < 2)
            {
                Console.WriteLine($"Enter {valueModifier} {valueDescription}: ");;
                if (int.TryParse(Console.ReadLine().Trim(), out int parsedInt))
                {
                    result = parsedInt;
                    valid = true;
                    return true;
                }
                else
                {
                    Console.WriteLine($"Invalid entry. {2 - counter} attempts remaining.");
                    counter++;
                }
            }
            result = -1;
            return false;
        }

        public static bool ValidationLoop(string valueModifier, string valueDescription, string regEx, out int result)
        {
            bool valid = false;
            string input = null;
            int counter = 0;
            while (!valid && counter < 2)
            {
                Console.WriteLine($"Enter {valueModifier} {valueDescription}: ");
                input = Console.ReadLine().Trim();
                if (ValidateWRegEx(valueDescription, regEx, input))
                {
                    result = int.Parse(input);
                    valid = true;
                    return true;
                }
                else
                {
                    Console.WriteLine($"Invalid entry. {2 - counter} attempts remaining.");
                    counter++;
                }
            }
            result = -1;
            return false;
        }

        public static bool ValidationLoop(string valueModifier, string valueDescription, out double result)
        {
            bool valid = false;
            int counter = 0;
            while (!valid && counter < 2)
            {
                Console.WriteLine($"Enter {valueModifier} {valueDescription}: "); ;
                if (double.TryParse(Console.ReadLine().Trim(), out double parsedDouble))
                {
                    result = parsedDouble;
                    valid = true;
                    return true;
                }
                else
                {
                    Console.WriteLine($"Invalid entry. {2 - counter} attempts remaining.");
                    counter++;
                }
            }
            result = -1;
            return false;
        }

        public static bool ValidateWord(string input)
        {
            bool isValid = ValidateWRegEx("word", @"\w+", input);
            return isValid;
        }

        public static bool ValidateName(string nameModifier, out string name)
        {
            bool valid = false;
            name = null;
            int counter = 0;
            while (!valid && counter < 2)
            {
                Console.WriteLine($"Enter {nameModifier}name: ");
                name = Console.ReadLine().Trim();
                if (ValidateWRegEx("name", @"[A-Z]{1}[a-z]{0,29}", name))
                {
                    valid = true;
                    return valid;
                }
                else
                {
                    Console.WriteLine($"Invalid entry. {2-counter} attempts remaining.");
                    counter++;
                }
            }
            return false;
        }

        public static bool ValidateAddress(string addressModifier, out string address)
        {
            Console.WriteLine($"Enter {addressModifier}address: ");
            address = Console.ReadLine().Trim();
            bool isValid = ValidateWRegEx("date", @"\d+[ ](?:[A-Za-z0-9.-]+[ ]?)+(?:Avenue|Lane|Road|Boulevard|Drive|Street|Ave|Dr|Rd|Blvd|Ln|St)\.?,?\s(?:[A-Z][a-z.-]+[ ]?)+,?\s((Alabama|Alaska|Arizona|Arkansas|California|Colorado|Connecticut|Delaware|Florida|Georgia|Hawaii|Idaho|Illinois|Indiana|Iowa|Kansas|Kentucky|Louisiana|Maine|Maryland|Massachusetts|Michigan|Minnesota|Mississippi|Missouri|Montana|Nebraska|Nevada|New Hampshire|New Jersey|New Mexico|New York|North Carolina|North Dakota|Ohio|Oklahoma|Oregon|Pennsylvania|Rhode Island|South Carolina|South Dakota|Tennessee|Texas|Utah|Vermont|Virginia|Washington|West Virginia|Wisconsin|Wyoming)\s)?((AL|AK|AS|AZ|AR|CA|CO|CT|DE|DC|FM|FL|GA|GU|HI|ID|IL|IN|IA|KS|KY|LA|ME|MH|MD|MA|MI|MN|MS|MO|MT|NE|NV|NH|NJ|NM|NY|NC|ND|MP|OH|OK|OR|PW|PA|PR|RI|SC|SD|TN|TX|UT|VT|VI|VA|WA|WV|WI|WY)\s)?\b\d{5}(?:-\d{4})?\b", address);
            return isValid;
        }
    }
}