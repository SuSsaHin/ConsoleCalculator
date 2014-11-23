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
			Decimal
		}

		private static State currentState;
		private static string currentNumber;

		public static double Calculate(string input)
		{
			currentNumber = "";
			currentState = State.Initial;

			foreach (var c in input)
			{
				switch (currentState)
				{
					case State.Initial:
						ProcessInitial(c);
						break;
					case State.Integer:
						ProcessInteger(c);
						break;
					case State.Decimal:
						ProcessDecimal(c);
						break;
				}
			}

			return Double.Parse(currentNumber, new CultureInfo("en-US"));
		}

		private static void ProcessInitial(char c)
		{
			if (Char.IsDigit(c))
			{
				currentState = State.Integer;
				ProcessInteger(c);
			}
			else 
			{
				throw new Exception("Unexpected char in input: " + c);
			}
		}

		private static void ProcessInteger(char c)
		{
			if (Char.IsDigit(c))
			{
				currentNumber += c;
			}
			else if (c == '.')
			{
				currentNumber += c;
				currentState = State.Decimal;
			}
			else
			{
				throw new Exception("Unexpected char in input: " + c);
			}
		}

		private static void ProcessDecimal(char c)
		{
			if (Char.IsDigit(c))
			{
				currentNumber += c;
			}
			else
			{
				throw new Exception("Unexpected char in input: " + c);
			}
		}

	}
}
