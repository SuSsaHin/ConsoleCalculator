using System;
using System.Collections.Generic;
using System.Linq;
using OperatorsLibrary;

namespace ConsoleCalculator
{
	public partial class Calculator
	{
		private class CalculatorContext
		{
			public const char DecimalSeparator = '.';
			public const char LeftBracket = '(';
			public const char RightBracket = ')';

			public IOperators AllOperators { get; private set; }

			private readonly Stack<IOperator> operators = new Stack<IOperator>();
			private readonly Stack<double> numbers = new Stack<double>();

			public IState CurrentState { private get; set; }

			public void PushOperator(string key, int dimension)
			{
				var oper = AllOperators.Get(key, dimension);
				ExecuteOperators(oper.Priority);
				operators.Push(oper);
			}

			public void PushNumber(double number)
			{
				numbers.Push(number);
			}

			private void ExecuteOperator(IOperator executed)
			{
				var args = new List<double>();

				for (int i = 0; i < executed.Dimension; i++)
				{
					args.Insert(0, numbers.Pop());
				}

				numbers.Push(executed.Execute(args));
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

				if (numbers.Count != (operators.Sum(oper => oper.Dimension - 1) + 1))
					throw new Exception("Bad input expression");

				ExecuteOperators();

				return numbers.Single();
			}

			public CalculatorContext(IOperators allOperators)
			{
				AllOperators = allOperators;
				CurrentState = new InitialState();
			}
		}
	}
}
