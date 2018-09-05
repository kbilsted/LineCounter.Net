using System.IO;
﻿using System.Collections.Generic;
using System.Linq;

namespace KbgSoft.LineCounter.Strategies
{
	public class JSStrategy : IStrategy
	{
		private static readonly TrimStringLens l = new TrimStringLens();
	    Statistics res = new Statistics();
        private readonly HashSet<int> SeenBefore = new HashSet<int>();
        public string StatisticsKey => "Javascript";

		public Statistics Count(string path)
		{
			using (TextReader reader = File.OpenText(path))
			{
			    var filteredFileContant = new MultiLineCommentFilterStream().ReadLines(reader).ToArray();

			    var hash = string.Join("",filteredFileContant).GetHashCode();
                if(SeenBefore.Contains(hash))
                    return new Statistics();
			    SeenBefore.Add(hash);

			    return Count(filteredFileContant);
			}
		}

		public Statistics Count(IEnumerable<string> lines)
		{
		    res.CodeLines = 0;
		    res.DocumentationLines = 0;
		    res.Files = 1;

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