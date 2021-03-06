﻿using System;
using System.Linq;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Collections;
=======
>>>>>>> c8439d19b9f2bc62ea026e66856aba256f3696bc

namespace ModulPolynomial
{
    static class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Введите степень первого многочлена: ");
                double[] monomials = new double[Convert.ToInt32(Console.ReadLine()) + 1];
                for (int i = 0; i < monomials.Length; i++)
                {
                    Console.Write($"Введите {i}-й коэффициент: ");
                    monomials[i] = Convert.ToDouble(Console.ReadLine());
                    if (i == monomials.Length - 1 && monomials[i] == 0)
                        Console.WriteLine("Коэффициент последнего одночлена не может ровняться нулю", i--);
                }
                Polynom c1 = new Polynom(monomials);
                Console.Clear();
                Console.Write("Введите степень второго многочлена: ");
                Array.Resize(ref monomials, Convert.ToInt32(Console.ReadLine()) + 1);
                for (int i = 0; i < monomials.Length; i++)
                {
                    Console.Write($"Введите {i}-й коэффициент: ");
                    monomials[i] = Convert.ToDouble(Console.ReadLine());
                    if (i == monomials.Length - 1 && monomials[i] == 0)
                        Console.WriteLine("Коэффициент последнего одночлена не может ровняться нулю", i--);
                }
                Polynom c2 = new Polynom(monomials);
                Console.Clear();

                Console.WriteLine($"Первый многочлен: {c1}\nВторой многочлен: {c2}\n");
<<<<<<< HEAD

                Console.WriteLine($"Сумма многочленов равна: {c1 + c2}\n");

                Console.WriteLine($"Вычитание многочленов равно: {c1 - c2}\n");

                Console.WriteLine($"Произведение многочленов равно: {c1 * c2}\n");

=======

                Console.WriteLine($"Сумма многочленов равна: {c1 + c2}\n");

                Console.WriteLine($"Вычитание многочленов равно: {c1 - c2}\n");

                Console.WriteLine($"Произведение многочленов равно: {c1 * c2}\n");

>>>>>>> c8439d19b9f2bc62ea026e66856aba256f3696bc
                if (c1.Length > 1 && c2.Length > 1 && c1.Length >= c2.Length)
                    Console.WriteLine($"Деление многочленов с остатком:\nЧастное: {c1 / c2}, остаток: {c1 % c2}\n");
                else
                    Console.WriteLine("Деление невозможно.\n");

                Console.WriteLine($"Отношение многочленов:\nA == B - {c1 == c2}\nA != B - {c1 != c2}\n");

                Console.Write("Введите натуральную степень k: ");
                int k = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"Возведение многочленов в степень k = {k}, равно:\nA) {Polynom.Pow(c1, k)}\nB) {Polynom.Pow(c2, k)}\n");

                Console.WriteLine($"Производные многочленов равны:\nA) {Polynom.Derivative(c1)}\nB) {Polynom.Derivative(c2)}\n");

                Console.Write("Введите значение точки x0: ");
                double x0 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine($"Значение многочленов в точке x0 = {x0}, равно:\nA) {Polynom.Derivative(c1).ToNumber(x0)}\nB) {Polynom.ReplaceX(Polynom.Derivative(c2), x0)}");
<<<<<<< HEAD
                
=======

>>>>>>> c8439d19b9f2bc62ea026e66856aba256f3696bc
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
<<<<<<< HEAD
=======
    /// <summary>
    /// Перечисление для указания необходимого избытка при делении многочеленов: частного или остатка.
    /// </summary>
    public enum Remnant { Quotient, Remaider }
>>>>>>> c8439d19b9f2bc62ea026e66856aba256f3696bc

    /// <summary>
    /// Предоставляет набор подпрограмм для выполнения операций с многочленами от одной переменной.
    /// </summary>
    public struct Polynom : IEquatable<Polynom>
    {
        /// <summary>
        /// Многочлен представлен следующим типом, коэффициент одночлена aₙxⁿ - элемент массива с индексом n, т.о. индекс соответствует степени x.
        /// </summary>
        private double[] p;

        /// <summary>
        /// Конструктор инициализирующий многочлен с заданными коэффициентами.
        /// </summary>
<<<<<<< HEAD
        /// <param name="monoms">Массив значений с плавающей точкой содержащий коэффициенты многочлена.</param>
        public Polynom(params double[] Monomials)
        {
            for (int j = 0; j < Monomials.Length; j++)
                if (double.IsNaN(Monomials[j]) || double.IsInfinity(Monomials[j]))
                    throw new NotFiniteNumberException("Коэффициенты многочлена должны быть действительными числами не являющиеся бесконечностью.", Monomials[j]);
            double[] monoms = (double[])Monomials.Clone();
            int i = monoms.Length - 1;
            for (; i > 0; i--)
                if (monoms[i] != 0.0)
                    break;
            Array.Resize(ref monoms, i + 1);
            p = (double[])monoms.Clone();
=======
        /// <param name="Monomials">Массив значений с плавающей точкой содержащий коэффициенты многочлена.</param>
        public Polynom(double[] Monomials)
        {
            int i = Monomials.Length - 1;
            for (; i > 0; i--)
                if (Monomials[i] != 0.0)
                    break;
            Array.Resize(ref Monomials, i + 1);
            p = (double[])Monomials.Clone();
>>>>>>> c8439d19b9f2bc62ea026e66856aba256f3696bc
        }

        /// <summary>
        /// Возвращает массив значений с плавающей точкой содержащий коэффициенты многочлена.
        /// </summary>
        /// <returns>Массив содержащий коэффициенты многочлена.</returns>
        public double[] Get { get { return (double[])p.Clone(); } }

        /// <summary>
        /// Возвращает количество одночленов многочлена.
        /// </summary>
        /// <returns>Количество одночленов многочлена.</returns>
        public int Length { get { return p.Length; } }
<<<<<<< HEAD
        
        /// <summary>
        /// Sorry...
        /// </summary>
        /// <returns></returns>
        private string[] StringHelper()
        {
            string[] monoms = new string[Length];
            for (int i = 0; i < monoms.Length; i++)
                monoms[i] = p[i] != 0 ? (
                    (p[i] > 0 ? (p[i] == 1 ? (i != 0 ? " + " : " + 1") : $" + {p[i]}")
                        : (p[i] == -1 ? (i != 0 ? " - " : " - 1") : $" - {-1 * p[i]}"))
                    + (i > 0 ? (i > 1 ? $"x^{i}" : "x") : "")) : "";
            return monoms;
        }
=======
>>>>>>> c8439d19b9f2bc62ea026e66856aba256f3696bc

        /// <summary>
        /// Возвращает строку в стандартном виде многочлена: "aₒ + a₁x + a₂x² + ... + aₙxⁿ".
        /// </summary>
        public string S
        {
            get
            {
<<<<<<< HEAD
                string monomials = string.Join("", StringHelper());
                if (monomials == "")
                    monomials = " + 0";
                return monomials[1] == '+' ? monomials.Remove(0, 3) : "-" + monomials.Remove(0, 3);
            }
        }

        /// <summary>
        /// Возвращает строку в стандартном виде многочлена: "aₙxⁿ + ... + a₂x² + a₁x + aₒ".
        /// </summary>
        public string SReverse
        {
            get
            {
                string[] monoms = StringHelper();
                Array.Reverse(monoms);
                string monomials = string.Join("", monoms);
                if (monomials == "")
                    monomials = " + 0";
                return monomials[1] == '+' ? monomials.Remove(0, 3) : "-" + monomials.Remove(0, 3);
=======
                string[] tmp = new string[Length];
                for (int i = 0; i < tmp.Length; i++)
                    tmp[i] = p[i] != 0 ? ((p[i] > 0 ? (p[i] == 1 ? (i != 0 ? " + " : " + 1") : $" + {p[i]}") : (p[i] == -1 ? (i != 0 ? " - " : " - 1") : $" - {-1 * p[i]}")) + (i > 0 ? (i > 1 ? $"x^{i}" : "x") : "")) : "";
                string s = string.Join("", tmp) == "" ? " + 0" : string.Join("", tmp);
                return s[1] == '+' ? s.Remove(0, 3) : "-" + s.Remove(0, 3);
>>>>>>> c8439d19b9f2bc62ea026e66856aba256f3696bc
            }
        }

        /// <summary>
<<<<<<< HEAD
=======
        /// Возвращает строку в стандартном виде многочлена: "aₙxⁿ + ... + a₂x² + a₁x + aₒ".
        /// </summary>
        public string SReverse
        {
            get
            {
                string[] tmp = new string[Length];
                for (int i = 0; i < tmp.Length; i++)
                    tmp[i] = p[i] != 0 ? (
                        (p[i] > 0 ? (p[i] == 1 ? (i != 0 ? " + " : " + 1") : $" + {p[i]}") 
                            : (p[i] == -1 ? (i != 0 ? " - " : " - 1") : $" - {-1 * p[i]}")) 
                        + (i > 0 ? (i > 1 ? $"x^{i}" : "x") : "")) : "";
                string s = string.Join("", tmp) == "" ? " + 0" : string.Join("", tmp.Reverse());
                return s[1] == '+' ? s.Remove(0, 3) : "-" + s.Remove(0, 3);
            }
        }

        /// <summary>
>>>>>>> c8439d19b9f2bc62ea026e66856aba256f3696bc
        /// Преобразовывает значение этого экземпляра в эквивалентное ему строковое представление (см. свойство S).
        /// </summary>
        /// <returns>aₒ + a₁x + a₂x² + ... + aₙxⁿ</returns>
        public override string ToString() => S;

        /// <summary>
        /// Вычисляет значение заданного многочлена в точке <paramref name="x"/>.
        /// </summary>
        /// <param name="x">Переменная типа <see cref="double"/>, представляющая точку в которой надо найти значение заданного многочлена.</param>
        /// <returns>Значение с плавающей точкой.</returns>
        public double ToNumber(double x) => ReplaceX(this, x);

<<<<<<< HEAD
        public static Polynom operator -(Polynom value) => value * -1;
        public static Polynom operator ++(Polynom value) => value + 1;
        public static Polynom operator --(Polynom value) => value - 1;

=======
>>>>>>> c8439d19b9f2bc62ea026e66856aba256f3696bc
        /// <summary>
        /// Вычисялет сумму двух заданных многочленов.
        /// </summary>
        /// <param name="a">Первое слагаемый многочлен.</param>
        /// <param name="b">Второй слагаемый многочлен.</param>
        /// <returns>Многочлен типа <see cref="Polynom"/>.</returns>
        public static Polynom Sum(Polynom a, Polynom b)
        {
            Polynom res = new Polynom(a.Length > b.Length ? a.Get : b.Get);
            for (int i = 0; i < (a.Length <= b.Length ? a.Length : b.Length); i++)
                res.p[i] += (a.Length <= b.Length ? a.Get[i] : b.Get[i]);
            return res;
        }
        public static Polynom operator +(Polynom a, Polynom b) => Sum(a, b);
<<<<<<< HEAD
=======
        public static Polynom operator +(Polynom a, double b) => Sum(a, new Polynom(new[] { b }));
        public static Polynom operator +(double a, Polynom b) => Sum(new Polynom(new[] { a }), b);
>>>>>>> c8439d19b9f2bc62ea026e66856aba256f3696bc

        /// <summary>
        /// Вычисляет разность двух заданных многочленов.
        /// </summary>
        /// <param name="a">Уменьшаемый многочлен.</param>
        /// <param name="b">Вычитаемый многочлен.</param>
        /// <returns>Многочлен типа <see cref="Polynom"/>.</returns>
        public static Polynom Subtract(Polynom a, Polynom b)
        {
            Polynom res = new Polynom(a.Length > b.Length ? a.Get : b.Get);
            for (int i = 0; i < (a.Length >= b.Length ? a.Length : b.Length); i++)
                res.p[i] = (i < a.Length ? a.p[i] : 0) - (i < b.Length ? b.p[i] : 0);
            return res;
        }
        public static Polynom operator -(Polynom a, Polynom b) => Subtract(a, b);
<<<<<<< HEAD
=======
        public static Polynom operator -(Polynom a, double b) => Subtract(a, new Polynom(new[] { b }));
        public static Polynom operator -(double a, Polynom b) => Subtract(new Polynom(new[] { a }), b);
>>>>>>> c8439d19b9f2bc62ea026e66856aba256f3696bc

        /// <summary>
        /// Вычисляет произведение двух заданных многочленов.
        /// </summary>
        /// <param name="a">Первый многочлен множитель.</param>
        /// <param name="b">Второй многочлен множитель.</param>
        /// <returns>Многочлен типа <see cref="Polynom"/>.</returns>
        public static Polynom Multiply(Polynom a, Polynom b)
        {
            double[] res = new double[a.Length * b.Length];
            for (int i = 0; i < a.Length; i++)
                for (int j = 0; j < b.Length; j++)
                    res[i + j] += a.p[i] * b.p[j];
            return (new Polynom(res));
        }
        public static Polynom operator *(Polynom a, Polynom b) => Multiply(a, b);
<<<<<<< HEAD

        /// <summary>
        /// Вычисляет отношение и остаток от деления двух заданных многочленов.
        /// </summary>
        /// <param name="dvd">Делимый многочлен.</param>
        /// <param name="dvs">Многочлен делитель.</param>
        /// <param name="rem">Многочлен, в который передастся остаток от деления.</param>
        /// <returns>Многочлен типа <see cref="Polynom"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static Polynom Divide(Polynom dvd, Polynom dvs, out Polynom rem)
=======
        public static Polynom operator *(Polynom a, double b) => Multiply(a, new Polynom(new[] { b }));
        public static Polynom operator *(double a, Polynom b) => Multiply(new Polynom(new[] { a }), b);

        /// <summary>
        /// Вычисляет отношение двух заданных многочленов, с возможностью получить частное или остаток.
        /// </summary>
        /// <param name="dvd">Делимый многочлен.</param>
        /// <param name="dvs">Многочлен делитель.</param>
        /// <param name="rem">Указатель, какой избыток возвращать частное или остаток.</param>
        /// <returns>Многочлен типа <see cref="Polynom"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static Polynom Divide(Polynom dvd, Polynom dvs, Remnant rem)
>>>>>>> c8439d19b9f2bc62ea026e66856aba256f3696bc
        {
            if (dvs.Length > dvd.Length)
                throw new ArgumentOutOfRangeException("dvs", dvs, "Степень делителя должа быть не больше степени делимого.");
            double[] q = new double[dvd.Length - dvs.Length + 1];
<<<<<<< HEAD
            double[] r = (double[])dvd.Get.Clone();
=======
            double[] r = dvd.Get;
>>>>>>> c8439d19b9f2bc62ea026e66856aba256f3696bc
            for (int i = 0; i < q.Length; i++)
            {
                double tmp = r[r.Length - i - 1] / dvs.p[dvs.Length - 1];       //dvd - dividend - делимое
                q[q.Length - i - 1] = tmp;                                      //dvs - divisor - делитель
                for (int j = 0; j < dvs.Length; j++)                            //q - quotient - частное
                    r[r.Length - i - j - 1] -= tmp * dvs.p[dvs.Length - j - 1]; //r - remaider - остаток
            }
<<<<<<< HEAD
            rem = new Polynom(r);
            return new Polynom(q);
        }
        /// <summary>
        /// Вычисляет отношение двух заданных многочленов.
        /// </summary>
        /// <param name="dvd">Делимый многочлен.</param>
        /// <param name="dvs">Многочлен делитель.</param>
        /// <returns>Многочлен типа <see cref="Polynom"/>.</returns>
        public static Polynom Divide(Polynom dvd, Polynom dvs) => Divide(dvd, dvs, out Polynom rem);
        public static Polynom operator /(Polynom dvd, Polynom dvs) => Divide(dvd, dvs);
        public static Polynom operator %(Polynom dvd, Polynom dvs)
        {
            Polynom tmp = Divide(dvd, dvs, out Polynom rem);
            return rem;
        }
=======
            if (rem == Remnant.Quotient)
                return new Polynom(q);
            else if (rem == Remnant.Remaider)
                return new Polynom(r);
            else
                throw new ArgumentOutOfRangeException("rem", rem, "Неверно определен указатель избытка.");
        }
        public static Polynom operator /(Polynom dvd, Polynom dvs) => Divide(dvd, dvs, Remnant.Quotient);
        public static Polynom operator %(Polynom dvd, Polynom dvs) => Divide(dvd, dvs, Remnant.Remaider);
        public static Polynom operator /(Polynom dvd, double dvs) => Divide(dvd, new Polynom(new[] { dvs }), Remnant.Quotient);
        public static Polynom operator %(Polynom dvd, double dvs) => Divide(dvd, new Polynom(new[] { dvs }), Remnant.Remaider);
>>>>>>> c8439d19b9f2bc62ea026e66856aba256f3696bc

        /// <summary>
        /// Находит являются ли заданные многочлены равными.
        /// </summary>
        /// <param name="left">Первая логическая переменная.</param>
        /// <param name="right">Вторая логическая переменная.</param>
        /// <returns>Значение типа <see cref="bool"/>.</returns>
<<<<<<< HEAD
        public static bool Eq(Polynom left, Polynom right)
        {
            if (left.Length != right.Length)
                return false;
            for (int i = 0; i < left.Length; i++)
                if (left.p[i] != right.p[i])
                    return false;
            return true;
        }
        public static bool operator ==(Polynom left, Polynom right) => Eq(left, right);

        /// <summary>
        /// Находит являются ли заданные многочлены неравными.
        /// </summary>
        /// <param name="left">Первая логическая переменная.</param>
        /// <param name="right">Вторая логическая переменная.</param>
        /// <returns>Значение типа <see cref="bool"/>.</returns>
        public static bool NoEq(Polynom left, Polynom right) => !Eq(left, right);
        public static bool operator !=(Polynom left, Polynom right) => !Eq(left, right);

        /// <summary>
        /// Возвращает значение, показывающее, равен ли данный экземпляр заданному объекту.
        /// </summary>
        /// <param name="obj">Объект object, сравниваемый с этим экземпляром.</param>
        /// <returns>Значение типа <see cref="bool"/>.</returns>
        public override bool Equals(object obj) => !(obj is Polynom) && this == (Polynom)obj;
        /// <summary>
        /// Возвращает значение, позволяющее определить, представляют ли этот экземпляр и заданный объект <see cref="Polynom"/> одно и тоже значение.
        /// </summary>
        /// <param name="obj">Объект Polynom, сравниваемый с этим экземпляром.</param>
        /// <returns></returns>
        public bool Equals(Polynom obj) => this == obj;

        /// <summary>
        /// Возвращает хэш-код данного экземпляра.
        /// </summary>
        /// <returns>Целочисленное значение.</returns>
        public override int GetHashCode()
        {
            double sum = 0;
            for (int i = 0; i < Length; i++)
                sum += Get[i] * (i + 1);
            return sum.GetHashCode();
        }

        /// <summary>
        /// Возводит заданный многочлен в степень <paramref name="power"/>.
        /// </summary>
        /// <param name="value">Многочлен возводимый в степень.</param>
        /// <param name="power">Целочисленное значение степени.</param>
        /// <returns>Многочлен типа <see cref="Polynom"/></returns>
        /// <exception cref="ArithmeticException"/>
        public static Polynom Pow(Polynom value, int power)
        {
            if (power < 0)
                throw new ArithmeticException("Недопустимо отрицательное значение параметра power.");
            if (power == 0)
                return new Polynom(new[] { 1.0 });
            Polynom res = new Polynom(value.Get);
            for (int i = 1; i < power; i++)
                res = res * value;
            return res;
        }

        /// <summary>
        /// Вычисляет производную функцию заданного многочлена.
        /// </summary>
        /// <param name="value">Многочлен для вычисления производной.</param>
        /// <returns>Многочлен типа <see cref="Polynom"/></returns>
        public static Polynom Derivative(Polynom value)
        {
            double[] res = new double[value.Length - 1];
            for (int i = 1; i <= res.Length; i++)
                res[i - 1] = value.p[i] * i;
            return (new Polynom(res));
        }

        /// <summary>
=======
        public static bool Eq(Polynom left, Polynom right) => (left.Length == right.Length) && left.Get.SequenceEqual(right.Get);
        public static bool operator ==(Polynom left, Polynom right) => Eq(left, right);

        /// <summary>
        /// Находит являются ли заданные многочлены неравными.
        /// </summary>
        /// <param name="left">Первая логическая переменная.</param>
        /// <param name="right">Вторая логическая переменная.</param>
        /// <returns>Значение типа <see cref="bool"/>.</returns>
        public static bool NoEq(Polynom left, Polynom right) => !Eq(left, right);
        public static bool operator !=(Polynom left, Polynom right) => !Eq(left, right);

        /// <summary>
        /// Возвращает значение, показывающее, равен ли данный экземпляр заданному объекту.
        /// </summary>
        /// <param name="obj">Объект object, сравниваемый с этим экземпляром.</param>
        /// <returns>Значение типа <see cref="bool"/>.</returns>
        public override bool Equals(object obj) => !(obj is Polynom) && this == (Polynom)obj;
        /// <summary>
        /// Возвращает значение, позволяющее определить, представляют ли этот экземпляр и заданный объект <see cref="Polynom"/> одно и тоже значение.
        /// </summary>
        /// <param name="obj">Объект Polynom, сравниваемый с этим экземпляром.</param>
        /// <returns></returns>
        public bool Equals(Polynom obj) => this == obj;

        /// <summary>
        /// Возвращает хэш-код данного экземпляра.
        /// </summary>
        /// <returns>Целочисленное значение.</returns>
        public override int GetHashCode()
        {
            double sum = 0;
            for (int i = 0; i < Length; i++)
                sum += Get[i] * (i + 1);
            return sum.GetHashCode();
        }

        /// <summary>
        /// Возводит заданный многочлен в степень <paramref name="power"/>.
        /// </summary>
        /// <param name="value">Многочлен возводимый в степень.</param>
        /// <param name="power">Целочисленное значение степени.</param>
        /// <returns>Многочлен типа <see cref="Polynom"/></returns>
        /// <exception cref="ArithmeticException"/>
        public static Polynom Pow(Polynom value, int power)
        {
            if (power < 0)
                throw new ArithmeticException("Недопустимо отрицательное значение параметра power.");
            if (power == 0)
                return new Polynom(new[] { 1.0 });
            Polynom res = new Polynom(value.Get);
            for (int i = 1; i < power; i++)
                res = res * value;
            return res;
        }

        /// <summary>
        /// Вычисляет производную функцию заданного многочлена.
        /// </summary>
        /// <param name="value">Многочлен для вычисления производной.</param>
        /// <returns>Многочлен типа <see cref="Polynom"/></returns>
        public static Polynom Derivative(Polynom value)
        {
            double[] res = new double[value.Length - 1];
            for (int i = 1; i <= res.Length; i++)
                res[i - 1] = value.p[i] * i;
            return (new Polynom(res));
        }

        /// <summary>
>>>>>>> c8439d19b9f2bc62ea026e66856aba256f3696bc
        /// Вычисляет значение заданного многочлена в точке <paramref name="x"/>.
        /// </summary>
        /// <param name="value">Многочлен для вичислений.</param>
        /// <param name="x">Переменная типа <see cref="double"/>, представляющая точку в которой надо найти значение заданного многочлена.</param>
        /// <returns>Значение с плавающей точкой.</returns>
        public static double ReplaceX(Polynom value, double x)
        {
            double res = 0;
            for (int i = 0; i < value.Length; i++)
                res += value.p[i] * Math.Pow(x, i);
            return res;
        }

        public static implicit operator Polynom(byte value) => new Polynom(value);
        public static implicit operator Polynom(sbyte value) => new Polynom(value);
        public static implicit operator Polynom(short value) => new Polynom(value);
        public static implicit operator Polynom(ushort value) => new Polynom(value);
        public static implicit operator Polynom(int value) => new Polynom(value);
        public static implicit operator Polynom(uint value) => new Polynom(value);
        public static implicit operator Polynom(long value) => new Polynom(value);
        public static implicit operator Polynom(ulong value) => new Polynom(value);
        public static implicit operator Polynom(float value) => new Polynom(value);
        public static implicit operator Polynom(double value) => new Polynom(value);
        public static implicit operator Polynom(decimal value) => new Polynom((double)value);
    }
    
    public struct Polynomial//no working
    {
        private Dictionary<Dictionary<string, int>, double> bp;
        private Monomial[] monoms;
        
    
        public Polynomial(string Monomials)
        {
            Dictionary<string, double> p = new Dictionary<string, double>();
            Monomials = Monomials.Replace(" ", "");
            string[] pluses = Monomials.Split('+');
            for(int i = 0; i < Monomials.Length; i++)
            {
                
            }
            bp = new Dictionary<Dictionary<string, int>, double>();
            monoms = new Monomial[100];
            for (int i = 0; i < 100; i++)
            {
                Dictionary<string, int> tmp = new Dictionary<string, int>() { ["a"] = 1 };
                for (int j = 0; j < 2; j++)
                    tmp.Add("a", j);
                monoms[i] = new Monomial(0, tmp);
            }
        }

        public Polynomial(Dictionary<Dictionary<string, int>, double> Monomials)
        {
            bp = Monomials;
            monoms = new Monomial[100];
        }
    }

    struct Monomial//no working
    {
        private dynamic coef;
        //private Dictionary<string, int> vars;
        private string[] letters;
        private int[] degrees;
        
        public Monomial(double factor, Dictionary<string, int> indeters)
        {
            coef = factor;
            //vars = indeters;
            letters = /*new string[indeters.Count]*/indeters.Keys.ToArray();
            degrees = /*new int[indeters.Count]*/indeters.Values.ToArray();
            //for (int i = 0; i < indeters.Count; i++)
            //{
            //    var LetInDeg = indeters.ElementAt(i);
            //    letters[i] = LetInDeg.Key;
            //    degrees[i] = LetInDeg.Value;
            //}
        }
    }

    public struct Polynomial
    {
        private Dictionary<string, double> bp;

        public Polynomial(string Monomials)
        {
            Dictionary<string, double> p = new Dictionary<string, double>();
            Monomials = Monomials.Replace(" ", "");
            string[] pluses = Monomials.Split('+');
            for(int i = 0; i < Monomials.Length; i++)
            {

            }
            bp = new Dictionary<string, double>();
        }
    }
}
