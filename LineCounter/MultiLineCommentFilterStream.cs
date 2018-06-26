using System;
using System.Collections.Generic;
using System.IO;

namespace KbgSoft.LineCounter
{
	public class MultiLineCommentFilterStream
	{
		public IEnumerable<string> ReadLines(TextReader tr)
		{
			bool isInsideMultilineComment = false;

			string line;
			while ((line = tr.ReadLine()) != null)
			{
				if (isInsideMultilineComment)
				{
					int multilineCommentPos = line.IndexOf("*/", StringComparison.Ordinal);
					if (multilineCommentPos == -1)
					{
						continue;
					}
					else
					{
						isInsideMultilineComment = false;
						yield return line.Substring(multilineCommentPos + 2);
					}
				}
				else
				{
					if (string.IsNullOrWhiteSpace(line))
					{
						continue;
					}

					var multilineCommentPos = line.IndexOf("/*", 0, StringComparison.Ordinal);

					if (multilineCommentPos == -1)
					{
						yield return line;
					}
					else
					{
						isInsideMultilineComment = true;
						yield return line.Substring(multilineCommentPos);
					}
				}
			}
		}
	}
}