using System;

namespace ConsoleCalculator
{
	public partial class Calculator
	{
		private sealed class UnaryOperatorState : ActiveState
		{
			private string unaryOperator;

			public UnaryOperatorState(char c)
			{
				Process(c);
			}

			protected override void Process(char c)
			{
				unaryOperator += c;
			}

			public override void Complete(CalculatorContext calculator)
			{
				calculator.PushUnaryOperator(unaryOperator);
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