namespace ConsoleCalculator
{
	public partial class Calculator
	{
		public static double Calculate(string input, IOperators operators)
		{
			var context = new CalculatorContext(operators);
			foreach (var c in input)
			{
				context.ProcessCharacter(c);
			}

			return context.GetAnswer();
		}
	}
}
