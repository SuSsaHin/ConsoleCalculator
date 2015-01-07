using System;

namespace ConsoleCalculator
{
	public partial class Calculator
	{
		private class InitialState : IState
		{
			public void ProcessChar(CalculatorContext calculator, char c)
			{
				IState nextState;
				if (Char.IsDigit(c))
				{
					nextState = new NumberState(c);
				}
				else if (c == '(')
				{
					nextState = new InnerExpressionState();
				}
				else if (c == ')')
				{
					throw new Exception("Unexpected " + c);
				}
				else
				{
					nextState = new UnaryOperatorState(c);
				}

				calculator.CurrentState = nextState;
			}

			public void Complete(CalculatorContext calculator)
			{
				throw new Exception("Empty input");
				//calculator.PushNumber(0);
			}
		}
	}
}
