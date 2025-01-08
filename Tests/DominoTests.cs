using Domino;
using Microsoft.VisualStudio.TestPlatform.Common.DataCollection;

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
		public void GetDominos_DominosGeneratedWithXNumberOfDominos_CorrectNumberOfDominosAreCreated(int numberOfDominoesToGenerate)
		{
			Dominoes dominoes = CreateSUT();

			var result = dominoes.GenerateDominoes(numberOfDominoesToGenerate);

			Assert.Equal(numberOfDominoesToGenerate, result.Count);
		}

		[Fact]
		public void CreateDominos_DominosGeneratedWithXNumberOfDominos_AllDominosNumbersAreWithinRange()
		{
			int lowerBound = 0;
			int upperBound = 6;

			Dominoes dominoes = CreateSUT();

			var result = dominoes.GenerateDominoes(100);

			foreach (var domino in result)
			{
				if (domino.Item1 < lowerBound || domino.Item1 > upperBound || domino.Item2 < lowerBound || domino.Item2 > upperBound)
				{
					Assert.Fail("Domino number was out of range");
					return;
				}
			}
		}

		private List<List<(int, int)>> validDominoSets = new()
		{
			new List<(int, int)> { (3, 4), (0, 2), (0, 0), (2, 3), (4, 0) },
			new List<(int, int)> { (0, 0), (0, 0) }
		};

		[Theory]
		[InlineData(0)]
		//[InlineData(1)]
		public void DominoCircuitExists_ValidCircuitPassedForChecking_ReturnsTrue(int setToCheck)
		{
			Dominoes dominoes = CreateSUT();

			var result = dominoes.DominoCircuitExists(validDominoSets[setToCheck]);

			Assert.True(result);
		}

		[Fact]
		public void DominoCircuitExists_InvalidCircuitPassedForChecking_ReturnsFalse()
		{
			var invalidDominoCircuit = new List<(int, int)> { (1, 1), (2, 6) };

			Dominoes dominoes = CreateSUT();

			var result = dominoes.DominoCircuitExists(invalidDominoCircuit);

			Assert.False(result);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		public void DominoCircuitExists_ZeroOrOneDominoPassedForChecking_ReturnsFalse(int numberOfDominoesToGenerate)
		{
			Dominoes dominoes = CreateSUT();

			var dominoSet = dominoes.GenerateDominoes(numberOfDominoesToGenerate);

			var result = dominoes.DominoCircuitExists(dominoSet);

			Assert.False(result);
		}
	}
}