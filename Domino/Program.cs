using System;

namespace DominoApplication
{
	internal class Program
	{
		static void Main(string[] args)
		{
			int amountOfDominoes = -1;

			var dominoes = new Dominoes();

			while (true)
			{
				while (amountOfDominoes == -1)
				{
					Console.WriteLine("How many dominos would you like to pick? (enter q to quit)");
					string input = Console.ReadLine().Trim();

					if (input == "q")
						Environment.Exit(0);

					int.TryParse(input, out amountOfDominoes);

					if (amountOfDominoes < 3 || amountOfDominoes > 28)
					{
						amountOfDominoes = -1;

						Console.WriteLine("Please enter a valid number bigger than or equal to 3 and less than or equal to 28...");
						Console.WriteLine("");
					}
				}

				dominoes.GenerateCheckAndPrintDominoes(amountOfDominoes);

				amountOfDominoes = -1;
			}
		}
	}
}