using ConsoleCalculator.MyOperators;

namespace ConsoleCalculator
{
	interface IOperators
	{
		IOperator Get(string text, int dimension);
	}
}
