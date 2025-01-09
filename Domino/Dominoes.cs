
namespace DominoApplication
{
	public class Domino
	{
		public int First { get; set; }
		public int Second { get; set; }

		public Domino(int first, int second)
		{
			First = first;
			Second = second;
		}

		public void FlipDomino()
		{
			int tempFirst = First;
			First = Second;
			Second = tempFirst;
		}

		public override string ToString()
		{
			return $"[{First}|{Second}]";
		}
	}

	public class Dominoes
	{
		private int maxNumberOfDotsOnDomino = 6;

		public void GenerateCheckAndPrintDominoes(int amountOfDominoesToGenerate)
		{
			var dominoes = GenerateDominoes(amountOfDominoesToGenerate);
			var successfulDominoChain = TryAndBuildChainsAndReturnFirstWorkingCircuit(dominoes.Count, new List<Domino>(), dominoes);

			if (successfulDominoChain == null)
			{
				Console.WriteLine("No circuit was possible with the given dominos");
				return;
			}

			Console.WriteLine("A circuit was possible with the given dominos:");
			PrintDominosToScreen(successfulDominoChain);
		}

		public List<Domino> GenerateDominoes(int numberToGenerate)
		{
			var dominoSet = GenerateDominoSet(maxNumberOfDotsOnDomino);

			var rand = new Random();

			var dominoes = new List<Domino>();

			for (int i = 0; i < numberToGenerate; i++)
			{
				int dominoToGetFromSet = rand.Next(0, dominoSet.Count);
				dominoes.Add(dominoSet[dominoToGetFromSet]);
				dominoSet.RemoveAt(dominoToGetFromSet);
			}

			Console.WriteLine($"Picked the following dominoes:");

			PrintDominosToScreen(dominoes);

			return dominoes;
		}

		public List<Domino>? TryAndBuildChainsAndReturnFirstWorkingCircuit(
		int numberOfDominos,
		List<Domino> chain,
		List<Domino> dominoes,
		List<Domino>? firstWorkingChain = null
		)
		{
			if (firstWorkingChain != null)
			{
				return firstWorkingChain;
			}

			for (int i = 0; i < dominoes.Count; ++i)
			{
				Domino proposedDomino = dominoes[i];

				if (CanAddDominoToChain(proposedDomino, chain))
				{
					chain.Add(proposedDomino);
					var savedDomino = dominoes[i];
					dominoes.RemoveAt(i);

					if (firstWorkingChain == null && chain.Count == numberOfDominos && chain.First().First == chain.Last().Second)
					{
						firstWorkingChain = CreateValueCopyOfDominoList(chain);
					}

					firstWorkingChain = TryAndBuildChainsAndReturnFirstWorkingCircuit(numberOfDominos, chain, dominoes, firstWorkingChain);

					dominoes.Insert(i, savedDomino);
					chain.RemoveAt(chain.Count - 1);
				}

				proposedDomino.FlipDomino();

				if (CanAddDominoToChain(proposedDomino, chain))
				{
					chain.Add(proposedDomino);
					var saved = dominoes[i];
					dominoes.RemoveAt(i);

					if (firstWorkingChain == null && chain.Count == numberOfDominos && chain.First().First == chain.Last().Second)
					{
						firstWorkingChain = CreateValueCopyOfDominoList(chain);
					}

					firstWorkingChain = TryAndBuildChainsAndReturnFirstWorkingCircuit(numberOfDominos, chain, dominoes, firstWorkingChain);

					dominoes.Insert(i, saved);
					chain.RemoveAt(chain.Count - 1);
				}
			}

			return firstWorkingChain;
		}

		public static List<Domino> CreateValueCopyOfDominoList(List<Domino> dominoes)
		{
			var dominoesCopy = new List<Domino>();

			foreach (var domino in dominoes)
			{
				dominoesCopy.Add(new Domino(domino.First, domino.Second));
			}

			return dominoesCopy;
		}

		public static List<Domino> GenerateDominoSet(int maxNumberOfDots)
		{
			var DominoSet = new List<Domino>();

			for (int i = 0; i <= maxNumberOfDots; i++)
			{
				for (int j = i; j <= maxNumberOfDots; j++)
				{
					DominoSet.Add(new Domino(i, j));
				}
			}

			return DominoSet;
		}

		public static bool CanAddDominoToChain(Domino domino, List<Domino> chain)
		{
			return chain == null || chain.Count == 0 || chain.Last().Second == domino?.First;
		}

		public static void PrintDominosToScreen(List<Domino> dominoes)
		{
			foreach (var domino in dominoes)
			{
				Console.WriteLine(domino);
			}
		}
	}
}
