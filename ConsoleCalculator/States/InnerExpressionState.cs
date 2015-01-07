using System;

namespace ConsoleCalculator
{
	public partial class Calculator
	{
		private sealed class InnerExpressionState : ActiveState
		{
			private readonly CalculatorContext innerContext = new CalculatorContext();
			private int breacketsDiff = 1;

			protected override void Process(char c)
			{
				if (c == '(')
					breacketsDiff++;
				else if (c == ')')
				{
					breacketsDiff--;

					if (breacketsDiff == 0)
						return;
				}

				innerContext.ProcessCharacter(c);
			}

			public override void Complete(CalculatorContext calculator)
			{
				if (breacketsDiff != 0)
					throw new Exception("Lacking ')'");

				calculator.PushNumber(innerContext.GetAnswer());
			}

			protected override IState GetNext(char c)
			{
				if (breacketsDiff != 0) 
					return null;

				if (c == '(' || c == ')')
					throw new Exception("Unexpected " + c);

				if (Char.IsDigit(c))
					return new NumberState(c);

				return new BinaryOperatorState(c);
			}
		}
	}
}