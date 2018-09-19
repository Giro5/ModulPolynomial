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
            while (true)
            {
                Console.Write("Введите степень первого многочлена: ");
                Polynom c1 = new Polynom { p = new double[Convert.ToInt32(Console.ReadLine()) + 1] };
                for (int i = 0; i < c1.p.Length; i++)
                {
                    Console.Write($"Введите {i}-й коэффициент: ");
                    c1.p[i] = Convert.ToDouble(Console.ReadLine());
                    if (i == c1.p.Length - 1 && c1.p[i] == 0)
                    {
                        Console.WriteLine("Коэффициент последнего одночлена не может ровняться нулю");
                        i--;
                    }
                }
                Console.Clear();
                Console.Write("Введите степень второго многочлена: ");
                Polynom c2 = new Polynom { p = new double[Convert.ToInt32(Console.ReadLine()) + 1] };
                for (int i = 0; i < c2.p.Length; i++)
                {
                    Console.Write($"Введите {i}-й коэффициент: ");
                    c2.p[i] = Convert.ToDouble(Console.ReadLine());
                    if (i == c2.p.Length - 1 && c2.p[i] == 0)
                    {
                        Console.WriteLine("Коэффициент последнего одночлена не может ровняться нулю");
                        i--;
                    }
                }
                Console.Clear();
                Console.WriteLine($"Первый многочлен: {c1.S}\nВторой многочлен: {c2.S}\n");

                Console.WriteLine($"Сумма многочленов равна: {Polynom.Sum(c1, c2).S}\n");

                Console.WriteLine($"Вычитание многочленов равно: {Polynom.Subtract(c1, c2).S}\n");

                Console.WriteLine($"Произведение многочленов равно: {Polynom.Multiplication(c1, c2).S}\n");

                if (c1.p.Length >= c2.p.Length)
                    Console.WriteLine($"Деление многочленов с остатком:\nЧастное: {Polynom.Divide(c1, c2, 'q').S}, остаток: {Polynom.Divide(c1, c2, 'r').S}\n");
                else
                    Console.WriteLine("Деление невозможно поскольку второй многочлен имеет большую степень.");

                Console.WriteLine($"Отношение многочленов:\nA == B - {Polynom.Eq(c1, c2)}\nA != B - {Polynom.NoEq(c1, c2)}\n");

                Console.Write("Введите натуральную степень k: ");
                int k = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"Возведение многочленов в степень k = {k}, равно:\nA) {Polynom.DegreeK(c1, k).S}\nB) {Polynom.DegreeK(c2, k).S}\n");

                Console.WriteLine($"Производные многочленов равны:\n{Polynom.Derivative(c1).S}\n{Polynom.Derivative(c2).S}\n");

                Console.Write("Введите значение точки x0: ");
                double x0 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine($"Значение многочленов в точке x0 = {x0}, равно: {Polynom.SubstitutionOfX(Polynom.Derivative(c1), x0)}, {Polynom.SubstitutionOfX(Polynom.Derivative(c2), x0)}\n");

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
    class Polynom
    {
        public double[] p;
        string s = null;
        public string S
        {
            get
            {
                string[] tmp = new string[p.Length];//Временный массив одночленов
                for (int i = 0; i < tmp.Length; i++)
                {
                    tmp[i] = $"{p[i]}x^{i}";//Определение одночлена
                    if (tmp[i] != null && tmp[i][0] != '-')//Вставка в одночлен +, если нет -
                        tmp[i] = tmp[i].Insert(0, "+");
                    if (tmp[i] != null && tmp[i].Contains("^0"))//Избавляет одночлен от нулевой степени
                        tmp[i] = tmp[i].Remove(tmp[i].Length - 3);
                    if (tmp[i] != null && tmp[i].Contains("0x") && tmp[i][1] == '0')//Удаляет одночлены с нулевыми коэффициентами
                        tmp[i] = null;
                    if (tmp[i] != null && tmp[i].Contains("1x") && tmp[i][1] == '1' && tmp[i][2] != '1')//Удаляет единичные коэффициенты
                        tmp[i] = tmp[i].Remove(1, 1);
                    if (tmp[i] != null && tmp[i].Contains("^1"))//Избавляет одночлен от единичной степени
                        tmp[i] = tmp[i].Remove(tmp[i].Length - 2);
                    if (tmp[i] != null && tmp[i] == "+0")//Удаляет одночлены с нулевыми коэффициентами
                        tmp[i] = null;
                }
                if (tmp.Length == 0)//Если массив пустой
                    tmp = new string[] { "0" };
                if (tmp[0] != null && tmp[0][0] == '+')//Избавление от + в первом одночлене
                    tmp[0] = tmp[0].Remove(0, 1);
                s = string.Join("", tmp);//Объединение одночленов
                if (s != "" && s[0] == '+')//Удаление плюса в начале
                    s = s.Remove(0, 1);
                s = s.Replace("+", " + ");//Марафет
                if (s != "" && s[0] == '-')
                {
                    s = s.Remove(0, 1);
                    s = s.Replace("-", " - ");
                    s = s.Insert(0, "-");
                }
                else
                    s = s.Replace("-", " - ");
                if (s == "")//Ну так надо
                    s = "0";
                return s;
            }
        }

        public static Polynom Sum(Polynom a, Polynom b)
        {
            Polynom res = new Polynom { p = new double[a.p.Length >= b.p.Length ? a.p.Length : b.p.Length] };
            for (int i = 0; i < (a.p.Length >= b.p.Length ? a.p.Length : b.p.Length); i++)
                res.p[i] = i < a.p.Length ? a.p[i] : 0;
            for (int i = 0; i < b.p.Length; i++)
                res.p[i] += b.p[i];
            return res;
        }
        public static Polynom Subtract(Polynom a, Polynom b)
        {
            Polynom res = new Polynom { p = new double[a.p.Length >= b.p.Length ? a.p.Length : b.p.Length] };
            for (int i = 0; i < (a.p.Length >= b.p.Length ? a.p.Length : b.p.Length); i++)
                res.p[i] = i < a.p.Length ? a.p[i] : 0;
            for (int i = 0; i < b.p.Length; i++)
                res.p[i] -= b.p[i];
            return res;
        }
        public static Polynom Multiplication(Polynom a, Polynom b)
        {
            Polynom res = new Polynom { p = new double[a.p.Length * b.p.Length] };
            for (int i = 0; i < res.p.Length; i++)
                res.p[i] = 0;
            for (int i = 0; i < a.p.Length; i++)
                for (int j = 0; j < b.p.Length; j++)
                    res.p[i + j] += a.p[i] * b.p[j];
            return res;
        }
        public static Polynom Divide(Polynom a, Polynom b, char d)
        {// a - didvidend, b - divisor
            Polynom r = new Polynom { p = (double[])a.p.Clone() };//remaider
            Polynom q = new Polynom { p = new double[a.p.Length - b.p.Length + 1] };//quotient
            for (int i = 0; i < q.p.Length; i++)
            {
                double tmp = r.p[r.p.Length - i - 1] / b.p[b.p.Length - 1];
                q.p[q.p.Length - i - 1] = tmp;
                for (int j = 0; j < b.p.Length; j++)
                    r.p[r.p.Length - i - j - 1] -= tmp * b.p[b.p.Length - j - 1];
            }
            if (d == 'q')
                return q;
            else
                return r;
        }
        public static bool Eq(Polynom a, Polynom b)
        {
            if (a.p.Length != b.p.Length)
                return false;
            for (int i = 0; i < a.p.Length; i++)
                if (a.p[i] != b.p[i])
                    return false;
            return true;
        }
        public static bool NoEq(Polynom a, Polynom b) => !Eq(a, b);
        public static Polynom DegreeK(Polynom a, int k)
        {
            Polynom res = a;
            for (int i = 1; i < k; i++)
                res = Multiplication(res, a);
            return res;
        }
        public static Polynom Derivative(Polynom a)
        {
            Polynom res = new Polynom { p = new double[a.p.Length - 1] };
            for (int i = 0; i < res.p.Length; i++)
                res.p[i] = a.p[i];
            for (int i = 1; i <= res.p.Length; i++)
                res.p[i - 1] = a.p[i] * i;
            return res;
        }
        public static double SubstitutionOfX(Polynom a, double x0)
        {
            double res = 0;
            for (int i = 0; i < a.p.Length; i++)
                res += a.p[i] * Math.Pow(x0, i);
            return res;
        }
    }
}
