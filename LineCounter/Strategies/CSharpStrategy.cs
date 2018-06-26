using System.IO;
﻿using System.Collections.Generic;

namespace KbgSoft.LineCounter.Strategies {
	public class CSharpStrategy : IStrategy {
		private static readonly TrimStringLens l = new TrimStringLens();
		private bool foundTests = false;
		public string StatisticsKey => foundTests ? "C# test" : "C#";

		public Statistics Count(string path) {
			using (TextReader reader = File.OpenText(path))
			{
				return Count(new MultiLineCommentFilterStream().ReadLines(reader));
			}
		}

		public Statistics Count(IEnumerable<string> lines) {
			var res = new Statistics();

			int lineCount = 0;
			foreach (var line in lines) {
				lineCount++;
				l.SetValue(line);

				if (lineCount < 40 && !foundTests && (l.StartsWithOrdinal("using NUnit.") || l.StartsWithOrdinal("using Selenium.") || l.StartsWithOrdinal("using Xunit")))
					foundTests = true;

				if (l == "{" || l == "}" || l == ";" || l == "};")
					continue;

				if (l.StartsWithOrdinal("/")) {
					if (l.StartsWithOrdinal("/// ")) {
						if (l == "/// <summary>" || l == "/// </summary>")
							continue;

						res.DocumentationLines++;
						continue;
					}

					if (l.StartsWithOrdinal("//"))
						continue;
				}

				res.CodeLines++;
			}

			return res;
		}
	}
}