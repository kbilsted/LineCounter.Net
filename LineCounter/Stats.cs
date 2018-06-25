using System.Collections.Generic;
using System.Linq;

namespace KbgSoft.LineCounter
{
	public class Stats
	{
		public readonly Dictionary<string, Statistics> FiletypeStat = new Dictionary<string, Statistics>();
		public readonly Statistics Total = new Statistics();

		public void Add(string statisticsKey, Statistics stats)
		{
			Total.CodeLines += stats.CodeLines;
			Total.DocumentationLines += stats.DocumentationLines;

			if (!FiletypeStat.ContainsKey(statisticsKey))
				FiletypeStat.Add(statisticsKey, new Statistics());
			FiletypeStat[statisticsKey].CodeLines += stats.CodeLines;
			FiletypeStat[statisticsKey].DocumentationLines += stats.DocumentationLines;
		}

		public string Print()
		{
			var formatter = new WebFormatter();
			return string.Join("\n", FiletypeStat.Select(x => $"{x.Key,15}: {formatter.ToString(x.Value)}"));
		}
	}
}