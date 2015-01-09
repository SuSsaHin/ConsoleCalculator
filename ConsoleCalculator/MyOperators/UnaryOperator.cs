using System;
using System.Collections.Generic;

namespace ConsoleCalculator.MyOperators
{
	public abstract class UnaryOperator : IOperator
	{
		public uint Priority { get; private set; }

		public int Dimension
		{
			get { return 1; }
		}

		public string Text { get; private set; }
		
		public double Execute(List<double> args)
		{
			if (args.Count != Dimension)
				throw new Exception("Bad arguments count for unary operator: " + args.Count);

			return Execute(args[0]);
		}

		protected abstract double Execute(double arg);

		protected UnaryOperator(uint priority, string text)
		{
			Text = text;
			Priority = priority;
		}
	}
}
