namespace KbgSoft.LineCounter.Strategies {
	internal class UnknownFileTypeStragegy : IStrategy {
		public string StatisticsKey => "unknown";

		public Statistics Count(string path) {
			return new Statistics();
		}
	}
}