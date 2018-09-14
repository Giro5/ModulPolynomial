using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModulPolynomial
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите первый многочлен: ");
            Polynom c1 = new Polynom { s = Console.ReadLine() };
            Console.WriteLine("Введите второй многочлен: ");
            Polynom c2 = new Polynom { s = Console.ReadLine() };

        }
    }
    class Polynom
    {
        public string s;
        public static Polynom Sum (Polynom a, Polynom b)
        {
            return new Polynom { };
        }
    }
}
