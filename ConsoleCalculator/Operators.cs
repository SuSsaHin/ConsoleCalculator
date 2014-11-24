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
			{"+", new Operator(Sum, 1) },
			{"-", new Operator(Difference, 1) },
			{"*", new Operator(Mult, 2) },
			{"/", new Operator(Division, 2) },
			{"^", new Operator(Power, 3) },
		};

		private static double Power(double arg1, double arg2)
		{
			return Math.Pow(arg1, arg2);
		}

		private static readonly Dictionary<string, Operator> unaryOperators = new Dictionary<string, Operator>
		{
			{"--", new Operator(Invert, 1000) },
			{"-", new Operator(Invert, 1) },
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

		private static double Sum(double arg1, double arg2)
		{
			return arg1 + arg2;
		}

		private static double Difference(double arg1, double arg2)
		{
			return arg1 - arg2;
		}

		private static double Division(double arg1, double arg2)
		{
			return arg1 / arg2;
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
