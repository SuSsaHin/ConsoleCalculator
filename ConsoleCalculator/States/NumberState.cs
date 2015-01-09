using System;
using System.Globalization;

namespace ConsoleCalculator
{
	public partial class Calculator
	{
		private sealed class NumberState : IState 
		{
			private string number;
			private bool hasDot;

			public NumberState(char c)
			{
				Process(c);
			}

			public void ProcessChar(CalculatorContext context, char c)
			{
				var nextState = GetNext(c);

				if (nextState == null)
				{
					Process(c);
					return;
				}

				Complete(context);
				context.CurrentState = nextState;
			}

			private void Process(char c)
			{
				number += c;
			}

			public void Complete(CalculatorContext context)
			{
				context.PushNumber(Double.Parse(number, new CultureInfo("en-US")));
			}

			private IState GetNext(char c)
			{
				if (Char.IsDigit(c))
					return null;

				if (c == CalculatorContext.DecimalSeparator)
				{
					if (hasDot)
						throw new Exception("Unexpected " + CalculatorContext.DecimalSeparator);

					hasDot = true;
					return null;
				}

				if (c == CalculatorContext.RightBracket || c == CalculatorContext.LeftBracket)
					throw new Exception("Unexpected " + c);

				return new OperatorState(c, 2);
			}
		}
	}
}
