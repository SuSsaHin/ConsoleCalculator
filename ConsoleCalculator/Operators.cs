using System.Collections.Generic;

namespace ConsoleCalculator
{
	public class Operators
	{
		private static readonly Dictionary<string, Operator> operators = new Dictionary<string, Operator>
		{
			{"+", new Operator{ Function = Plus, Priority = 1} },
		};

		private static double Plus(double arg1, double arg2)
		{
			return arg1 + arg2;
		}

		public static Operator Get(string key)
		{
			Operator oper;
			operators.TryGetValue(key, out oper);
			return oper;
		}
	}
}
