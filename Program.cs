using System;

namespace Practice5
{
    public class TCircle<T> : IComparable<TCircle<T>> where T : struct, IComparable, IConvertible
    {
        private T radius;

        public T Radius
        {
            get { return radius; }
            set
            {
                if (Convert.ToDouble(value) < 0)
                    throw new ArgumentException("Радіус не може бути від'ємним.");
                radius = value;
            }
        }

        // Конструктор без параметрів
        public TCircle()
        {
            radius = default(T);
        }

        // Конструктор з параметрами
        public TCircle(T r)
        {
            Radius = r;
        }

        // Конструктор копіювання
        public TCircle(TCircle<T> other)
        {
            this.radius = other.radius;
        }

        // Введення даних
        public void Input()
        {
            Console.Write("Введіть радіус кола: ");
            string input = Console.ReadLine();
            Radius = (T)Convert.ChangeType(input, typeof(T));
        }

        // Виведення даних
        public void Output()
        {
            Console.WriteLine($"Коло [Радіус = {radius}, Довжина = {GetLength():F2}, Площа круга = {GetArea():F2}]");
        }

        // Визначення площі круга
        public double GetArea()
        {
            double r = Convert.ToDouble(radius);
            return Math.PI * r * r;
        }

        // Визначення площі сектора (кут у градусах)
        public double GetSectorArea(double angleDegrees)
        {
            double r = Convert.ToDouble(radius);
            return (Math.PI * r * r * angleDegrees) / 360.0;
        }

        // Визначення довжини кола
        public double GetLength()
        {
            double r = Convert.ToDouble(radius);
            return 2 * Math.PI * r;
        }

        // Порівняння з іншим колом
        public int CompareTo(TCircle<T> other)
        {
            if (other == null) return 1;
            return this.radius.CompareTo(other.radius);
        }

        // Перевантаження оператора + (додавання радіусів)
        public static TCircle<T> operator +(TCircle<T> c1, TCircle<T> c2)
        {
            double res = Convert.ToDouble(c1.radius) + Convert.ToDouble(c2.radius);
            return new TCircle<T>((T)Convert.ChangeType(res, typeof(T)));
        }

        // Перевантаження оператора - (віднімання радіусів)
        public static TCircle<T> operator -(TCircle<T> c1, TCircle<T> c2)
        {
            double res = Convert.ToDouble(c1.radius) - Convert.ToDouble(c2.radius);
            if (res < 0) res = 0;
            return new TCircle<T>((T)Convert.ChangeType(res, typeof(T)));
        }

        // Перевантаження оператора * (множення радіуса на число)
        public static TCircle<T> operator *(TCircle<T> c, double number)
        {
            double res = Convert.ToDouble(c.radius) * number;
            return new TCircle<T>((T)Convert.ChangeType(res, typeof(T)));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Тестування TCircle<int> ===");
            TCircle<int> c1 = new TCircle<int>(10);
            TCircle<int> c2 = new TCircle<int>(4);
            c1.Output();
            c2.Output();
            Console.WriteLine($"Площа сектора першого кола (кут 90 град.): {c1.GetSectorArea(90):F2}");

            TCircle<int> cSum = c1 + c2;
            Console.Write("Сума кіл (c1 + c2): ");
            cSum.Output();

            TCircle<int> cSub = c1 - c2;
            Console.Write("Різниця кіл (c1 - c2): ");
            cSub.Output();

            TCircle<int> cMult = c2 * 3;
            Console.Write("Множення (c2 * 3): ");
            cMult.Output();

            Console.WriteLine($"Порівняння c1 та c2: {(c1.CompareTo(c2) > 0 ? "c1 більше за c2" : "c1 менше або рівне c2")}\n");

            Console.WriteLine("=== Тестування TCircle<double> ===");
            TCircle<double> cd1 = new TCircle<double>(5.5);
            TCircle<double> cd2 = new TCircle<double>(cd1); // Конструктор копіювання
            cd1.Output();
            Console.Write("Копія кола: ");
            cd2.Output();

            Console.ReadLine();
        }
    }
}