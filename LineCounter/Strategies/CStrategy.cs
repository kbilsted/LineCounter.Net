namespace KbgSoft.LineCounter.Strategies {
	public class CStrategy : IStrategy {
		private static readonly TrimStringLens l = new TrimStringLens();

		public string StatisticsKey => "C";

		public Statistics Count(string path) {
			using (TextReader reader = File.OpenText(path))
			{
				return Count(new MultiLineCommentFilterStream().ReadLines(reader));
			}
		}

		public Statistics Count(IEnumerable<string> lines) {
			var res = new Statistics()
			{
			    Files = 1,
			};

			foreach (var line in lines) {
				l.SetValue(line);

				if (l == "{" || l == "}" || l == ";" || l == "};")
					continue;

				if (l.StartsWithOrdinal("//"))
					continue;

				res.CodeLines++;
			}

			return res;
		}
	}
}