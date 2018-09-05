namespace KbgSoft.LineCounter.Strategies {
	internal class UnknownFileTypeStragegy : IStrategy {
        Statistics s = new Statistics(){Files = 1};
		public string StatisticsKey => "unknown";

	    public Statistics Count(string path)
	    {
            return s;
        }
	}
}