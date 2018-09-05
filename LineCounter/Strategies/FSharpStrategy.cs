using System.IO;

namespace KbgSoft.LineCounter.Strategies {
	internal class FSharpStrategy : IStrategy {
		private static readonly TrimStringLens lens = new TrimStringLens();

		public string StatisticsKey => "F#";

		public Statistics Count(string path) {
			using (TextReader reader = File.OpenText(path))
			{
				var lines = new MultiLineCommentFilterStream().ReadLines(reader);

				var res = new Statistics()
				{
				    Files = 1,
				};

                foreach (var line in lines)
				{
					lens.SetValue(line);

					if (lens.StartsWithOrdinal("/// "))
					{
						res.DocumentationLines++;
						continue;
					}

					if (lens.StartsWithOrdinal("//"))
						continue;

					res.CodeLines++;
				}

				return res;
			}
		}
	}
}