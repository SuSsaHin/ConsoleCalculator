using OperatorsLibrary;

namespace ConsoleCalculator.MyOperators
{
	class MinusUnaryOperator : UnaryOperator
	{
		public MinusUnaryOperator()
			: base(1, "-")
		{}

		protected override double Execute(double arg)
		{
			return -arg;
		}
	}
}
