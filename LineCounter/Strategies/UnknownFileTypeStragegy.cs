namespace KbgSoft.LineCounter.Strategies {
	internal class UnknownFileTypeStragegy : IStrategy {
		public Statistics Count(string path) {
			return new Statistics();
		}
	}
}