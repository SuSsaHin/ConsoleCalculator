using System;
using System.Collections.Generic;
using ConsoleCalculator.MyOperators;

namespace ConsoleCalculator
{
	public class Operators
	{
		private static readonly Dictionary<string, IOperator> operators = new Dictionary<string, IOperator>();

		static Operators()
		{
			Add(new PlusOperator());
			Add(new DivisionOperator());
			Add(new MinusOperator());
			Add(new MultiplyOperator());

			Add(new MinusUnaryOperator());
		}

		public static IOperator Get(string text, int dimension)
		{
			var key = text + " /" + dimension;
			try
			{
				return operators[key];
			}
			catch (KeyNotFoundException)
			{
				throw new Exception("Unknown operator: " + key);
			}
		}

		private static void Add(IOperator oper)
		{
			operators.Add(oper.Text + " /" + oper.Dimension, oper);
		}
	}
}
