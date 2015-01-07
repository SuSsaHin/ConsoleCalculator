using System;
using System.Collections.Generic;

namespace ConsoleCalculator
{
	public abstract class BinaryOperator : IOperator
	{
		public uint Priority { get; private set; }

		public int ArgsCount
		{
			get { return 2; }
		}

		public double Execute(List<double> args)
		{
			if (args.Count != ArgsCount)
				throw new Exception("Bad arguments count for unary operator: " + args.Count);

			return Execute(args[0], args[1]);
		}

		protected abstract double Execute(double arg1, double arg2);

		protected BinaryOperator(uint priority)
		{
			Priority = priority;
		}
	}
}
