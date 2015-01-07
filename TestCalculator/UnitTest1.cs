using System;
using System.Collections.Generic;
using ConsoleCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace TestCalculator
{
	[TestClass]
	public class UnitTest1
	{
		[TestCase("+", 1, 2, 1+2)]
		public void TestBinaryOperators(string key, double arg1, double arg2, double result)
		{
			var oper = Operators.Get(key, 2);
			Assert.That(Math.Abs(oper.Execute(new List<double>{arg1, arg2}) - result) < 0.0001);
		}

		[TestCase("55", 55)]
		[TestCase("10", 10)]
		[TestCase("1.0", 1)]
		[TestCase("-1.0", -1)]
		[TestCase("1+2+3+5", 1 + 2 + 3 + 5)]
		[TestCase("1+2*3+5", 1 + 2 * 3 + 5)]
		[TestCase("1+2*(3+5)", 1 + 2 * (3 + 5))]
		[TestCase("1+2*(3*6+(-5))", 1 + 2 * (3 * 6 + (-5)))]
		[TestCase("1+(2*3)+5", 1 + (2 * 3) + 5)]
		[TestCase("1+(4/2*3)+5-5", 1 + (4 / 2 * 3) + 5 - 5)]
		[TestCase("-2^4", -16)]
		[TestCase(".-2^4", 16)]
		public void TestParsing(string input, double result)
		{
			double calculated = Calculator.Calculate(input);
			Assert.That(Math.Abs(calculated - result) < 0.0001);
		}

		[TestCase("55.55.55")]
		[TestCase("25+()")]
		[TestCase("25+(")]
		[TestCase(")25")]
		[TestCase("25+")]
		[TestCase("")]
		public void TestBadInput(string input)
		{
			Assert.Throws(typeof(Exception), () => Calculator.Calculate(input));
		}
	}
}
