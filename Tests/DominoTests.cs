using Domino;

namespace TestProject1
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
		public void GetDominos_DominoGenerationOnAppStart_CorrectNumberOfDominosAreCreated(int numberOfDominoesToGenerate)
		{
			Dominoes dominoes = CreateSUT();

			(int, int)[] result = dominoes.GetDominoes(numberOfDominoesToGenerate);

			Assert.Equal(numberOfDominoesToGenerate, result.Length);
		}

		[Fact]
		public void CreateDominos_DominoGenerationOnAppStart_AllDominosNumbersAreWithinRange()
		{
			int lowerBound = 0;
			int upperBound = 6;

			Dominoes dominoes = CreateSUT();

			(int, int)[] result = dominoes.GetDominoes(100);

			foreach (var domino in result)
			{
				Assert.True(domino.Item1 >= lowerBound && domino.Item1 <= upperBound);
				Assert.True(domino.Item2 >= lowerBound && domino.Item2 <= upperBound);
			}
		}
	}
}