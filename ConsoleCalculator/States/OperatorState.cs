using System;

namespace ConsoleCalculator
{
	public partial class Calculator
	{
		private sealed class OperatorState : ActiveState
		{
			private string oper;
			private readonly int dimension;

			public OperatorState(char c, int dimension)
			{
				this.dimension = dimension;
				Process(c);
			}

			protected override void Process(char c)
			{
				oper += c;
			}

			public override void Complete(CalculatorContext calculator)
			{
				calculator.PushOperator(oper, dimension);
			}

			protected override IState GetNext(char c)
			{
				if (Char.IsDigit(c))
					return new NumberState(c);

				if (c == '(')
					return new InnerExpressionState();

				if (c == ')')
					throw new Exception("Unexpected ')'");

				return null;
			}
		}
	}
}