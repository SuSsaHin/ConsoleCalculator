using System;

namespace ConsoleCalculator
{
	public partial class Calculator
	{
		private class InitialState : IState
		{
			public void ProcessChar(CalculatorContext context, char c)
			{
				IState nextState;
				if (Char.IsDigit(c))
				{
					nextState = new NumberState(c);
				}
				else if (c == CalculatorContext.LeftBracket)
				{
					nextState = new InnerExpressionState(c, context);
				}
				else if (c == CalculatorContext.RightBracket)
				{
					throw new Exception("Unexpected " + c);
				}
				else
				{
					nextState = new OperatorState(c, 1);
				}

				context.CurrentState = nextState;
			}

			public void Complete(CalculatorContext context)
			{
				throw new Exception("Empty input");
				//calculator.PushNumber(0);
			}
		}
	}
}
