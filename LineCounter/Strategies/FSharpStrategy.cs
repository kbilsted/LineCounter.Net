using System.IO;

namespace KbgSoft.LineCounter.Strategies {
	internal class FSharpStrategy : IStrategy {
		private static readonly TrimStringLens lens = new TrimStringLens();

		public Statistics Count(string path) {
			var res = new Statistics();
			var lines = File.ReadAllLines(path);

			foreach (var line in lines) {
				lens.SetValue(line);
				if (lens.IsWhitespace())
					continue;

				if (lens == "/// <summary>" || lens == "/// </summary>")
					continue;

				if (lens.StartsWithOrdinal("/// ")) {
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