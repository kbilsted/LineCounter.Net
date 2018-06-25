namespace KbgSoft.LineCounter {
	public class WebFormatter {
		public string CreateGithubShields(Statistics stats) {
			var str = @"[![Stats](https://img.shields.io/badge/Code_lines-{0}-ff69b4.svg)]()
[![Stats](https://img.shields.io/badge/Doc_lines-{1}-ff69b4.svg)]()";
			var res = string.Format(str, Format(stats.CodeLines), Format(stats.DocumentationLines));

			return res;
		}

		private string Format(int count) {
			if (count < 1000)
				return "" + count;
			return string.Format("{0:0.0}_K", count / 1000M);
		}

		private string FormatWholeK(int count)
		{
			return count < 1000 ? $"{count}" : $"{count / 1000M:0}K";
		}

		public string ToString(Statistics stats)
		{
			return $@"Code: {FormatWholeK(stats.CodeLines),5}  Doc: {FormatWholeK(stats.DocumentationLines),5}";
		}
	}
}