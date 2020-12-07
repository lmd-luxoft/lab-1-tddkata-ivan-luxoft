// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace TDDKata
{
    internal class StringCalc
    {
        internal int Sum(string v)
        {
            ReadOnlyCollection<int> arrayOfInteger;
            try
            {
                arrayOfInteger = new ArgumentSplitter(v).Arguments;
            }
            catch
            {
                return -1;
            }

            if (arrayOfInteger.Count == 0)
                return -1;

            return arrayOfInteger.Aggregate((i1, i2) => i1 + i2);
        }

        private class ArgumentSplitter
        {
            private char[] delimeters = new char[] { ',', '\n' };
            private List<int> arguments = new List<int>();

            public ReadOnlyCollection<int> Arguments => arguments.AsReadOnly();

            public ArgumentSplitter(string argumentsString)
            {
                if (argumentsString == "")
                {
                    arguments.Add(0);
                }
                else if (argumentsString != null)
                {
                    var stringArgWoDelimeter = FindDelimeterAndReturnArgumentsRow(argumentsString);
                    if (stringArgWoDelimeter != null)
                    {
                        var argumentsFromString = SplitStringToArrayInt(stringArgWoDelimeter);
                        this.arguments.AddRange(argumentsFromString);
                    }
                }
            }

            private int[] SplitStringToArrayInt(string arg)
            {
                var splitedArguments = arg.Split(this.delimeters);

                if (splitedArguments.Any(s => s.Equals("")))
                    throw new ArgumentException("Between delimeters cannot be empty space");

                try
                {
                    return splitedArguments
                        .Select(s => int.Parse(s, NumberStyles.None))
                        .ToArray();
                }
                catch(Exception ex)
                {
                    throw new ArgumentException("Between delimeters should be only digits", ex);
                }
            }

            private string FindDelimeterAndReturnArgumentsRow(string arg)
            {
                if (!arg.StartsWith("//"))
                    return arg;

                var customDelimeterParameters = arg.Split('\n').First();
                var customDelimeter = customDelimeterParameters.Skip(2).ToArray();

                if (customDelimeter.Length != 1)
                    throw new ArgumentException("Custom delimeter length must be one symbol");

                if (int.TryParse(customDelimeter[0].ToString(), out var _))
                    throw new ArgumentException("Custom delimeter cannot be number");

                this.delimeters = customDelimeter;
                return arg.Replace($"{customDelimeterParameters}\n", "");
            }
        }
    }
}