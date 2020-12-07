// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System;
using System.Linq;

namespace TDDKata
{
    internal class StringCalc
    {
        internal int Sum(string v)
        {
            var arrayOfInteger = SplitStringToArrayInt(v);

            if (arrayOfInteger == null
                || arrayOfInteger.Length > 2)
                return -1;

            return arrayOfInteger.Aggregate((i1, i2) => i1 + i2);
        }

        private int[] SplitStringToArrayInt(string arg)
        {
            if (arg == null)
                return null;

            if (arg == "")
                return new int[] { 0 };

            var result = default(int[]);
            var splitedArguments = arg.Split(',');

            if (splitedArguments.Any(s => s.Contains("-")))
                return result;

            try
            {
                result = splitedArguments
                    .Select(s => int.Parse(s))
                    .ToArray();
            }
            catch
            {
                return result;
            }

            return result;
        }
    }
}