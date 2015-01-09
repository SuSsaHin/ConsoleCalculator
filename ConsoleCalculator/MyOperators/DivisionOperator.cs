namespace ConsoleCalculator.MyOperators
{
	class DivisionOperator : BinaryOperator
	{
		public DivisionOperator()
			: base(2, "/")
		{}

		protected override double Execute(double arg1, double arg2)
		{
			return arg1 / arg2;
		}
	}
}
