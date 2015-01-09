using System;

namespace ConsoleCalculator
{
	class Program
	{
		static void Main(string[] args)
		{
			var operators = new PluginsOperators();
			while (true)
			{
				var input = Console.ReadLine();
				if (input == "exit")
					break;

				try
				{
					Console.WriteLine(Calculator.Calculate(input, operators));
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}
	}
}
