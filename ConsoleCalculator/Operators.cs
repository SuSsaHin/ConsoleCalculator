using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleCalculator
{
	public class Operators
	{
		public static uint MaxPriority { get; private set; }

		private static readonly Dictionary<string, Operator> binaryOperators = new Dictionary<string, Operator>
		{
			{"+", new Operator(Plus, 1) },
			{"*", new Operator(Mult, 2) },
		};

		private static readonly Dictionary<string, Operator> unaryOperators = new Dictionary<string, Operator>
		{
			{"-", new Operator(Invert, 1000) },
		};

		static Operators()
		{
			MaxPriority = Math.Max(binaryOperators.Max(oper => oper.Value.Priority), unaryOperators.Max(oper => oper.Value.Priority)) + 1;
		}

		private static double Invert(double arg)
		{
			return -arg;
		}

		private static double Mult(double arg1, double arg2)
		{
			return arg1 * arg2;
		}

		private static double Plus(double arg1, double arg2)
		{
			return arg1 + arg2;
		}

		public static Operator GetBinary(string key)
		{
			try
			{
				return new Operator(binaryOperators[key]);
			}
			catch (Exception)
			{
				throw new Exception("Unknown binary operator: " + key);
			}
		}

		public static Operator GetUnary(string key)
		{
			try
			{
				return new Operator(unaryOperators[key]);
			}
			catch (Exception)
			{
				throw new Exception("Unknown unary operator: " + key);
			}
		}
	}
}
