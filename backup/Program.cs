﻿using System;
using System.Linq;
using System.Collections.Generic;
namespace ModulPolynomial
{
    static class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Введите степень первого многочлена: ");
                Polynom c1 = new Polynom { p = new double[Convert.ToInt32(Console.ReadLine()) + 1] };
                for (int i = 0; i < c1.P.Length; i++)
                {
                    Console.Write($"Введите {i}-й коэффициент: ");
                    c1.p[i] = Convert.ToDouble(Console.ReadLine());
                    if (i == c1.P.Length - 1 && c1.p[i] == 0)
                        Console.WriteLine("Коэффициент последнего одночлена не может ровняться нулю", i--);
                }
                Console.Clear();
                Console.Write("Введите степень второго многочлена: ");
                Polynom c2 = new Polynom { p = new double[Convert.ToInt32(Console.ReadLine()) + 1] };
                for (int i = 0; i < c2.P.Length; i++)
                {
                    Console.Write($"Введите {i}-й коэффициент: ");
                    c2.p[i] = Convert.ToDouble(Console.ReadLine());
                    if (i == c2.P.Length - 1 && c2.p[i] == 0)
                        Console.WriteLine("Коэффициент последнего одночлена не может ровняться нулю", i--);
                }
                Console.Clear();
                Console.WriteLine($"Первый многочлен: {c1.S}\nВторой многочлен: {c2.S}\n");

                Console.WriteLine($"Сумма многочленов равна: {Polynom.Sum(c1, c2).S}\n");

                Console.WriteLine($"Вычитание многочленов равно: {Polynom.Subtract(c1, c2).S}\n");

                Console.WriteLine($"Произведение многочленов равно: {Polynom.Multiplication(c1, c2).S}\n");

                if (c1.P.Length > 1 && c2.P.Length > 1 && c1.P.Length >= c2.P.Length)
                    Console.WriteLine($"Деление многочленов с остатком:\nЧастное: {Polynom.Divide(c1, c2, Remnant.Quotient).S}, остаток: {Polynom.Divide(c1, c2, Remnant.Remaider).S}\n");
                else
                    Console.WriteLine("Деление невозможно.\n");

                Console.WriteLine($"Отношение многочленов:\nA == B - {Polynom.Eq(c1, c2)}\nA != B - {Polynom.NoEq(c1, c2)}\n");

                Console.Write("Введите натуральную степень k: ");
                int k = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"Возведение многочленов в степень k = {k}, равно:\nA) {Polynom.DegreeK(c1, k).S}\nB) {Polynom.DegreeK(c2, k).S}\n");

                Console.WriteLine($"Производные многочленов равны:\nA) {Polynom.Derivative(c1).S}\nB) {Polynom.Derivative(c2).S}\n");

                Console.Write("Введите значение точки x0: ");
                double x0 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine($"Значение многочленов в точке x0 = {x0}, равно:\nA) {Polynom.SubstitutionOfX(Polynom.Derivative(c1), x0)}\nB) {Polynom.SubstitutionOfX(Polynom.Derivative(c2), x0)}");

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
    /// <summary>
    /// Перечисление для указания необходимого избытка при делении: частного или остатка.
    /// </summary>
    public enum Remnant { Quotient, Remaider }
    /// <summary>
    /// Предоставляет набор подпрограмм для выполнения операций с многочленами от одной переменной.
    /// </summary>
    public class Polynom
    {
        /// <summary>
        /// Многочлен представлен следующим типом, коэффициент одночлена aₙxⁿ - элемент массива с индексом n, т.о. индекс соответствует степени x.
        /// </summary>
        //public double[] P { get; set; }//массив всех коэф многочлена
        private List<double> p;

        public Polynom(params double[] P) => this.p = new List<double>(P);

        public Double[] 
        /// <summary>
        /// Возвращает строку в стандартном виде многочлена: "aₒ + a₁x + a₂x² + ... + aₙxⁿ".
        /// </summary>
        public string S //ну задумка была проще
        {
            get
            {
                string[] tmp = new string[p.Length];//Временный массив одночленов
                for (int i = 0; i < tmp.Length; i++) //время 5:11
                    tmp[i] = p[i] != 0 ? ((p[i] > 0 ? (p[i] == 1 ? (i != 0 ? " + " : " + 1") : $" + {p[i]}") : (p[i] == -1 ? (i != 0 ? " - " : " - 1") : $" - {-1 * p[i]}")) + (i > 0 ? (i > 1 ? $"x^{i}" : "x") : "")) : "";
                string s = string.Join("", tmp) == "" ? " + 0" : string.Join("", tmp);//Объединение одночленов
                return s[1] == '+' ? s.Remove(0, 3) : "-" + s.Remove(0, 3);//удаление начальных пропусков
            }
        }
        /// <summary>
        /// Вычисялет сумму двух заданных многочленов.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Polynom Sum(Polynom a, Polynom b)
        {
            Polynom res = new Polynom { p = new double[a.p.Length >= b.p.Length ? a.p.Length : b.p.Length] };
            for (int i = 0; i < (a.p.Length >= b.p.Length ? a.p.Length : b.p.Length); i++)
                res.p[i] = (i < a.p.Length ? a.p[i] : 0) + (i < b.p.Length ? b.p[i] : 0);
            return res;
        }
        /// <summary>
        /// Вычисляет разность двух заданных многочленов.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Polynom Subtract(Polynom a, Polynom b)
        {
            Polynom res = new Polynom { p = new double[a.p.Length >= b.p.Length ? a.p.Length : b.p.Length] };
            for (int i = 0; i < (a.p.Length >= b.p.Length ? a.p.Length : b.p.Length); i++)
                res.p[i] = (i < a.p.Length ? a.p[i] : 0) - (i < b.p.Length ? b.p[i] : 0);
            return res;
        }
        /// <summary>
        /// Вычисляет произведение двух заданных многочленов.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Polynom Multiplication(Polynom a, Polynom b)
        {
            Polynom res = new Polynom { p = new double[a.p.Length * b.p.Length] };
            for (int i = 0; i < a.p.Length; i++)
                for (int j = 0; j < b.p.Length; j++)
                    res.p[i + j] += a.p[i] * b.p[j];
            return res;
        }
        /// <summary>
        /// Вычисляет отношение двух заданных многочленов, с возможностью получить частное или остаток.
        /// </summary>
        /// <param name="dvd - dividend - делимое"></param>
        /// <param name="dvs - divisor - делитель"></param>
        /// <param name="rem"></param>
        /// <returns></returns>
        public static Polynom Divide(Polynom dvd, Polynom dvs, Remnant rem)
        {
            if (dvs.p.Length > dvd.p.Length)
                return new Polynom { p = new double[0] };
            Polynom q = new Polynom { p = new double[dvd.p.Length - dvs.p.Length + 1] };
            Polynom r = new Polynom { p = (double[])dvd.p.Clone() };
            for (int i = 0; i < q.p.Length; i++)
            {
                double tmp = r.p[r.p.Length - i - 1] / dvs.p[dvs.p.Length - 1];       //dvd - dividend - делимое
                q.p[q.p.Length - i - 1] = tmp;                                        //dvs - divisor - делитель
                for (int j = 0; j < dvs.p.Length; j++)                                  //q - quotient - частное
                    r.p[r.p.Length - i - j - 1] -= tmp * dvs.p[dvs.p.Length - j - 1];   //r - remaider - остаток
            }
            if (rem == Remnant.Quotient)
                return q;
            else if (rem == Remnant.Remaider)
                return r;
            else
                return new Polynom { p = new double[0] };
        }
        /// <summary>
        /// Находит являются ли заданные многочлены равными.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Eq(Polynom left, Polynom right)
        {
            if (left.p.Length != right.p.Length)
                return false;
            for (int i = 0; i < left.p.Length; i++)
                if (left.p[i] != right.p[i])
                    return false;
            return true;
        }
        /// <summary>
        /// Находит являются ли заданные многочлены неравными.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool NoEq(Polynom left, Polynom right) => !Eq(left, right);
        /// <summary>
        /// Возводит заданный многочлен в степень <paramref name="deg"/>.
        /// </summary>
        /// <param name="polynom"></param>
        /// <param name="deg"></param>
        /// <returns></returns>
        public static Polynom DegreeK(Polynom polynom, int deg)
        {
            Polynom res = polynom;
            for (int i = 1; i < deg; i++)
                res = Multiplication(res, polynom);
            return res;
        }
        /// <summary>
        /// Вычисляет производную функцию заданного многочлена.
        /// </summary>
        /// <param name="polynom"></param>
        /// <returns></returns>
        public static Polynom Derivative(Polynom polynom)
        {
            Polynom res = new Polynom { p = new double[polynom.p.Length - 1] };
            for (int i = 1; i <= res.p.Length; i++)
                res.p[i - 1] = polynom.p[i] * i;
            return res;
        }
        /// <summary>
        /// Вычисляет значение заданного многочлена в точке <paramref name="xₒ"/>.
        /// </summary>
        /// <param name="polynom"></param>
        /// <param name="xₒ"></param>
        /// <returns></returns>
        public static double SubstitutionOfX(Polynom polynom, double x)
        {
            double res = 0;
            for (int i = 0; i < polynom.p.Length; i++)
                res += polynom.p[i] * Math.Pow(x, i);
            return res;
        }
    }
}
