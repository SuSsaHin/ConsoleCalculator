using System;
using System.Collections.Generic;

namespace ConsoleCalculator
{
	public abstract class UnaryOperator : IOperator
	{
		public uint Priority { get; private set; }

		public int ArgsCount
		{
			get { return 1; }
		}

		public double Execute(List<double> args)
		{
			if (args.Count != ArgsCount)
				throw new Exception("Bad arguments count for unary operator: " + args.Count);

			return Execute(args[0]);
		}

		protected abstract double Execute(double arg);

		protected UnaryOperator(uint priority)
		{
			Priority = priority;
		}
	}
}
