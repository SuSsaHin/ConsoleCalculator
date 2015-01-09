using System.Collections.Generic;

namespace ConsoleCalculator.MyOperators
{
	public interface IOperator
	{
		uint Priority { get; }
		int Dimension { get; }
		string Text { get; }
		double Execute(List<double> args);
	}
}
