namespace KbgSoft.LineCounter.Strategies {
	public interface IStrategy {
		string StatisticsKey { get; }
		Statistics Count(string path);
	}
}