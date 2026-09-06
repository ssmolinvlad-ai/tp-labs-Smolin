using System;

namespace Lab1
{
    class Program
    {
        static void Main()
        {
            Console.Write("Введите целое неотрицательное число n: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int n) && n >= 0)
            {
                try
                {
                    long factorial = ComputeFactorial(n);
                    Console.WriteLine($"{n}! = {factorial}");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Ошибка: результат слишком велик для long");
                }
            }
            else
            {
                Console.WriteLine("Ошибка ввода: нужно целое неотрицательное число");
            }

            Console.Write("\nВведите целое неотрицательное число n: ");
            input = Console.ReadLine();
            if (int.TryParse(input, out n) && n >= 0)
            {
                string fibSequence = GetFibonacciSequence(n);
                Console.WriteLine($"Последовательность: {fibSequence}");
            }
            else
            {
                Console.WriteLine("Ошибка ввода: нужно целое неотрицательное число");
            }

            Console.Write("\nВведите значение x: ");
            input = Console.ReadLine();
            if (double.TryParse(input, out double x))
            {
                try
                {
                    double result = ComputeFunctionA(x);
                    Console.WriteLine($"A({x}) = {result}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Ошибка ввода: нужно вещественное число");
            }

            Console.Write("\nВведите x для ln(1+x) (-1 < x <= 1): ");
            input = Console.ReadLine();
            if (double.TryParse(input, out double xLn))
            {
                try
                {
                    const double epsilon = 1e-6;
                    var (sum, count) = ComputeTaylorLn1PlusX(xLn, epsilon);
                    double mathLn = Math.Log(1 + xLn);
                    Console.WriteLine($"Сумма ряда: {sum}");
                    Console.WriteLine($"Math.Log(1+x): {mathLn}");
                    Console.WriteLine($"Количество просуммированных членов: {count}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Ошибка ввода: нужно вещественное число");
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        static long ComputeFactorial(int n)
        {
            if (n < 0) throw new ArgumentException("n должно быть >= 0");
            long result = 1;
            checked
            {
                for (int i = 2; i <= n; i++)
                    result *= i;
            }
            return result;
        }

        static string GetFibonacciSequence(int n)
        {
            if (n < 0) throw new ArgumentException("n должно быть >= 0");
            if (n > 92)
            {
                Console.WriteLine("Предупреждение: числа Фибоначчи при n > 92 не помещаются в тип long. Будет выведено до 92.");
                n = 92;
            }

            if (n == 0) return "0";
            if (n == 1) return "0, 1";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("0, 1");
            long a = 0, b = 1;
            for (int i = 2; i <= n; i++)
            {
                long next = a + b;
                sb.Append($", {next}");
                a = b;
                b = next;
            }
            return sb.ToString();
        }

        static double ComputeFunctionA(double x)
        {
            if (x <= 0)
                throw new ArgumentException("x должен быть > 0 (из-за логарифма)");
            if (x < 54)
                throw new ArgumentException("под корнем отрицательное число (x−54 < 0)");
            double sinVal = Math.Sin(x * x);
            if (Math.Abs(sinVal) < 1e-15)
                throw new ArgumentException("деление на ноль (sin(x²) ≈ 0)");

            double result = Math.Sqrt(x - 54) + Math.Cos(x / 2) / sinVal - Math.Log(x);
            if (double.IsNaN(result) || double.IsInfinity(result))
                throw new ArgumentException("результат не является числом (NaN или бесконечность)");
            return result;
        }

        static (double sum, int count) ComputeTaylorLn1PlusX(double x, double epsilon)
        {
            if (x <= -1 || x > 1)
                throw new ArgumentException("x должен быть в диапазоне -1 < x <= 1");

            double sum = 0.0;
            double term = x;
            int n = 1;
            while (Math.Abs(term) > epsilon)
            {
                sum += term;
                n++;
                term = -term * x * (n - 1) / n;
            }
            return (sum, n - 1);
        }
    }
}