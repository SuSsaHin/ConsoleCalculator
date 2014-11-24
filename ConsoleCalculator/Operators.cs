using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleCalculator
{
	public class Operators
	{
		private static readonly Dictionary<string, Operator> operators = new Dictionary<string, Operator>
		{
			{"+", new Operator(Plus, 1) },
			{"*", new Operator(Mult, 2) },
		};

		public static uint MaxPriority { get; private set; }

		static Operators()
		{
			MaxPriority = operators.Max(oper => oper.Value.Priority) + 1;
		}

		private static double Mult(double arg1, double arg2)
		{
			return arg1 * arg2;
		}

		private static double Plus(double arg1, double arg2)
		{
			return arg1 + arg2;
		}

		public static Operator Get(string key)
		{
			Operator oper;
			operators.TryGetValue(key, out oper);
			if(oper == null)
				throw new Exception("Unknown operator: " + key);

			return new Operator(oper);
		}
	}
}
