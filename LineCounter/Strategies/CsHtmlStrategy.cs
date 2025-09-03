namespace KbgSoft.LineCounter.Strategies {
	internal class CsHtmlStrategy : IStrategy {
		private static readonly TrimStringLens l = new TrimStringLens();

		public string StatisticsKey => "cshtml";

		public Statistics Count(string path)
		{
			using (TextReader reader = File.OpenText(path))
			{
				var lines = new MultiLineCommentFilterStream().ReadLines(reader);

				var res = new Statistics()
				{
                    Files = 1,
				};
				foreach (var line in lines)
				{
					l.SetValue(line);

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
}