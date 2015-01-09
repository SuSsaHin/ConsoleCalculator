using System;

namespace ConsoleCalculator
{
	public class Operator : IOperator
	{
		public Func<double, double, double> BinaryFunction { get; private set; }
		public Func<double, double> UnaryFunction { get; private set; }
		public uint Priority { get; set; }
		public bool IsUnary { get; private set; }

		public Operator(Operator source)
		{
			BinaryFunction = source.BinaryFunction;
			UnaryFunction = source.UnaryFunction;
			Priority = source.Priority;
			IsUnary = source.IsUnary;
		}

		public Operator(Func<double, double, double> function, uint priority)
		{
			BinaryFunction = function;
			Priority = priority;
			IsUnary = false;
		}

		public Operator(Func<double, double> function, uint priority)
		{
			UnaryFunction = function;
			Priority = priority;
			IsUnary = true;
		}
	}
}
