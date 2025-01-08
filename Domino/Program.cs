using System;

namespace Domino
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
					Console.WriteLine("How many dominos would you like to randomly generate? (enter q to quit)");
					string input = Console.ReadLine().Trim();

					if (input == "q")
						Environment.Exit(0);

					int.TryParse(input, out amountOfDominoes);

					if (amountOfDominoes < 1)
					{
						amountOfDominoes = -1;

						Console.WriteLine("Please enter a valid number bigger than 0...");
						Console.WriteLine("");
					}
				}

				dominoes.GenerateAndCheckDominoes(amountOfDominoes);

				amountOfDominoes = -1;
			}
		}
	}
}