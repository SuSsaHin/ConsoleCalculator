using System;
using ConsoleCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace TestCalculator
{
	[TestClass]
	public class UnitTest1
	{
		[TestCase("55")]
		[TestCase("10")]
		[TestCase("1.0")]
		public void TestParsing(string input)
		{
			Calculator.Calculate(input);
		}
	}
}
