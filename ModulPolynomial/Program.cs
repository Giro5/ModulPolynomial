using System;
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

                Console.WriteLine($"Сумма многочленов равна: {c1 + c2}\n");

                Console.WriteLine($"Вычитание многочленов равно: {c1 - c2}\n");

                Console.WriteLine($"Произведение многочленов равно: {c1 * c2}\n");

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
                Console.WriteLine($"Значение многочленов в точке x0 = {x0}, равно:\nA) {Polynom.ReplaceX(Polynom.Derivative(c1), x0)}\nB) {Polynom.ReplaceX(Polynom.Derivative(c2), x0)}");
                
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
    public struct Polynom : IEquatable<Polynom>
    {
        /// <summary>
        /// Многочлен представлен следующим типом, коэффициент одночлена aₙxⁿ - элемент массива с индексом n, т.о. индекс соответствует степени x.
        /// </summary>
        private List<double> p;

        /// <summary>
        /// Конструктор инициализирующий заданное количество элементов.
        /// </summary>
        /// <param name="P">Целочисленное значение количества элементов.</param>
        public Polynom(int Count) => this.p = new List<double>(Count);

        /// <summary>
        /// Конструктор создающий инициализирующий многочлен с заданными коэффициентами.
        /// </summary>
        /// <param name="Monomials">Массив значений с плавающей точкой содержащий коэффициенты многочлена.</param>
        public Polynom(params double[] Monomials) => this.p = new List<double>(Monomials);

        /// <summary>
        /// Возвращает массив значений с плавающей точкой содержащий коэффициенты многочлена.
        /// </summary>
        /// <returns>Массив содержащий коэффициенты многочлена.</returns>
        public double[] Get { get { return p.ToArray(); } }

        /// <summary>
        /// Возвращает количество одночленов многочлена.
        /// </summary>
        /// <returns>Количество одночленов многочлена.</returns>
        public int Length { get { return p.ToArray().Length; } }

        /// <summary>
        /// Возвращает строку в стандартном виде многочлена: "aₒ + a₁x + a₂x² + ... + aₙxⁿ".
        /// </summary>
        public string S
        {
            get
            {
                string[] tmp = new string[p.Count];
                for (int i = 0; i < tmp.Length; i++)
                    tmp[i] = p[i] != 0 ? ((p[i] > 0 ? (p[i] == 1 ? (i != 0 ? " + " : " + 1") : $" + {p[i]}") : (p[i] == -1 ? (i != 0 ? " - " : " - 1") : $" - {-1 * p[i]}")) + (i > 0 ? (i > 1 ? $"x^{i}" : "x") : "")) : "";
                string s = string.Join("", tmp) == "" ? " + 0" : string.Join("", tmp);
                return s[1] == '+' ? s.Remove(0, 3) : "-" + s.Remove(0, 3);
            }
        }

        /// <summary>
        /// Возвращает строку в стандартном виде многочлена: "aₙxⁿ + ... + a₂x² + a₁x + aₒ".
        /// </summary>
        public string SReverse
        {
            get
            {
                string[] tmp = new string[p.Count];
                for (int i = 0; i < tmp.Length; i++)
                    tmp[i] = p[i] != 0 ? ((p[i] > 0 ? (p[i] == 1 ? (i != 0 ? " + " : " + 1") : $" + {p[i]}") : (p[i] == -1 ? (i != 0 ? " - " : " - 1") : $" - {-1 * p[i]}")) + (i > 0 ? (i > 1 ? $"x^{i}" : "x") : "")) : "";
                string s = string.Join("", tmp) == "" ? " + 0" : string.Join("", tmp.Reverse());
                return s[1] == '+' ? s.Remove(0, 3) : "-" + s.Remove(0, 3);
            }
        }

        /// <summary>
        /// Преобразовывает значение этого экземпляра в эквивалентное ему строковое представление (см. свойство S).
        /// </summary>
        /// <returns>aₒ + a₁x + a₂x² + ... + aₙxⁿ</returns>
        public override string ToString() => this.S;

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

        /// <summary>
        /// Вычисляет отношение двух заданных многочленов, с возможностью получить частное или остаток.
        /// </summary>
        /// <param name="dvd">Делимый многочлен.</param>
        /// <param name="dvs">Многочлен делитель.</param>
        /// <param name="rem">Указатель, какой избыток возвращать частное или остаток.</param>
        /// <returns>Многочлен типа <see cref="Polynom"/>.</returns>
        public static Polynom Divide(Polynom dvd, Polynom dvs, Remnant rem)
        {
            if (dvs.Length > dvd.Length)
                return (new Polynom(0.0));
            double[] q = new double[dvd.Length - dvs.Length + 1];
            double[] r = (double[])dvd.Get.Clone();
            for (int i = 0; i < q.Length; i++)
            {
                double tmp = r[r.Length - i - 1] / dvs.p[dvs.Length - 1];       //dvd - dividend - делимое
                q[q.Length - i - 1] = tmp;                                      //dvs - divisor - делитель
                for (int j = 0; j < dvs.Length; j++)                            //q - quotient - частное
                    r[r.Length - i - j - 1] -= tmp * dvs.p[dvs.Length - j - 1]; //r - remaider - остаток
            }
            if (rem == Remnant.Quotient)
                return (new Polynom(q));
            else if (rem == Remnant.Remaider)
                return (new Polynom(r));
            else
                return (new Polynom(0.0));
        }
        public static Polynom operator /(Polynom dvd, Polynom dvs) => Divide(dvd, dvs, Remnant.Quotient);
        public static Polynom operator %(Polynom dvd, Polynom dvs) => Divide(dvd, dvs, Remnant.Remaider);

        /// <summary>
        /// Находит являются ли заданные многочлены равными.
        /// </summary>
        /// <param name="left">Первая логическая переменная.</param>
        /// <param name="right">Вторая логическая переменная.</param>
        /// <returns>Значение типа <see cref="bool"/>.</returns>
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
        public override bool Equals(object obj) => !(obj is Polynom) && this == ((Polynom)obj);
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
        public override int GetHashCode() => this.Get.Sum().GetHashCode() % 997;

        /// <summary>
        /// Возводит заданный многочлен в степень <paramref name="power"/>.
        /// </summary>
        /// <param name="value">Многочлен возводимый в степень.</param>
        /// <param name="power">Целочисленное значение степени.</param>
        /// <returns>Многочлен типа <see cref="Polynom"/></returns>
        public static Polynom Pow(Polynom value, int power)
        {
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
        /// Вычисляет значение заданного многочлена в точке <paramref name="x"/>.
        /// </summary>
        /// <param name="value">Многочлен для вичислений.</param>
        /// <param name="x">Переменная типа <see cref="double"/>, представляющая точку в которой надо найти значение заданного многочлена.</param>
        /// <returns></returns>
        public static double ReplaceX(Polynom value, double x)
        {
            double res = 0;
            for (int i = 0; i < value.Length; i++)
                res += value.p[i] * Math.Pow(x, i);
            return res;
        }
    }
}
