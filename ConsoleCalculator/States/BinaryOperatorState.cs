using System;

namespace ConsoleCalculator
{
	public partial class Calculator
	{
		private sealed class BinaryOperatorState : ActiveState
		{
			private string binaryOperator;

			public BinaryOperatorState(char c)
			{
				Process(c);
			}

			protected override void Process(char c)
			{
				binaryOperator += c;
			}

			public override void Complete(CalculatorContext calculator)
			{
				calculator.PushBinaryOperator(binaryOperator);
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