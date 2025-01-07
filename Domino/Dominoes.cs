using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domino
{
	public class Dominoes
	{
		public (int, int)[] GetDominoes(int numberToGenerate)
		{
			var rand = new Random();

			(int, int)[] dominoes = new (int, int)[numberToGenerate];

			for (int i = 0; i < numberToGenerate; i++)
			{
				dominoes[i] = (rand.Next(0, 7), rand.Next(0, 7));
			}

			foreach (var domino in dominoes)
			{
				Console.WriteLine(domino);
			}

			return dominoes;
		}
	}

	public class Domino
	{
		protected int firstNumber;
		public int FirstNumber { get => firstNumber; set => firstNumber = value; }

		protected int secondNumber;
		public int SecondNumber { get => secondNumber; set => secondNumber = value; }
	}
}
