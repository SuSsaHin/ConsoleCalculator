using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleCalculator
{
	public class Operators
	{
		public static uint MaxPriority { get; private set; }

		private static readonly Dictionary<string, Operator> binaryOperators = new Dictionary<string, Operator>	//Operators keys can't start from '.'
		{
			{"+", new Operator((x, y) => x + y, 1) },
			{"-", new Operator((x, y) => x - y, 1) },
			{"*", new Operator((x, y) => x * y, 2) },
			{"/", new Operator((x, y) => x / y, 2) },
			{"^", new Operator(Math.Pow, 3) },
		};

		private static readonly Dictionary<string, Operator> unaryOperators = new Dictionary<string, Operator>
		{
			{"--", new Operator(x => -x, 1000) },
			{"-", new Operator(x => -x, 1) },
			{"sign", new Operator(x => Math.Sign(x), 1) },
		};

		static Operators()
		{
			MaxPriority = Math.Max(binaryOperators.Max(oper => oper.Value.Priority), unaryOperators.Max(oper => oper.Value.Priority)) + 1;
		}

		public static Operator GetBinary(string key)
		{
			try
			{
				return new Operator(binaryOperators[key]);
			}
			catch (KeyNotFoundException)
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
			catch (KeyNotFoundException)
			{
				throw new Exception("Unknown unary operator: " + key);
			}
		}
	}
}
