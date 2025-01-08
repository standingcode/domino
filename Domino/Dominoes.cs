using System.ComponentModel.Design;

namespace Domino
{
	public class Dominoes
	{
		public void GenerateCheckAndPrintDominoes(int amountOfDominoesToGenerate)
		{
			var dominoes = GenerateDominoes(amountOfDominoesToGenerate);
			var dominoChainToPrint = CheckDominoesAndReturnChain(dominoes);

			if (dominoChainToPrint != null)
			{
				PrintDominosToScreen(dominoChainToPrint);
			}
		}

		public int[,]? CheckDominoesAndReturnChain(List<(int, int)> dominoes)
		{
			var dominoCircuit = CheckIfCircuitPossible(dominoes);

			if (dominoCircuit == null)
			{
				Console.WriteLine("No circuit was possible with the given dominos");
				return null;
			}

			Console.WriteLine("A circuit was possible with the given dominos:");
			return createDominoPairs(dominoCircuit);
		}

		public List<(int, int)> GenerateDominoes(int numberToGenerate)
		{
			var dominoSet = GenerateDominoSet(6);

			var rand = new Random();

			var dominoes = new List<(int, int)>();


			for (int i = 0; i < numberToGenerate; i++)
			{
				int dominoToGetFromSet = rand.Next(0, dominoSet.Count);
				dominoes.Add(dominoSet[dominoToGetFromSet]);
				dominoSet.RemoveAt(dominoToGetFromSet);
			}

			Console.WriteLine($"Generated the following dominoes:");

			PrintDominosToScreen(dominoes);

			return dominoes;
		}

		public List<(int, int)> GenerateDominoSet(int maxNumberOfDots)
		{
			var DominoSet = new List<(int, int)>();

			for (int i = 0; i <= maxNumberOfDots; i++)
			{
				for (int j = i; j <= maxNumberOfDots; j++)
				{
					DominoSet.Add((i, j));
				}
			}

			return DominoSet;
		}

		public List<int>? CheckIfCircuitPossible(List<(int, int)> dominoes)
		{
			// Cannot build a chain with no existing dominoes or one domino
			if (dominoes.Count < 2)
			{
				return null;
			}

			// Dictionary is created of all nodes with their adjacent nodes
			var graph = ReturnGraphRepresentationOfDominos(dominoes);

			// Check if all the nodes contain even number of adjacent nodes 
			if (!ValidateEvenDegrees(graph))
			{
				return null;
			}

			return VerifyEulerianCycle(graph);
		}

		private static Dictionary<int, List<int>> ReturnGraphRepresentationOfDominos(List<(int, int)> dominoes)
		{
			Dictionary<int, List<int>> values = new Dictionary<int, List<int>>();

			for (int i = 0; i < dominoes.Count; i++)
			{
				if (!values.ContainsKey(dominoes[i].Item1))
				{
					values[dominoes[i].Item1] = new List<int>();
				}
				if (!values.ContainsKey(dominoes[i].Item2))
				{
					values[dominoes[i].Item2] = new List<int>();
				}
				values[dominoes[i].Item1].Add(dominoes[i].Item2);
				values[dominoes[i].Item2].Add(dominoes[i].Item1);
			}

			return values;
		}

		private static bool ValidateEvenDegrees(Dictionary<int, List<int>> dominoesGraph)
		{
			// All nodes must have even number of adjacent nodes
			return dominoesGraph.All(x => x.Value.Count % 2 == 0);
		}

		private static List<int>? VerifyEulerianCycle(Dictionary<int, List<int>> dominoesGraph)
		{
			int startNode = dominoesGraph.FirstOrDefault(x => x.Value.Count > 0).Key;

			// If none of the nodes contains adjacent values, this cannot be a domino chain
			if (startNode == -1)
			{
				return null;
			}

			Stack<int> stack = new Stack<int>();
			HashSet<int> visited = new HashSet<int>();
			//List<(int, int)> dominoChain = new List<(int, int)>();
			List<int> dominoChain = new List<int>();

			stack.Push(startNode);

			while (stack.Count > 0)
			{
				int top = stack.Pop();

				// If the top of the stack does not exist in the visited hash set
				if (!visited.Contains(top))
				{
					// Add the top to the visited hash set
					visited.Add(top);
					dominoChain.Add(top);

					// For each of the adjacent values for the current node
					foreach (var adjacentValue in dominoesGraph[top])
					{


						//// Add to the chain
						//if (dominoChain.Count == 0 || dominoChain[dominoChain.Count - 1].Item2 == adjacentValue)
						//	dominoChain.Add((adjacentValue, top));
						//else if (dominoChain[dominoChain.Count - 1].Item2 == top)
						//	dominoChain.Add((top, adjacentValue));

						// If the adjacent value does not exist in the visited hash set, push the adjacent value to the stack
						if (!visited.Contains(adjacentValue))
						{
							// Push adjacent value to the stack
							stack.Push(adjacentValue);
						}
					}
				}
			}

			foreach (var node in dominoesGraph)
			{
				if (node.Value.Count > 0 && !visited.Contains(node.Key))
					return null;
			}


			//createDominoPairs(dominoChain);

			//return new List<(int, int)>();

			//return createDominoPairs(dominoChain);

			return dominoChain;
		}

		private static int[,] createDominoPairs(List<int> eulerianCycle)
		{
			int[,] pairs = new int[eulerianCycle.Count, 2]; ;
			for (var i = 0; i < eulerianCycle.Count; i++)
			{
				var next = i == eulerianCycle.Count - 1 ? 0 : i + 1;
				pairs[i, 0] = eulerianCycle[i];
				pairs[i, 1] = eulerianCycle[next];
			}
			return pairs;
		}

		private static void PrintDominosToScreen(List<(int, int)> dominoes)
		{
			foreach (var domino in dominoes)
			{
				Console.WriteLine(domino);
			}
		}

		private static void PrintDominosToScreen(int[,] dominoes)
		{
			var listOfDominoes = new List<(int, int)>();

			for (int i = 0; i < dominoes.Length / 2; i++)
			{
				listOfDominoes.Add((dominoes[i, 0], dominoes[i, 1]));
			}

			PrintDominosToScreen(listOfDominoes);
		}
	}
}
