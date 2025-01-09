using DominoApplication;

namespace Tests
{
	public class DominoTests
	{

		private List<List<Domino>> validDominoSets = new()
		{
			new List<Domino>() { new Domino(3, 4), new Domino(0, 2), new Domino(0, 0), new Domino(2, 3), new Domino(4, 0) },
			new List<Domino>() { new Domino(3, 2), new Domino(1, 2), new Domino(3, 1) }
		};

		private List<List<Domino>> validDominoChains = new() {
			new List<Domino>() { new Domino(3, 4), new Domino(4, 0), new Domino(0, 0), new Domino(0, 2), new Domino(2, 3) },
			new List<Domino>() { new Domino(3, 2), new Domino(2, 1), new Domino(1, 3) },
		};

		private List<List<Domino>> invalidDominoSets = new() {
			new List<Domino>() { new Domino(3, 4), new Domino(2, 6) },
			new List<Domino>() { new Domino(3, 2), new Domino(2, 1) },
		};

		public Dominoes CreateSUT()
		{
			return new Dominoes();
		}

		[Fact]
		public void GenerateCheckAndPrintDominoes_CalledNormally_NoExceptionsThrown()
		{
			var dominoes = CreateSUT();

			dominoes.GenerateCheckAndPrintDominoes(28);

			Assert.True(true);
		}

		[Theory]
		[InlineData(7)]
		[InlineData(11)]
		[InlineData(28)]
		public void GenerateDominos_DominosGeneratedWithXNumberOfDominos_CorrectNumberOfDominosAreCreated(int numberOfDominoesToGenerate)
		{
			var dominoes = CreateSUT();

			var result = dominoes.GenerateDominoes(numberOfDominoesToGenerate);

			Assert.Equal(numberOfDominoesToGenerate, result.Count);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		public void TryAndBuildChainsAndReturnFirstWorkingCircuit_ValidDominoSetPassedForChecking_CorrectChainReturned(int index)
		{
			var set = validDominoSets[index];
			var chain = validDominoChains[index];

			var dominoes = CreateSUT();

			var result = dominoes.TryAndBuildChainsAndReturnFirstWorkingCircuit(set.Count, new List<Domino>(), set);

			if (result == null)
			{
				Assert.Fail();
			}

			for (int i = 0; i < result?.Count; i++)
			{
				if (!(result[i].First == chain[i].First && result[i].Second == chain[i].Second))
				{
					Assert.Fail();
				}
			}
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		public void TryAndBuildChainsAndReturnFirstWorkingCircuit_InvalidCircuitPassedForChecking_ResultShouldBeNull(int index)
		{
			var invalidSet = invalidDominoSets[index];

			var dominoes = CreateSUT();

			var result = dominoes.TryAndBuildChainsAndReturnFirstWorkingCircuit(invalidSet.Count, new List<Domino>(), invalidSet);

			Assert.Null(result);
		}

		[Fact]
		public void CreateValueCopyOfDominoList_ValidDominoListPassed_CorrectCopyReturned()
		{
			var set = validDominoSets[0];

			var result = Dominoes.CreateValueCopyOfDominoList(set);

			for (int i = 0; i < result.Count; i++)
			{
				if (!(result[i].First == set[i].First && result[i].Second == set[i].Second))
				{
					Assert.Fail();
				}
			}
		}

		[Fact]
		public void GenerateDominoSet_maxNumberOfDotsDefinedAs6_28DominosCreated()
		{
			var result = Dominoes.GenerateDominoSet(6);

			Assert.Equal(28, result.Count);
		}

		[Fact]
		public void CanAddDominoToChain_DominoPassedWhichCanBeAdded_ReturnsTrue()
		{
			var result = Dominoes.CanAddDominoToChain(new Domino(3, 4), new List<Domino>() { new Domino(0, 3) });

			Assert.True(result);
		}

		[Fact]
		public void CanAddDominoToChain_DominoPassedWhichCannotBeAdded_ReturnsFalse()
		{
			var result = Dominoes.CanAddDominoToChain(new Domino(3, 4), new List<Domino>() { new Domino(4, 0) });

			Assert.False(result);
		}
	}
}