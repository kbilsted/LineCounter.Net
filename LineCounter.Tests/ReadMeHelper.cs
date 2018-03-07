using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;
using TeamBinary.LineCounter;

namespace LineCounter.Tests
{
	public class ReadMeHelper
	{
		[Test]
		public void MutateReadme() {
			var pathReadme = @"C:\src\LineCounter.Net\README.md";
			var readme = File.ReadAllText(pathReadme);
			var stats = new DirWalker().DoWork(@"C:\src\LineCounter.Net\");

			var shieldsRegEx = new Regex("<!--start-->.*<!--end-->", RegexOptions.Singleline);
			var githubShields = new WebFormatter().CreateGithubShields(stats);
			var newReadMe = shieldsRegEx.Replace(readme, "<!--start-->"+githubShields+ "<!--end-->");

			if (readme!=newReadMe)
				File.WriteAllText(pathReadme, newReadMe);
		}
	}
}
