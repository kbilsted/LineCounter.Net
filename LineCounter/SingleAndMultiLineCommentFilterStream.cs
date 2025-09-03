namespace KbgSoft.LineCounter
{
	class SingleAndMultiLineCommentFilterStream
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

					int singlecommentPos = -1;
					int multilineCommentPos = -1;
					var slashPos = line.IndexOf("/", StringComparison.Ordinal);
					if (slashPos > -1)
					{
						singlecommentPos = line.IndexOf("//", slashPos, StringComparison.Ordinal);
						multilineCommentPos = line.IndexOf("/*", slashPos, StringComparison.Ordinal);
					}

					if (singlecommentPos == -1)
					{
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
					else
					{
						if (multilineCommentPos == -1)
						{
							yield return line.Substring(singlecommentPos);
						}
						else
						{
							if (multilineCommentPos < singlecommentPos)
							{
								isInsideMultilineComment = true;
								yield return line.Substring(multilineCommentPos);
							}
							else
							{
								yield return line.Substring(singlecommentPos);
							}
						}
					}
				}
			}
		}
	}
}
