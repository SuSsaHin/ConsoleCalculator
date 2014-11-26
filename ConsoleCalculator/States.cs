using System;

namespace ConsoleCalculator
{
	public partial class Calculator
	{
		private partial class StateMachine
		{
			private static class States
			{
				public enum State
				{
					Number,
					UnaryOperator,
					BinaryOperator,
					LeftBracket,
					RightBracket,
					Same	//The state hasn't changed
				}

				public static State GetNext(State currentState, char c)
				{
					switch (currentState)
					{
						case State.Number:
							return NumbersNext(c);
						case State.UnaryOperator:
						case State.BinaryOperator:
							return OperatorsNext(c);
						case State.LeftBracket:
							return LeftBracketNext(c);
						case State.RightBracket:
							return RightBracketsNext(c);
					}
					throw new Exception("Unexpected state");
				}

				private static State RightBracketsNext(char c)
				{
					if (c == ')')
						return State.RightBracket;

					if (Char.IsDigit(c))
						throw new Exception("Unexpected number after ')'");

					return State.BinaryOperator;
				}

				private static State LeftBracketNext(char c)
				{
					if (c == '(')
						return State.LeftBracket;

					if (Char.IsDigit(c))
						return State.Number;

					return State.UnaryOperator;
				}

				private static State NumbersNext(char c)
				{
					if (Char.IsDigit(c) || c == '.')	//Multi-dot check made before pushing
						return State.Same;

					if (c == ')')
						return State.RightBracket;

					if (c == '(')
						throw new Exception("Unexpected '('");

					return State.BinaryOperator;
				}

				private static State OperatorsNext(char c)
				{
					if (Char.IsDigit(c))
						return State.Number;

					if (c == '(')
						return State.LeftBracket;

					if (c == ')')
						throw new Exception("Unexpected ')'");

					return State.Same;
				}
			}
		}
	}
	
}
