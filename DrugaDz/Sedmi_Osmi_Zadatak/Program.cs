using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedmi_Osmi_Zadatak
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = Task.Run(() => LetsSayUserClickedAButtonOnGuiMethod());
            Console.Read();
        }

        private static void LetsSayUserClickedAButtonOnGuiMethod()
        {
            var result = GetTheMagicNumber();
            Console.WriteLine(result.Result);
        }

        private static async Task<int> GetTheMagicNumber()
        {
            return await IKnowIGuyWhoKnowsAGuy();
        }

        private static async Task<int> IKnowIGuyWhoKnowsAGuy()
        {
            var result1 = await IKnowWhoKnowsThis(10);
            var result2 = await IKnowWhoKnowsThis(5);

            return result1 + result2;
        }

        private static async Task<int> IKnowWhoKnowsThis(int n)
        {
            return await FactorialDigitSum(n);
        }

        public static async Task<int> FactorialDigitSum(int n)
        {
            return await Task.Factory.StartNew<int>(() =>
            {
                long factorial = 1;
                for (int i = 1; i <= n; i++)
                {
                    factorial = factorial * i;
                }

                int sum = 0;
                while (factorial > 0)
                {
                    sum += (int)(factorial % 10);
                    factorial /= 10;
                }
                return sum;
            });
        }
    }
}
