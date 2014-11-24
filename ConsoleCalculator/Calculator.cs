using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ConsoleCalculator
{
	public class Calculator
	{
		private enum State
		{
			Initial,
			Number,
			Operator,
			End
		}

		private static State currentState;
		private static string currentValue;

		private static Stack<Operator> operators = new Stack<Operator>();
		private static Stack<double> numbers = new Stack<double>(); 

		public static double Calculate(string input)
		{
			currentValue = "";
			currentState = State.Initial;

			foreach (var c in input)
			{
				var newState = ProcessState(c);
				if (newState == currentState)
					continue;

				ChangeState(newState);
				ProcessState(c);
			}
			
			ChangeState(State.End);
			ExecuteAllOperators();

			return numbers.Single();
		}

		private static void ExecuteAllOperators()
		{
			if (numbers.Count != (operators.Count + 1))
				throw new Exception("Bad input expression");

			while (operators.Count != 0)
			{
				var frontOperator = operators.Pop();
				var arg2 = numbers.Pop();
				var arg1 = numbers.Pop();

				numbers.Push(frontOperator.Function(arg1, arg2));
			}
		}

		private static void ChangeState(State newState)
		{
			if (currentState == State.Operator)
			{
				ExecuteOperators();
			}
			else if (currentState == State.Number)
			{
				SaveNumber();
			}

			currentValue = "";
			currentState = newState;
		}

		private static void ExecuteOperators()
		{
			var currentOperator = Operators.Get(currentValue);
			if(currentOperator == null)
				throw new Exception("Unknown operator");
			
			while (operators.Count != 0)
			{
				var frontOperator = operators.Peek();
				if (frontOperator.Priority < currentOperator.Priority)
					break;

				var arg2 = numbers.Pop();
				var arg1 = numbers.Pop();
				numbers.Push(frontOperator.Function(arg1, arg2));
				operators.Pop();
			}

			operators.Push(currentOperator);
		}

		private static void SaveNumber()
		{
			var number = Double.Parse(currentValue, new CultureInfo("en-US"));
			numbers.Push(number);
		}

		private static State ProcessState(char c)
		{
			switch (currentState)
			{
				case State.Initial:
					return ProcessInitial(c);
				case State.Number:
					return ProcessNumber(c);
				case State.Operator:
					return ProcessOperator(c);
			}
			throw new Exception("Unexpected state");
		}

		private static State ProcessInitial(char c)
		{
			if (Char.IsDigit(c))
				return State.Number;
			
			throw new Exception("Unexpected char in input: " + c);
		}

		private static State ProcessNumber(char c)
		{
			if (Char.IsDigit(c) || (c == '.' && !currentValue.Contains(".")))
			{
				currentValue += c;
				return State.Number;
			}

			return State.Operator;
		}

		private static State ProcessOperator(char c)
		{
			if (Char.IsDigit(c))
				return State.Number;

			currentValue += c;
			return State.Operator;

		}
	}
}
