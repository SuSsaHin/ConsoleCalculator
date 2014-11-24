using System;
using System.Globalization;

namespace ConsoleCalculator
{
	public class Calculator
	{
		private enum State
		{
			Initial,
			Integer,
			Decimal,
			Operator
		}

		private static State currentState;
		private static string currentValue;

		public static double Calculate(string input)
		{
			currentValue = "";
			currentState = State.Initial;

			foreach (var c in input)
			{
				var newState = ProcessState(c);
				if (newState == currentState)
					continue;

				ChangeState(c, newState);
			}

			return Double.Parse(currentValue, new CultureInfo("en-US"));
		}

		private static void ChangeState(char c, State newState)
		{
			if (currentState == State.Operator)
			{
				ExecuteOperator();
			}
			else if (currentState == State.Decimal || (currentState == State.Integer && newState != State.Decimal))
			{
				SaveNumber();
			}
			currentState = newState;

			if (newState == State.Decimal)
				return;

			ProcessState(c);
		}

		private static State ProcessState(char c)
		{
			switch (currentState)
			{
				case State.Initial:
					return ProcessInitial(c);
				case State.Integer:
					return ProcessInteger(c);
				case State.Decimal:
					return ProcessDecimal(c);
				case State.Operator:
					return ProcessOperator(c);
			}
			throw new Exception("Unexpected state");
		}

		private static State ProcessInitial(char c)
		{
			if (Char.IsDigit(c))
				return State.Integer;
			
			throw new Exception("Unexpected char in input: " + c);
		}

		private static State ProcessInteger(char c)
		{
			if (Char.IsDigit(c))
			{
				currentValue += c;
				return State.Integer;
			}

			if (c == '.')
			{
				currentValue += c;
				return State.Decimal;
			}

			//ExecuteNumber();
			return State.Operator;
		}

		private static State ProcessDecimal(char c)
		{
			if (!Char.IsDigit(c))
				return State.Operator;

			currentValue += c;
			return State.Decimal;
		}

		private static State ProcessOperator(char c)
		{
			if (Char.IsDigit(c))
				return State.Integer;

			currentValue += c;
			return State.Operator;

		}
	}
}
