using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ConsoleCalculator
{
	public partial class Calculator
	{
		private class StateMachine
		{
			private enum State
			{
				Number,
				UnaryOperator,
				BinaryOperator,
				LeftBracket,
				RightBracket,
				Same	//The state hasn't changed
			}

			private readonly Stack<Operator> operators = new Stack<Operator>();
			private readonly Stack<double> numbers = new Stack<double>();

			private string currentValue = "";
			private State currentState = State.LeftBracket;		//Initial state is equal to LeftBracket state

			private uint priorityOffset;
			private static readonly uint priorityStep = Operators.MaxPriority;

			private State GetNextState(char c)
			{
				switch (currentState)
				{
					case State.Number:
						return GetStateAfterNumber(c);
					case State.UnaryOperator:
					case State.BinaryOperator:
						return GetStateAfterOperator(c);
					case State.LeftBracket:
						return GetStateAfterLeftBracket(c);
					case State.RightBracket:
						return GetStateAfterRightBracket(c);
				}
				throw new Exception("Unexpected state");
			}

			private static State GetStateAfterRightBracket(char c)
			{
				if (c == ')')
					return State.RightBracket;

				if (Char.IsDigit(c))
					throw new Exception("Unexpected number after ')'");

				return State.BinaryOperator;
			}

			private static State GetStateAfterLeftBracket(char c)
			{
				if (c == '(')
					return State.LeftBracket;

				if (Char.IsDigit(c))
					return State.Number;

				return State.UnaryOperator;
			}

			private static State GetStateAfterNumber(char c)
			{
				if (Char.IsDigit(c) || c == '.')	//Multi-dot check made before pushing
					return State.Same;

				if (c == ')')
					return State.RightBracket;

				if (c == '(')
					throw new Exception("Unexpected '('");

				return State.BinaryOperator;
			}

			private static State GetStateAfterOperator(char c)
			{
				if (Char.IsDigit(c))
					return State.Number;

				if (c == '(')
					return State.LeftBracket;

				if (c == ')')
					throw new Exception("Unexpected ')'");

				return State.Same;
			}

			private void ExecuteOperator(Operator executed)
			{
				if (executed.IsUnary)
				{
					numbers.Push(executed.UnaryFunction(numbers.Pop()));
					return;
				}

				var arg2 = numbers.Pop();
				var arg1 = numbers.Pop();
				numbers.Push(executed.BinaryFunction(arg1, arg2));
			}

			private void ExecuteOperators(uint minPriority = 0)
			{
				while (operators.Count != 0)
				{
					var frontOperator = operators.Peek();
					if (frontOperator.Priority < minPriority)
						break;

					ExecuteOperator(frontOperator);
					operators.Pop();
				}
			}

			private void PushOperator(Operator currentOperator)
			{
				currentOperator.Priority += priorityOffset;

				ExecuteOperators(currentOperator.Priority);
				operators.Push(currentOperator);
			}

			private void PushNumber(string number)
			{
				if (number.Count(c => c == '.') > 1)
					throw new Exception("Unexpected '.' in number");

				numbers.Push(Double.Parse(number, new CultureInfo("en-US")));
			}

			private void CompleteCurrentState()
			{
				switch (currentState)
				{
					case State.UnaryOperator:
						PushOperator(Operators.GetUnary(currentValue));
						break;
					case State.BinaryOperator:
						PushOperator(Operators.GetBinary(currentValue));
						break;
					case State.Number:
						PushNumber(currentValue);
						break;
				}
			}

			public void ProcessCharacter(char c)
			{
				if (c == ' ')
					return;

				var newState = GetNextState(c);
				if (newState != State.Same)
				{
					CompleteCurrentState();
					currentValue = "";
					currentState = newState;
				}

				switch (currentState)
				{
					case State.LeftBracket:
						priorityOffset += priorityStep;
						break;
					case State.RightBracket:
						if (priorityOffset < priorityStep)
							throw new Exception("Unexpected ')'");
						priorityOffset -= priorityStep;
						break;
					default:
						currentValue += c;
						break;
				}
			}

			public void Clear()
			{
				currentValue = "";
				currentState = State.LeftBracket;
				priorityOffset = 0;
				operators.Clear();
				numbers.Clear();
			}

			public double GetAnswer()
			{
				CompleteCurrentState();

				if (priorityOffset != 0)
					throw new Exception("Lacking ')'");

				if (numbers.Count != (operators.Count(oper => !oper.IsUnary) + 1))
					throw new Exception("Bad input expression");

				ExecuteOperators();

				return numbers.Single();
			}
		}
	}
}
