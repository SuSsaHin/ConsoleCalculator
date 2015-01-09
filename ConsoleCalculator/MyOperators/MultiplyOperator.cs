namespace ConsoleCalculator.MyOperators
{
	class MultiplyOperator : BinaryOperator
	{
		public MultiplyOperator()
			: base(2, "*")
		{}

		protected override double Execute(double arg1, double arg2)
		{
			return arg1 * arg2;
		}
	}
}
