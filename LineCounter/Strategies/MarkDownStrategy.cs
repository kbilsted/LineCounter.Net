using System.IO;

namespace KbgSoft.LineCounter.Strategies {
	internal class MarkDownStrategy : IStrategy {
		private const int LineWidth = 75;

		public Statistics Count(string path) {
			var res = new Statistics();
			var lines = File.ReadAllLines(path);

			foreach (var line in lines) {
				var l = line.Trim();
				if (string.IsNullOrWhiteSpace(l))
					continue;

				res.DocumentationLines += l.Length / LineWidth;
				res.DocumentationLines += l.Length % LineWidth == 0 ? 0 : 1;
			}

			return res;
		}
	}
}