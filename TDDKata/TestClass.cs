// NUnit 3 tests
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
