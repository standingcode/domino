namespace Domino
{
	public class Dominoes
	{
		public void GenerateAndCheckDominoes(int amountOfDominoesToGenerate)
		{
			var Dominoes = GenerateDominoes(amountOfDominoesToGenerate);

			var dominoCircuit = DominoCircuitExists(Dominoes);
		}

		private List<(int, int)> GenerateDominoes(int numberToGenerate)
		{
			var rand = new Random();

			var dominoes = new List<(int, int)>();

			for (int i = 0; i < numberToGenerate; i++)
			{
				dominoes.Add((rand.Next(0, 7), rand.Next(0, 7)));
			}

			Console.WriteLine($"Generated the following dominoes:");

			PrintDominosToScreen(dominoes);

			return dominoes;
		}

		private List<(int, int)>? DominoCircuitExists(List<(int, int)> dominoes)
		{
			// Cannot build a chain with no existing dominoes or one domino
			if (dominoes.Count < 2)
			{
				return null;
			}

			return CheckIfChainPossible(dominoes);
		}

		private List<(int, int)>? CheckIfChainPossible(List<(int, int)> dominoes)
		{
			// Dictionary is created of all nodes with their adjacent nodes
			var graph = ReturnGraphRepresentationOfDominos(dominoes);

			// Check if all the nodes contain even number of adjacent nodes 
			if (!ValidateEvenDegrees(graph))
			{
				return null;
			}


			&& VerifyEulerianCycle(graph);
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

		private static bool VerifyEulerianCycle(Dictionary<int, List<int>> dominoesGraph)
		{
			int startNode = dominoesGraph.FirstOrDefault(x => x.Value.Count > 0).Key;

			// If none of the nodes contains adjacent values, this cannot be a domino chain
			if (startNode == -1)
			{
				return false;
			}

			Stack<int> stack = new Stack<int>();
			HashSet<int> visited = new HashSet<int>();
			List<(int, int)> dominoChain = new List<(int, int)>();

			stack.Push(startNode);

			while (stack.Count > 0)
			{
				int top = stack.Pop();

				// If the top of the stack does not exist in the visited hash set
				if (!visited.Contains(top))
				{
					// Add the top to the visited hash set
					visited.Add(top);

					// For each of the adjacent values for the current node
					foreach (var adjacentValue in dominoesGraph[top])
					{
						// If the adjacent value does not exist in the visited hash set, push the adjacent value to the stack
						if (!visited.Contains(adjacentValue))
						{
							// Add to the chain
							if (dominoChain.Count == 0 || dominoChain[dominoChain.Count - 1].Item2 == adjacentValue)
								dominoChain.Add((adjacentValue, top));
							else
								dominoChain.Add((top, adjacentValue));

							// Push adjacent value to the stack
							stack.Push(adjacentValue);
						}
					}
				}
			}

			foreach (var node in dominoesGraph)
			{
				if (node.Value.Count > 0 && !visited.Contains(node.Key))
					return false;
			}

			return true;
		}

		private static void PrintDominosToScreen(List<(int, int)> dominoes)
		{
			foreach (var domino in dominoes)
			{
				Console.WriteLine(domino);
			}
		}
	}
}
