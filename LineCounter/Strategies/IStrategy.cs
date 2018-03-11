namespace KbgSoft.LineCounter.Strategies {
	public interface IStrategy {
		Statistics Count(string path);
	}
}