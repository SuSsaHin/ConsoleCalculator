using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ConsoleCalculator
{
	public partial class Calculator
	{
		private partial class StateMachine
		{
			

			private readonly Stack<Operator> operators = new Stack<Operator>();
			private readonly Stack<double> numbers = new Stack<double>();

			private string currentValue;
			private States.State currentState;

			private uint priorityOffset;
			private static readonly uint priorityStep = Operators.MaxPriority;

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
					case States.State.UnaryOperator:
						PushOperator(Operators.GetUnary(currentValue));
						break;
					case States.State.BinaryOperator:
						PushOperator(Operators.GetBinary(currentValue));
						break;
					case States.State.Number:
						PushNumber(currentValue);
						break;
				}
			}

			public void ProcessCharacter(char c)
			{
				if (c == ' ')
					return;

				var newState = States.GetNext(currentState, c);
				if (newState != States.State.Same)
				{
					CompleteCurrentState();
					currentValue = "";
					currentState = newState;
				}

				switch (currentState)
				{
					case States.State.LeftBracket:
						priorityOffset += priorityStep;
						break;
					case States.State.RightBracket:
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
				currentState = States.State.LeftBracket;		//Initial state is equal to LeftBracket state
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

			public StateMachine()
			{
				Clear();
			}
		}
	}
}
