using System;

namespace ConsoleCalculator
{
	public partial class Calculator
	{
		private sealed class OperatorState : IState
		{
			private string oper;
			private readonly int dimension;

			public OperatorState(char c, int dimension)
			{
				this.dimension = dimension;
				Process(c);
			}

			public void ProcessChar(CalculatorContext context, char c)
			{
				var nextState = GetNext(context, c);

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
				oper += c;
			}

			public void Complete(CalculatorContext context)
			{
				context.PushOperator(oper, dimension);
			}

			private IState GetNext(CalculatorContext context, char c)
			{
				if (Char.IsDigit(c))
					return new NumberState(c);

				if (c == CalculatorContext.LeftBracket)
					return new InnerExpressionState(c, context);

				if (c == CalculatorContext.RightBracket)
					throw new Exception("Unexpected " + c);

				return null;
			}
		}
	}
}