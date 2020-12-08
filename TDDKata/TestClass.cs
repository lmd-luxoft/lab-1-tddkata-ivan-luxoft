﻿// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace TDDKata
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void SimpleTest()
        {
            StringCalc calc = new StringCalc();
            int value = calc.Sum("2,2");
            Assert.That(value, Is.EqualTo(4), "Wrong actual value");
        }

        [Test]
        [TestCase("1,5", 6)]
        [TestCase("5,0", 5)]
        [TestCase("99,199", 298)]
        public void SumTwoArgumentShouldCalculateCorrectly(string argForCalc, int expectedValue)
        {
            // Arrange
            var calc = new StringCalc();

            // Act
            var actualValue = calc.Sum(argForCalc);

            // Arrange
            Assert.AreEqual(expectedValue, actualValue, "Two argument calculated not correctly");
        }

        [Test]
        [TestCase("1001", 0)]
        [TestCase("1001,5", 5)]
        [TestCase("7,9999,3", 10)]
        [TestCase("//+\n1000+1001+1000", 2000)]
        public void NumberGreatThanThousandShouldIgnore(string argForCalc, int expectedValue)
        {
            // Arrange
            var calc = new StringCalc();

            // Act
            var actualValue = calc.Sum(argForCalc);

            // Arrange
            Assert.AreEqual(expectedValue, actualValue, "Number great than 1000 should be ignore");
        }

        [Test]
        [TestCase("1,5,999", 1005)]
        [TestCase("5,0,1,1", 7)]
        [TestCase("1,2,3,4,5", 15)]
        public void MoreThanTwoArgumentShouldCalculateCorrectly(string argForCalc, int expectedValue)
        {
            // Arrange
            var calc = new StringCalc();

            // Act
            var actualValue = calc.Sum(argForCalc);

            // Arrange
            Assert.AreEqual(expectedValue, actualValue, "More than two argument calculated not correctly");
        }

        [Test]
        [TestCase("1,5\n999", 1005)]
        [TestCase("5\n1", 6)]
        [TestCase("1\n2,3", 6)]
        public void TwoSymbolCanBeUseAsDelimeterShouldWorkCorrectly(string argForCalc, int expectedValue)
        {
            // Arrange
            var calc = new StringCalc();

            // Act
            var actualValue = calc.Sum(argForCalc);

            // Arrange
            Assert.AreEqual(expectedValue, actualValue, "Symbols \"\\n\" or \",\" Can be use as delimeter for numbers");
        }

        [Test]
        [TestCase("//;\n1;5;998", 1004)]
        [TestCase("//(\n1(5(999", 1005)]
        [TestCase("//&\n1&5&999", 1005)]
        [TestCase("//'\n1'5'999", 1005)]
        [TestCase("//\\\n1\\5\\999", 1005)]
        [TestCase("//\"\n1\"5\"999", 1005)]
        [TestCase("// \n1 5 999", 1005)]
        [TestCase("//-\n1-5-999", 1005)]
        [TestCase("///\n1/5/997", 1003)]
        [TestCase("//a\n1a5a999", 1005)]
        public void CustomDelimeterShouldWorkCorrectly(string argForCalc, int expectedValue)
        {
            // Arrange
            var calc = new StringCalc();

            // Act
            var actualValue = calc.Sum(argForCalc);

            // Arrange
            Assert.AreEqual(expectedValue, actualValue, "Argument with custom delimeter should calculated correctly");
        }

        [Test]
        [TestCase("//\n,\n1,5\n999")]
        [TestCase("//,.\n1,5,999")]
        [TestCase("//,\n\n1,5,999")]
        public void CustomDelimeterWithSomeSymbolsReturnError(string argForCalc)
        {
            // Arrange
            var calc = new StringCalc();
            var expectedValue = -1;

            // Act
            var actualValue = calc.Sum(argForCalc);

            // Arrange
            Assert.AreEqual(expectedValue, actualValue, "Delimeter should have one symbol");
        }

        [Test]
        [TestCase("//-\n1,5")]
        [TestCase("//.\n1\n5")]
        public void UsageDefaultDelimeterInArgumentWithCustomShoulReturnError(string argForCalc)
        {
            // Arrange
            var calc = new StringCalc();
            var expectedValue = -1;

            // Act
            var actualValue = calc.Sum(argForCalc);

            // Arrange
            Assert.AreEqual(expectedValue, actualValue, "Default delimeter not works if set custom");
        }

        [Test]
        [TestCase("//1,5")]
        [TestCase("//,1,5")]
        [TestCase("//\n1,5")]
        public void UncorrectFormatForCustomDelimeterReturnError(string argForCalc)
        {
            // Arrange
            var calc = new StringCalc();
            var expectedValue = -1;
            // Act
            var actualValue = calc.Sum(argForCalc);

            // Arrange
            Assert.AreEqual(expectedValue, actualValue, "Uncorrect format for custom delimeter should return error");
        }

        [Test]
        [TestCase("//0\n105")]
        [TestCase("//8\n282")]
        public void NumbersCannotUseAsDelimeterReturnError(string argForCalc)
        {
            // Arrange
            var calc = new StringCalc();
            var expectedValue = -1;

            // Act
            var actualValue = calc.Sum(argForCalc);

            // Arrange
            Assert.AreEqual(expectedValue, actualValue, "Numbers cannot use as delimeter");
        }

        [Test]
        public void EmptyStringShouldReturnZero()
        {
            // Arrange
            var calc = new StringCalc();
            var argForCalc = "";
            var expectedValue = 0;

            // Act
            var actualValue = calc.Sum(argForCalc);

            // Arrange
            Assert.AreEqual(expectedValue, actualValue, "For empty argument should return zero");
        }

        [Test]
        public void OneArgumentShouldReturnHimself()
        {
            // Arrange
            var calc = new StringCalc();
            var argForCalc = "3";
            var expectedValue = 3;

            // Act
            var actualValue = calc.Sum(argForCalc);

            // Arrange
            Assert.AreEqual(expectedValue, actualValue, "For one argument should return the himself");
        }

        [Test]
        public void NullArgumentShouldReturnError()
        {
            // Arrange
            var calc = new StringCalc();
            string argForCalc = null;
            var expectedValue = -1;

            // Act
            var actualValue = calc.Sum(argForCalc);

            // Arrange
            Assert.AreEqual(expectedValue, actualValue, "Null argument should return error (-1)");
        }

        [Test]
        [TestCase("-1,2")]
        [TestCase("2,-1")]
        [TestCase("-2")]
        public void NegativelyArgumentShouldReturnError(string argForCalc)
        {
            // Arrange
            var calc = new StringCalc();
            var expectedValue = -1;

            // Act
            var actualValue = calc.Sum(argForCalc);

            // Arrange
            Assert.AreEqual(expectedValue, actualValue, "Negatively argument should return error (-1)");
        }

        [Test]
        [TestCase(",")]
        [TestCase("1,2,")]
        [TestCase(",2,0")]
        [TestCase("1,a")]
        [TestCase("a,1")]
        [TestCase("?")]
        [TestCase("\"")]
        [TestCase("-")]
        [TestCase("1,-")]
        [TestCase("1.0")]
        [TestCase("1f")]
        [TestCase("1m")]
        [TestCase("1d")]
        [TestCase("0x01")]
        public void UncorrectArgumentShouldReturnError(string argForCalc)
        {
            // Arrange
            var calc = new StringCalc();
            var expectedValue = -1;

            // Act
            var actualValue = calc.Sum(argForCalc);

            // Arrange
            Assert.AreEqual(expectedValue, actualValue, "Uncorrect argument should return error (-1)");
        }
    }
}
