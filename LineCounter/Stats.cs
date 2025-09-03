namespace KbgSoft.LineCounter
{
	public class Stats
	{
		public int Files { get; private set; } = 0;
		public readonly Dictionary<string, Statistics> FiletypeStat = new Dictionary<string, Statistics>();
		public readonly Statistics Total = new Statistics();

	    public Stats()
	    {
	        FiletypeStat = new Dictionary<string, Statistics>();
	    }

	    public Stats(Dictionary<string, Statistics> x)
	    {
	        FiletypeStat = x;
	    }

		public void Add(string statisticsKey, Statistics stats)
		{
			Files++;

			Total.CodeLines += stats.CodeLines;
			Total.DocumentationLines += stats.DocumentationLines;
			Total.TestCodeLines += stats.TestCodeLines;
		    Total.Files += stats.Files;

			if (!FiletypeStat.ContainsKey(statisticsKey))
				FiletypeStat.Add(statisticsKey, new Statistics());

		    var statistics = FiletypeStat[statisticsKey];
		    statistics.CodeLines += stats.CodeLines;
			statistics.TestCodeLines += stats.TestCodeLines;
			statistics.DocumentationLines += stats.DocumentationLines;
		    statistics.Files += stats.Files;
		}

	    public Stats Minus(Stats other)
	    {
	        var result = FiletypeStat.ToDictionary(x => x.Key, x => x.Value);

	        foreach (var stat in other.FiletypeStat)
	        {
	            if(result.TryGetValue(stat.Key, out var res))
	            {
	                res.CodeLines -= stat.Value.CodeLines;
	                res.DocumentationLines -= stat.Value.DocumentationLines;
	                res.TestCodeLines -= stat.Value.TestCodeLines;
	                res.Files -= stat.Value.Files;
	            }
	            else
	            {
	                result.Add(stat.Key,
	                    new Statistics()
	                    {
	                        CodeLines = -stat.Value.CodeLines,
	                        DocumentationLines = -stat.Value.DocumentationLines,
                            TestCodeLines = -stat.Value.TestCodeLines,
                            Files = -stat.Value.Files,
	                    });
	            }
	        }

	        var stats = new Stats(result);
	        return stats;
	    }

	    public Stats Plus(Stats another)
	    {
	        var result = FiletypeStat.ToDictionary(x => x.Key, x => x.Value);

	        foreach (var stat in another.FiletypeStat)
	        {
	            if (result.TryGetValue(stat.Key, out var res))
	            {
	                res.CodeLines += stat.Value.CodeLines;
	                res.DocumentationLines += stat.Value.DocumentationLines;
	                res.TestCodeLines+= stat.Value.TestCodeLines;
	                res.Files+= stat.Value.Files;
	            }
	            else
	            {
	                result.Add(stat.Key,
	                    new Statistics()
	                    {
	                        CodeLines = stat.Value.CodeLines,
	                        DocumentationLines = stat.Value.DocumentationLines,
	                        TestCodeLines = stat.Value.TestCodeLines,
                            Files = stat.Value.Files,
	                    });
	            }
	        }

	        var stats = new Stats(result);
	        stats.Files = Files + another.Files;
	        return stats;
	    }

        // example of linq usage
	    //public Stats Minus2(Stats other)
	    //{
	    //    var added = FiletypeStat.Keys.Except(other.FiletypeStat.Keys)
	    //        .Select(x => new { x, res = FiletypeStat[x] })
	    //        .ToDictionary(x => x.x, x => x.res);
	    //    var removed = other.FiletypeStat.Keys.Except(FiletypeStat.Keys)
	    //        .Select(x => new { x, res = other.FiletypeStat[x] })
	    //        .ToDictionary(x => x.x, x => x.res);
	    //    var calc = FiletypeStat.Keys.Intersect(other.FiletypeStat.Keys)
	    //        .Select(x => new { x, res = FiletypeStat[x] - other.FiletypeStat[x] })
	    //        .ToDictionary(x => x.x, x => x.res);

	    //    var result = added.Union(removed).Union(calc).ToDictionary(x => x.Key, x => x.Value);

	    //    return new Stats(result);
	    //}


        public string Print()
	    {
	        var formatter = new WebFormatter();
	        var lines = FiletypeStat
	            .OrderBy(x => x.Key)
                //.Select(x => $"{x.Key,15}:, {formatter.PrintOnlyCode(x.Value)}");
                .Select(x => $"{x.Key,15}:, {formatter.ToString(x.Value)}");
            return string.Join("\n", lines);
	    }
	}
}