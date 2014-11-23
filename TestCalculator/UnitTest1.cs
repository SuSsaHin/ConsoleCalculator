using System;
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
		public void TestOperators(string key, double arg1, double arg2, double result)
		{
			var oper = Operators.Get(key);
			Assert.That(Math.Abs(oper.Function(arg1, arg2) - result) < 0.0001);
		}

		[TestCase("55", 55)]
		[TestCase("10", 10)]
		[TestCase("1.0", 1)]
		[TestCase("1+2", 3)]
		public void TestParsing(string input, double result)
		{
			Assert.That(Math.Abs(Calculator.Calculate(input) - result) < 0.0001);
		}
	}
}
