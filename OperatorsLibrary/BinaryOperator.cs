using System;
using System.Collections.Generic;

namespace OperatorsLibrary
{
	public abstract class BinaryOperator : IOperator
	{
		public uint Priority { get; private set; }

		public string Text { get; private set; }

		public int Dimension
		{
			get { return 2; }
		}

		public double Execute(List<double> args)
		{
			if (args.Count != Dimension)
				throw new Exception("Bad arguments count for unary operator: " + args.Count);

			return Execute(args[0], args[1]);
		}

		protected abstract double Execute(double arg1, double arg2);

		protected BinaryOperator(uint priority, string text)
		{
			Text = text;
			Priority = priority;
		}
	}
}
