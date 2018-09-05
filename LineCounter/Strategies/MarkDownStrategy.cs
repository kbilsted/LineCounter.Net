using System.IO;

namespace KbgSoft.LineCounter.Strategies {
	internal class MarkDownStrategy : IStrategy {
		private const int LineWidth = 75;

		public string StatisticsKey => "Markdown";

		public Statistics Count(string path)
		{
			using (TextReader reader = File.OpenText(path))
			{
				var lines = new MultiLineCommentFilterStream().ReadLines(reader);

				var res = new Statistics()
			    {
			        Files = 1,
			    };

			    foreach (var line in lines)
				{
					var l = line.Trim();

					res.DocumentationLines += l.Length / LineWidth;
					res.DocumentationLines += l.Length % LineWidth == 0 ? 0 : 1;
				}

				return res;
			}
		}
	}
}