using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Core.Model;

namespace DPA_MusicSheets.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            var length = 0.1875.ToString(CultureInfo.InvariantCulture).Length;
            Console.WriteLine("length: {0}", length);

            var factor = Math.Pow(10, length);
            Console.WriteLine("factor: {0}", factor);

            var numerator = Convert.ToInt32(factor * 0.1875);
            Console.WriteLine("numerator: {0}| max: {1}, {2}", numerator, short.MaxValue, int.MaxValue);

            var fraction = new Fraction(numerator, 1);
            Console.WriteLine("fraction: {0}/{1}", fraction.Numerator, fraction.Denominator);

            var simple = fraction.Simplification();
            Console.WriteLine("simple: {0}/{1}", simple.Numerator, simple.Denominator);

            var truth = fraction.Simplification();
            Console.WriteLine("truth: {0}/{1} = {2}", 3, 6, 3.0 / 6.0);

            Console.WriteLine("{0}", 0.18752131245125125.ToString(CultureInfo.InvariantCulture));
            Console.Read();
        }
    }
}
