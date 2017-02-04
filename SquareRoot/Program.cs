using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareRoot
{
    class Program
    {
        static void Main(string[] args)
        {
            var threshold = 0.000001;
            var inputNumber = 0.5;

            Console.WriteLine("My Answer: " + Sqrt(inputNumber, threshold));
            Console.WriteLine("Actual answer: " + Math.Sqrt(inputNumber));
            Console.ReadLine();
        }

        public static double Sqrt(double input, double threshold)
        {
            return SqrtInner(input, threshold, 0, Math.Max(input, 1));
        }

        public static double SqrtInner(double input, double threshold, double start, double end)
        {
            var checkNumber = start + ((end - start) / 2);
            var approximation = checkNumber * checkNumber;
            if (Math.Abs(approximation-input) < threshold)
            {
                return checkNumber;
            }

            if (approximation > input)
            {
                return SqrtInner(input, threshold, start, checkNumber);
            }
            else
            {
                return SqrtInner(input, threshold, checkNumber, end);
            }
        }
    }
}
