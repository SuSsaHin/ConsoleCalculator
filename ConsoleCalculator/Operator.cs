using System;

namespace ConsoleCalculator
{
	public class Operator
	{
		public Func<double, double, double> Function { get; set; }
		public uint Priority { get; set; }

		public Operator(Operator source)
		{
			Function = source.Function;
			Priority = source.Priority;
		}

		public Operator(Func<double, double, double> function, uint priority)
		{
			Function = function;
			Priority = priority;
		}
	}
}
