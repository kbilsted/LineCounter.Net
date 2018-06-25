using System.IO;
using System.Linq;
using KbgSoft.LineCounter.Strategies;

namespace KbgSoft.LineCounter {
	public class LineCounting {
		public Stats CountFiles(string[] paths) {
			var stat = new Stats();

			foreach (var file in paths) {
				var strategy = GetStrategy(file);
				var res = strategy.Count(file);

				stat.Add(strategy.StatisticsKey, res);
			}

			return stat;
		}

		public Stats CountFolder(string path) {
			var files = GetFiles(path);

			//Console.WriteLine("filescount: " + files.Count());

			return CountFiles(files);
		}

		// TODO replace with recursive visitor to avoid load on file system since we then can skip visiting deep subfolders
		public virtual string[] GetFiles(string path) {
			var allFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
			var files = allFiles.Where(x => !(
				x.Contains(@"\.hg\")
				|| x.Contains(@"\.git\")
				|| x.Contains(@"\obj\Debug\")
				|| x.Contains(@"\obj\ReSharper\")));
			return files.ToArray();
		}

		public virtual IStrategy GetStrategy(string path) {
			var folderIsJsModuleFolder = path.Contains(@"\node_modules\");

			if (folderIsJsModuleFolder)
				return new UnknownFileTypeStragegy();

			//Console.WriteLine("path: " + path);
			var ext = Path.GetExtension(path);
			if (ext == ".cs")
				return new CSharpStrategy();
			if (ext == ".cshtml")
				return new CsHtmlStrategy();
			if (ext == ".fs")
				return new FSharpStrategy();
			if (ext == ".md")
				return new MarkDownStrategy();
			if (ext == ".c"|| ext == ".h")
				return new CStrategy();
			if (ext == ".js")
				return new JSStrategy();
			if (ext == ".ts")
				return new TSStrategy();

			return new UnknownFileTypeStragegy();
		}
	}
}