namespace ConsoleCalculator
{
	public partial class Calculator
	{
		private abstract class ActiveState : IState
		{
			public void ProcessChar(CalculatorContext calculator, char c)
			{
				var nextState = GetNext(c);

				if (nextState == null)
				{
					Process(c);
					return;
				}

				Complete(calculator);
				calculator.CurrentState = nextState;
			}

			public abstract void Complete(CalculatorContext calculator);

			protected abstract void Process(char c);

			protected abstract IState GetNext(char c);
		}
	}
}
