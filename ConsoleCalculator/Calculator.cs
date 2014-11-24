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
			UnaryOperator,
			BinaryOperator,
			SameState
		}

		private string currentValue = "";

		private readonly Stack<Operator> operators = new Stack<Operator>();
		private readonly Stack<double> numbers = new Stack<double>();

		private uint priorityDisp;
		private readonly uint priorityStep = Operators.MaxPriority;

		private void ExecuteOperators(Operator currentOperator)
		{
			currentOperator.Priority += priorityDisp;

			while (operators.Count != 0)
			{
				var frontOperator = operators.Peek();
				if (frontOperator.Priority < currentOperator.Priority)
					break;

				ExecuteOperator(frontOperator);

				operators.Pop();
			}

			operators.Push(currentOperator);
		}

		private void ExecuteOperator(Operator oper)
		{
			if (oper.IsUnary)
			{
				numbers.Push(oper.UnaryFunction(numbers.Pop()));
			}
			else
			{
				var arg2 = numbers.Pop();
				var arg1 = numbers.Pop();
				numbers.Push(oper.BinaryFunction(arg1, arg2));
			}
		}

		private void CompleteState(State currentState)
		{
			switch (currentState)
			{
				case State.UnaryOperator:
					ExecuteOperators(Operators.GetUnary(currentValue));
					break;
				case State.BinaryOperator:
					ExecuteOperators(Operators.GetBinary(currentValue));
					break;
				case State.Number:
					SaveNumber();
					break;
			}

			currentValue = "";
		}

		private void SaveNumber()
		{
			var number = Double.Parse(currentValue, new CultureInfo("en-US"));
			numbers.Push(number);
		}

		private State ProcessState(State currentState, char c)
		{
			switch (currentState)
			{
				case State.Initial:
					return ProcessInitial(c);
				case State.Number:
					return ProcessNumber(c);
				case State.UnaryOperator:
				case State.BinaryOperator:
					return ProcessOperator(c);
			}
			throw new Exception("Unexpected state");
		}

		private State ProcessInitial(char c)
		{
			if (c == '(')
			{
				priorityDisp += priorityStep;
				return State.SameState;
			}

			if (Char.IsDigit(c))
				return State.Number;
			
			return State.UnaryOperator;
		}

		private State ProcessNumber(char c)
		{
			if (Char.IsDigit(c) || (c == '.' && !currentValue.Contains(".")))
			{
				currentValue += c;
				return State.SameState;
			}

			if (c == ')')
			{
				if (priorityDisp < priorityStep)
					throw new Exception("Unexpected ')'");

				priorityDisp -= priorityStep;
				return State.SameState;
			}

			return State.BinaryOperator;
		}

		private State ProcessOperator(char c)
		{
			if (Char.IsDigit(c))
				return State.Number;

			if (c == '(')
				return State.Initial;

			currentValue += c;
			return State.SameState;
		}

		private double GetAnswer(string input)
		{
			currentValue = "";
			var currentState = State.Initial;
			priorityDisp = 0;

			foreach (var c in input)
			{
				if (c == ' ')
					continue;

				var newState = ProcessState(currentState, c);
				if (newState == State.SameState)
					continue;

				CompleteState(currentState);
				currentState = newState;

				ProcessState(currentState, c);
			}

			CompleteState(currentState);

			if (priorityDisp != 0)
				throw new Exception("Lacking right bracket");

			if (numbers.Count != (operators.Count(oper => !oper.IsUnary) + 1))
				throw new Exception("Bad input expression");

			while (operators.Count != 0)
				ExecuteOperator(operators.Pop());

			return numbers.Single();
		}

		public static double Calculate(string input)
		{
			var calculator = new Calculator();
			return calculator.GetAnswer(input);
		}
	}
}
