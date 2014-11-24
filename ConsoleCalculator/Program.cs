using System;

namespace ConsoleCalculator
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				try
				{
					var input = Console.ReadLine();
					if (input == "exit")
						break;

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
