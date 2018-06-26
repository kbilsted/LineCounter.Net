using System.IO;
﻿using System.Collections.Generic;

namespace KbgSoft.LineCounter.Strategies
{
	public class SqlStrategy : IStrategy
	{
		private static readonly TrimStringLens l = new TrimStringLens();

		public string StatisticsKey => "Sql";

		public Statistics Count(string path)
		{
			using (TextReader reader = File.OpenText(path))
			{
				return Count(new MultiLineCommentFilterStream().ReadLines(reader));
			}
		}

		public Statistics Count(IEnumerable<string> lines)
		{
			var res = new Statistics();

			foreach (var line in lines)
			{
				l.SetValue(line);

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