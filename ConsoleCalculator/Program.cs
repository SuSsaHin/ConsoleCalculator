using System;

namespace ConsoleCalculator
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				var input = Console.ReadLine();
				if (input == "exit")
					break;

				try
				{
					Console.WriteLine(Calculator.Calculate(input));
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}
	}
}
