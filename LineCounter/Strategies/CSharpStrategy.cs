using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;

namespace KbgSoft.LineCounter.Strategies {
	public class CSharpStrategy : IStrategy {
		private static readonly TrimStringLens l = new TrimStringLens();
        private readonly HashSet<int> SeenBefore = new HashSet<int>();
		private bool foundTests = false;
		public string StatisticsKey => "C#";
        Statistics res = new Statistics();

		public Statistics Count(string path) {
			using (TextReader reader = File.OpenText(path))
			{
			    var filteredFileContant = new MultiLineCommentFilterStream().ReadLines(reader).ToArray();

			    var hash = string.Join("", filteredFileContant).GetHashCode();
			    if (SeenBefore.Contains(hash))
			        return new Statistics();
			    SeenBefore.Add(hash);

				if(path.ToLower().Contains(".Test"))
					foundTests = true;
			    return Count(filteredFileContant);
			}
		}

		public Statistics Count(IEnumerable<string> lines) {
			res.CodeLines = 0;
		    res.DocumentationLines = 0;
		    res.Files = 1;
		    foundTests = false;

			int lineCount = 0;
			foreach (var line in lines) {
				l.SetValue(line);

			    if (l == "{" || l == "}" || l == ";" || l == "};")
			        continue;

				if(!foundTests && lineCount++ < 60)
				    if (l.StartsWithOrdinal("[Test]")
						|| l.StartsWithOrdinal("[Fact]") 
						|| l.StartsWithOrdinal("[TestFixture]") 
						|| l.StartsWithOrdinal("using NUnit.") 
						|| l.StartsWithOrdinal("using Selenium.") 
						|| l.StartsWithOrdinal("using Xunit"))
						foundTests = true;

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

			if (foundTests)
			{
				res.TestCodeLines= res.CodeLines;
				res.CodeLines = 0;
			}

			return res;
		}
	}
}