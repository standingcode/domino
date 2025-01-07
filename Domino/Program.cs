using System;

namespace Domino
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Application Started");

			var dominoes = new Dominoes();
			dominoes.GetDominoes(33);
		}
	}
}