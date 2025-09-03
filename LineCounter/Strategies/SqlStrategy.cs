namespace KbgSoft.LineCounter.Strategies
{
	public class SqlStrategy : IStrategy
	{
		private static readonly TrimStringLens l = new TrimStringLens();

		public string StatisticsKey => "Sql";
	    public Dictionary<string, int> sizes = new Dictionary<string, int>();
        private readonly HashSet<int> SeenBefore = new HashSet<int>();
	        
		public Statistics Count(string path)
		{
			using (TextReader reader = File.OpenText(path))
			{
			    var filteredFileContant = new MultiLineCommentFilterStream().ReadLines(reader).ToArray();
			    var joinedFiltered = string.Join("", filteredFileContant);

                sizes.Add(path, joinedFiltered.Length);

                var hash = joinedFiltered.GetHashCode();
			    if (SeenBefore.Contains(hash))
			        return new Statistics(){Files = 1};
			    SeenBefore.Add(hash);

			    return Count(filteredFileContant);
            }
		}

		public Statistics Count(IEnumerable<string> lines)
		{
			var res = new Statistics()
			{
			    Files = 1,
			};

			foreach (var line in lines)
			{
				l.SetValue(line);

				if (l.StartsWithOrdinal("--"))
					continue;

				if (l == "/" || l == "BEGIN" || l == "THEN" || l == "END;" || l == "END IF;" || l == "GO;" || l == "END LOOP;" || l == "NULL," || l == ", ''" || l == "VALUES")
					continue;

				res.CodeLines++;
			}

			return res;
		}
	}
}