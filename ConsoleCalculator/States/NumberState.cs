using System;
using System.Globalization;

namespace ConsoleCalculator
{
	public partial class Calculator
	{
		private sealed class NumberState : ActiveState
		{
			private string number;
			private bool hasDot;

			public NumberState(char c)
			{
				Process(c);
			}

			protected override void Process(char c)
			{
				number += c;
			}

			public override void Complete(CalculatorContext calculator)
			{
				calculator.PushNumber(Double.Parse(number, new CultureInfo("en-US")));
			}

			protected override IState GetNext(char c)
			{
				if (Char.IsDigit(c))
					return null;

				if (c == '.')
				{
					if (hasDot)
						throw new Exception("Unexpected '.'");

					hasDot = true;
					return null;
				}

				if (c == ')')
					throw new Exception("Unexpected ')'");

				if (c == '(')
					throw new Exception("Unexpected '('");

				return new OperatorState(c, 2);
			}
		}
	}
}
