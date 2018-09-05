using System.Collections.Generic;
using System.Linq;

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
	}
}