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
                Polynom c1 = new Polynom { n = Convert.ToInt32(Console.ReadLine()) };
                c1.p = new double[c1.n + 1];
                for (int i = 0; i <= c1.n; i++)
                {
                    Console.Write($"Введите {i}-й коэффициент: ");
                    c1.p[i] = Convert.ToDouble(Console.ReadLine());
                }
                //Console.WriteLine(c1.S);

                Console.Clear();
                Console.Write("Введите степень второго многочлена: ");
                Polynom c2 = new Polynom { n = Convert.ToInt32(Console.ReadLine()) };
                c2.p = new double[c2.n + 1];
                for (int i = 0; i <= c2.n; i++)
                {
                    Console.Write($"Введите {i}-й коэффициент: ");
                    c2.p[i] = Convert.ToDouble(Console.ReadLine());
                }
                Console.Clear();
                Console.WriteLine($"Первый многочлен: {c1.S}\nВторой многочлен: {c2.S}");

                Console.WriteLine($"Сумма многочленов равна: {Polynom.Sum(c1, c2).S}");
                Console.WriteLine($"Вычитание многочленов равно: {Polynom.Subtract(c1, c2).S}");
                Console.WriteLine($"Произведение многочленов равно: {Polynom.Multiplication(c1, c2).S}");
                Console.WriteLine($"Деление многочленов с остатком:\nЧастное: {Polynom.Divide(c1, c2, 'q').S}, остаток: {Polynom.Divide(c1, c2, 'r').S}");
                Console.WriteLine($"Отношение многочленов:\nA == B - {Polynom.Eq(c1, c2)}\nA != B - {Polynom.NoEq(c1, c2)}");
                Console.Write("Введите натуральную степень k: ");
                int k = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"Возведение многочленов в степень {k} равно:\n{Polynom.DegreeK(c1, k).S}\n{Polynom.DegreeK(c2, k).S}");
                foreach (double c in Polynom.Sum(c1, c2).p)
                    Console.WriteLine(c);
                Console.ReadKey();
            }
        }
    }
    class Polynom
    {
        public int n;
        public double[] p;
        string s = null;
        public string S
        {
            get
            {
                string[] tmp = new string[n + 1];//Временный массив одночленов
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
                if (tmp[0] != null && tmp[0][0] == '+')//Избавление от + в первом одночлене
                    tmp[0] = tmp[0].Remove(0, 1);
                s = string.Join("", tmp);//Объединение одночленов
                if (s != null && s[0] == '+')//Удаление плюса в начале
                    s = s.Remove(0, 1);
                s = s.Replace("+", " + ");//Марафет
                if (s != null && s[0] == '-')
                {
                    s = s.Remove(0, 1);
                    s = s.Replace("-", " - ");
                    s = s.Insert(0, "-");
                }
                else
                    s = s.Replace("-", " - ");
                return s;
            }
        }

        public static Polynom Sum(Polynom a, Polynom b)
        {
            Polynom res = new Polynom {n = a.n >= b.n ? a.n : b.n, p = new double[(a.n >= b.n ? a.n : b.n) + 1] };
            for (int i = 0; i <= b.n; i++)
                res.p[i] = i <= a.n ? a.p[i] : 0;
            for (int i = 0; i <= b.n; i++)
                res.p[i] += b.p[i];
            return res;
        }
        public static Polynom Subtract(Polynom a, Polynom b)
        {
            Polynom res = new Polynom { n = a.n >= b.n ? a.n : b.n, p = new double[(a.n >= b.n ? a.n : b.n) + 1] };
            for (int i = 0; i <= b.n; i++)
                res.p[i] = i <= a.n ? a.p[i] : 0;
            for (int i = 0; i <= b.n; i++)
                res.p[i] -= b.p[i];
            return res;
        }
        public static Polynom Multiplication(Polynom a, Polynom b)
        {
            Polynom res = new Polynom { n = a.n * b.n };
            res.p = new double[res.n + 1];
            for (int i = 0; i < res.n; i++)
                res.p[i] = 0;
            for (int i = 0; i <= a.n; i++)
                for (int j = 0; j <= b.n; j++)
                    res.p[i + j] += a.p[i] * b.p[j];
            return res;
        }
        public static Polynom Divide(Polynom a, Polynom b, char d)
        {// a - didvidend, b - divisor
            Polynom q = new Polynom();
            Polynom r = new Polynom();
            r.n = a.n;//remaider
            r.p = (double[])a.p.Clone();
            q.n = r.p.Length - b.p.Length;//quotient
            q.p = new double[r.p.Length - b.p.Length + 1];
            for(int i = 0; i < q.p.Length; i++)
            {
                double tmp = r.p[r.p.Length - i - 1] / b.p.Last();
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
            return a.p == b.p;
        }
        public static bool NoEq(Polynom a, Polynom b)
        {
            return a.p != b.p;
        }
        public static Polynom DegreeK(Polynom a, int k)
        {
            Polynom res = a;
            for (int i = 1; i < k; i++)
                res = Multiplication(res, a);
            return res;
        }

    }
}
