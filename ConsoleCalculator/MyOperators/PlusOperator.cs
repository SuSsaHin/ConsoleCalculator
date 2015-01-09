using OperatorsLibrary;

namespace ConsoleCalculator.MyOperators
{
	class PlusOperator : BinaryOperator
	{
		public PlusOperator()
			: base(1, "+")
		{}

		protected override double Execute(double arg1, double arg2)
		{
			return arg1 + arg2;
		}
	}
}
