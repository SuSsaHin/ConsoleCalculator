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

		private State currentState = State.Initial;
		private string currentValue = "";

		private readonly Stack<Operator> operators = new Stack<Operator>();
		private readonly Stack<double> numbers = new Stack<double>();

		private uint priorityDisp;
		private readonly uint priorityStep = Operators.MaxPriority;

		private void ExecuteAllOperators()
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

		private void ChangeState(State newState)
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

		private void ExecuteOperators()
		{
			var currentOperator = Operators.Get(currentValue);
			currentOperator.Priority += priorityDisp;

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

		private void SaveNumber()
		{
			var number = Double.Parse(currentValue, new CultureInfo("en-US"));
			numbers.Push(number);
		}

		private State ProcessState(char c)
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

		private State ProcessInitial(char c)
		{
			if (c == '(')
			{
				priorityDisp += priorityStep;
				return State.Initial;
			}

			if (Char.IsDigit(c))
				return State.Number;
			
			throw new Exception("Unexpected char in input: " + c);
		}

		private State ProcessNumber(char c)
		{
			if (Char.IsDigit(c) || (c == '.' && !currentValue.Contains(".")))
			{
				currentValue += c;
				return State.Number;
			}

			if (c == ')')
			{
				if(priorityDisp < priorityStep)
					throw new Exception("Unexpected ')'");

				priorityDisp -= priorityStep;
				return State.Number;
			}

			return State.Operator;
		}

		private State ProcessOperator(char c)
		{
			if (Char.IsDigit(c))
				return State.Number;

			if (c == '(')
				return State.Initial;

			currentValue += c;
			return State.Operator;

		}

		private double GetAnswer(string input)
		{
			currentValue = "";
			currentState = State.Initial;
			priorityDisp = 0;

			foreach (var c in input)
			{
				if (c == ' ')
					continue;

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

		public static double Calculate(string input)
		{
			var calculator = new Calculator();
			return calculator.GetAnswer(input);
		}
	}
}
