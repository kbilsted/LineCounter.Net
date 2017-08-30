using System;
using System.IO;
using LineCounter;

namespace TeamBinary.LineCounter
{
    class FSharpStrategy : IStrategy
    {
		static TrimStringLens l = new TrimStringLens();

		public Statistics Count(string path)
        {
            var res = new Statistics();
            var lines = File.ReadAllLines(path);

			foreach (var line in lines)
			{
				l.SetValue(line);
                if (l.IsWhitespace())
                    continue;

                if (l == "/// <summary>" || l == "/// </summary>")
                    continue;

	            if (l.StartsWithOrdinal("/// "))
	            {
		            res.DocumentationLines++;
					continue;
	            }

                if (l.StartsWithOrdinal("//"))
                    continue;

                res.CodeLines++;
            }

            return res;
        }
    }
}