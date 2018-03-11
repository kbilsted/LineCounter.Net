using System.IO;

namespace KbgSoft.LineCounter.Strategies {
	internal class CsHtmlStrategy : IStrategy {
		private static readonly TrimStringLens l = new TrimStringLens();

		public Statistics Count(string path) {
			var res = new Statistics();
			var lines = File.ReadAllLines(path);


			foreach (var line in lines) {
				l.SetValue(line);
				if (l.IsWhitespace())
					continue;

				if (l == "{" || l == "}" || l == ";")
					continue;

				if (l.StartsWithOrdinal("//"))
					continue;

				res.CodeLines++;
			}

			return res;
		}
	}
}