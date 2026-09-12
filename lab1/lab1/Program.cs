using System;
using System.Numerics;
using static Programm.Programm;

namespace Programm
{
    class Programm
    {
        static void Main()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Для выхода из программы введите exit в любом месте");
                Console.WriteLine("Для возврата в это меню введите q в любом месте");
                Console.WriteLine("Команды: ");
                Console.WriteLine("Задание 1: 1");
                Console.WriteLine("Задание 2: 2");
                Console.WriteLine("Задание 3: 3");
                try
                {
                    while (true)
                    {
                        int n = GetFromConsole<int>("Введите команду: ");
                        switch (n)
                        {
                            case 1:
                                Console.Clear();
                                FirstTask.Invoke();
                                break;
                            case 2:
                                Console.Clear();
                                SecondTask.Invoke();
                                break;
                            case 3:
                                Console.Clear();
                                ThirdTask.Invoke();
                                break;
                            default:
                                Console.WriteLine("Нет такой команды");
                                break;
                        }
                    }
                }
                catch (UserQuitException)
                {
                    Console.Clear();
                }
                catch (UserExitException)
                {
                    Console.Clear();
                    return;
                }
            }
        }
        public class UserQuitException : Exception
        {
            public UserQuitException() : base("Пользователь прервал ввод") { }
        }
        public class UserExitException : Exception
        {
            public UserExitException() : base("Пользователь вышел из программы") { }
        }
        public static T GetFromConsole<T>(string prompt)
        {
            while (true)
            {
                try
                {
                    Console.Write(prompt);
                    string s = Console.ReadLine() ?? "";
                    if (s == "q")
                    {
                        throw new UserQuitException();
                    }
                    if (s == "exit")
                    {
                        throw new UserExitException();
                    }
                    if (typeof(T) == typeof(double))
                    {
                        return (T)(object)double.Parse(s);
                    }
                    if (typeof(T) == typeof(int))
                    {
                        return (T)(object)int.Parse(s);
                    }
                    if (typeof(T) == typeof(string))
                    {
                        return (T)(object)s;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Невозможно преобразовать в тип {typeof(T)}");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Слишком большое число");
                }
                catch (UserQuitException)
                {
                    throw;
                }
                catch (UserExitException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Красава сломал консоль сам разбирайся с этой ошибкой: {ex.Message}");
                }
            }
        }
    }   

    static class FirstTask
    {
        public static void Invoke()
        {
            Console.WriteLine("Задание 1");
            double n;
            n = Programm.GetFromConsole<double>("Введите значение переменной n: ");

            double m;
            m = Programm.GetFromConsole<double>("Введите значение переменной m: ");

            double m1 = m;
            double n1 = n;
            Console.WriteLine($"--m - n++ = {Expression1(ref m1, ref n1)}, n = {n1}, m = {m1}");
            double m2 = m;
            double n2 = n;
            Console.WriteLine($"m*m < n++ = {Expression2(ref m2, ref n2)}, n = {n2}, m = {m2}");
            double m3 = m;
            double n3 = n;
            Console.WriteLine($"n-- > m++ = {Expression3(ref m3, ref n3)}, n = {n3}, m = {m3}");
            while (true)
            {
                double x = Programm.GetFromConsole<double>("Введите значение переменной x: ");
                Console.WriteLine($"1 + 1 / {x} + 1 / ({x} ^ 2) = {Expression4(x)}");
            }
        }
        static double Expression1(ref double m, ref double n)
        {
            return --m - n++;
        }
        static bool Expression2(ref double m, ref double n)
        {
            return m*m < n++;
        }
        static bool Expression3(ref double m, ref double n)
        {
            return n-- > m++;
        }
        static string Expression4(double x)
        {
            return double.IsInfinity(1 + (1 / x) + (1 / (x * x))) ? "inf": (1 + (1/x) + (1/ (x*x))).ToString();
        }
    }
    static class SecondTask
    {
        public static void Invoke()
        {
            Console.WriteLine("Задание 2");
            while (true)
            {
                double x;
                x = Programm.GetFromConsole<double>("Введите значение переменной x: ");

                double y;
                y = Programm.GetFromConsole<double>("Введите значение переменной y: ");

                Console.WriteLine(Check(x, y) ? "Точка принадлежит графику\n" : "Точка не принадлежит графику\n");
            }
        }
        
        static bool Check(double x, double y)
        {
            return CheckCircle(x, y, 0, 0, 5) || CheckCircle(x, y, -5, 0, 5);
        }
            
        static bool CheckCircle(double x, double y, double a, double b, double r)
        {
            return (x - a) * (x - a) + (y - b) * (y - b) <= r * r;
        }
    }
    static class ThirdTask
    {
        public static void Invoke()
        {
            while (true)
            {
                Console.WriteLine("Задание 3");
                Console.WriteLine("(a - b)^3 - (a^3 - 3(a^2)b");
                Console.WriteLine("——------------------------");
                Console.WriteLine("      3a(b^2) - b^3\n");
                Console.WriteLine($"float:  {Formula<float>(1000f, 0.0001f)}");
                Console.WriteLine($"double: {Formula<double>(1000, 0.0001)}");
                Programm.GetFromConsole<string>("");
                Console.Clear();
            }
        }
        static T Formula<T>(T a, T b) where T : INumber<T>
        {
            return (((a - b) * (a - b) * (a - b)) - (a * a * a - T.CreateChecked(3) * a * a * b)) / (T.CreateChecked(3) * a * b * b - b * b * b);
        }
    }
}