using Domino;

namespace Tests
{
	public class DominoTests
	{
		public Dominoes CreateSUT()
		{
			return new Dominoes();
		}

		[Theory]
		[InlineData(0)]
		[InlineData(7)]
		[InlineData(11)]
		[InlineData(30)]
		public void GenerateDominos_DominosGeneratedWithXNumberOfDominos_CorrectNumberOfDominosAreCreated(int numberOfDominoesToGenerate)
		{
			Dominoes dominoes = CreateSUT();

			var result = dominoes.GenerateDominoes(numberOfDominoesToGenerate);

			Assert.Equal(numberOfDominoesToGenerate, result.Count);
		}

		[Fact]
		public void CreateDominos_DominosGeneratedWithXNumberOfDominos_AllDominosNumbersAreWithinRange()
		{
			5
			int lowerBound = 0;
			int upperBound = 6;

			Dominoes dominoes = CreateSUT();

			var result = dominoes.GenerateDominoes(28);

			foreach (var domino in result)
			{
				if (domino.Item1 < lowerBound || domino.Item1 > upperBound || domino.Item2 < lowerBound || domino.Item2 > upperBound)
				{
					Assert.Fail("Domino number was out of range");
					return;
				}
			}

			//foreach (var domino in result)
			//{
			//	if (domino.Item1 < lowerBound || domino.Item1 > upperBound || domino.Item2 < lowerBound || domino.Item2 > upperBound)
			//	{
			//		Assert.Fail("Domino number was out of range");
			//		return;
			//	}
			//}
		}

		private List<List<(int, int)>> validDominoSets = new()
		{
			new List<(int, int)> { (3, 4), (0, 2), (0, 0), (2, 3), (4, 0) },
			new List<(int, int)> { (0, 0), (0, 0) }
		};

		[Theory]
		[InlineData(0)]
		//[InlineData(1)]
		//[InlineData(2)]
		public void DominoCircuitExists_ValidCircuitPassedForChecking_ResultIsNotNull(int setToCheck)
		{
			Dominoes dominoes = CreateSUT();

			var result = dominoes.CheckIfCircuitPossible(validDominoSets[setToCheck]);

			Assert.NotNull(result);
		}

		[Fact]
		public void DominoCircuitExists_InvalidCircuitPassedForChecking_ResultShouldBeNull()
		{
			var invalidDominoCircuit = new List<(int, int)> { (1, 1), (2, 6) };

			Dominoes dominoes = CreateSUT();

			var result = dominoes.CheckIfCircuitPossible(invalidDominoCircuit);

			Assert.Null(result);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		public void DominoCircuitExists_ZeroOrOneDominoPassedForChecking_ResultShouldBeNull(int numberOfDominoesToGenerate)
		{
			Dominoes dominoes = CreateSUT();

			var dominoSet = dominoes.GenerateDominoes(numberOfDominoesToGenerate);

			var result = dominoes.CheckIfCircuitPossible(dominoSet);

			Assert.Null(result);
		}

		[Fact]
		public void CheckDominosAndReturnChain_ValidDominoSetPassedForChecking_PrintsCorrectly()
		{
			Dominoes dominoes = CreateSUT();
			var result = dominoes.CheckDominoesAndReturnChain(validDominoSets[0]);
		}
	}
}