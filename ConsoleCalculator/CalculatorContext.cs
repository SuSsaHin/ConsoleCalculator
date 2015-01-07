using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleCalculator
{
	public partial class Calculator
	{
		private class CalculatorContext
		{
			private readonly Stack<Operator> operators = new Stack<Operator>();
			private readonly Stack<double> numbers = new Stack<double>();

			public IState CurrentState { private get; set; }

			public void PushUnaryOperator(string key)
			{
				var unary = Operators.GetUnary(key);
				ExecuteOperators(unary.Priority);
				operators.Push(unary);
			}

			public void PushBinaryOperator(string key)
			{
				var binary = Operators.GetBinary(key);
				ExecuteOperators(binary.Priority);
				operators.Push(binary);
			}

			public void PushNumber(double number)
			{
				numbers.Push(number);
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

			public void ProcessCharacter(char c)
			{
				if (c == ' ')
					return;

				CurrentState.ProcessChar(this, c);
			}

			public double GetAnswer()
			{
				CurrentState.Complete(this);

				if (numbers.Count != (operators.Count(oper => !oper.IsUnary) + 1))
					throw new Exception("Bad input expression");

				ExecuteOperators();

				return numbers.Single();
			}

			public CalculatorContext()
			{
				CurrentState = new InitialState();
			}
		}
	}
}
