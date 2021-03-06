namespace KbgSoft.LineCounter {
	public class WebFormatter {
		public string CreateGithubShields(Statistics stats) {
			var str = @"[![Stats](https://img.shields.io/badge/Code_lines-{0}-ff69b4.svg)]()
[![Stats](https://img.shields.io/badge/Test_lines-{1}-69ffb4.svg)]()
[![Stats](https://img.shields.io/badge/Doc_lines-{2}-ffb469.svg)]()";
			var res = string.Format(str, Format(stats.CodeLines), Format(stats.TestCodeLines), Format(stats.DocumentationLines));

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

	    public string PrintOnlyCode(Statistics stats)
	    {
	        return $@"{(stats.CodeLines.ToString("N0")),7}";
	    }

        public string ToString(Statistics stats)
		{
			return $@"Code: {FormatWholeK(stats.CodeLines),5}  Test: {FormatWholeK(stats.TestCodeLines),5} Doc: {FormatWholeK(stats.DocumentationLines),5} Files: {FormatWholeK(stats.Files),5}";
		}
	}
}