using System.IO;
﻿using System.Collections.Generic;

namespace KbgSoft.LineCounter.Strategies
{
	public class JSStrategy : IStrategy
	{
		private static readonly TrimStringLens l = new TrimStringLens();

		public string StatisticsKey => "Javascript";

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