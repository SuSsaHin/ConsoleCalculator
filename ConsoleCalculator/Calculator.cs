namespace ConsoleCalculator
{
	public partial class Calculator
	{
		public static double Calculate(string input)
		{
			var stateMachine = new StateMachine();
			foreach (var c in input)
			{
				stateMachine.ProcessCharacter(c);
			}

			return stateMachine.GetAnswer();
		}
	}
}
