using OperatorsLibrary;

namespace ConsoleCalculator
{
	public interface IOperators
	{
		IOperator Get(string text, int dimension);
	}
}
