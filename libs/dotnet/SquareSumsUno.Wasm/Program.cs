using SquareSums;
//using Uno;

namespace SquareSumsUno.Wasm
{
	//[Preserve]
	public static class SquareSums
	{
		//[Preserve]
		public static int FullSquareSums(int from, int to)
		{
			var metrics = new Metrics(false);
			for (var n = from; n <= to; n++)
			{
				Calculator.SquareSumsRow(n, metrics);
			}
			metrics.PrintMetrics();
			return 0;
		}
	}
	
	public class Program
	{
		static int Main(string[] args)
		{
			
			return 0;
		}
	}
}
