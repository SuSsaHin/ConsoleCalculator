using System.Collections.Generic;

namespace ConsoleCalculator
{
	interface IOperator
	{
		uint Priority { get; }
		int ArgsCount { get; }
		double Execute(List<double> args);
	}
}
