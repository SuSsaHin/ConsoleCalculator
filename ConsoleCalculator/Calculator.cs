namespace ConsoleCalculator
{
	public partial class Calculator
	{
		public static double Calculate(string input)
		{
			var context = new CalculatorContext(new PluginsOperators());
			foreach (var c in input)
			{
				context.ProcessCharacter(c);
			}

			return context.GetAnswer();
		}
	}
}
