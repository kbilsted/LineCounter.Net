using System.IO;

namespace KbgSoft.LineCounter.Strategies
{
	public class SqlStrategy : IStrategy
	{
		private static readonly TrimStringLens l = new TrimStringLens();

		public string StatisticsKey => "Sql";

		public Statistics Count(string path)
		{
			var lines = File.ReadAllLines(path);

			return Count(lines);
		}

		public Statistics Count(string[] lines)
		{
			var res = new Statistics();

			foreach (var line in lines)
			{
				l.SetValue(line);
				if (l.IsWhitespace())
					continue;

				if (l.StartsWithOrdinal("--"))
					continue;

				if (l == "/" || l=="THEN" || l == "END;" || l == "END IF;" || l == "GO;")
					continue;

				res.CodeLines++;
			}

			return res;
		}
	}
}