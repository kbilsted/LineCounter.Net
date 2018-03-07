using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace TeamBinary.LineCounter
{
	public class DirWalker
	{
		public Statistics DoWork(string[] files)
		{
			var stat = new Statistics();
			foreach (var file in files)
			{
				var strategy = GetStrategy(file);
				var res = strategy.Count(file);
				stat.CodeLines += res.CodeLines;
				stat.DocumentationLines += res.DocumentationLines;
			}

			return stat;
		}

		public Statistics DoWork(string path)
		{
			var files = GetFiles(path);

			//Console.WriteLine("filescount: " + files.Count());

			return DoWork(files);
		}

		public virtual string[] GetFiles(string path)
		{
			var allFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
			var files = allFiles.Where(x => !(
				x.Contains(@"\.hg\")
				|| x.Contains(@"\.git\")
				|| x.Contains(@"\obj\Debug\")
				|| x.Contains(@"\obj\ReSharper\")));
			return files.ToArray();
		}

		public virtual IStrategy GetStrategy(string path)
		{
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

			return new UnknownFileTypeStragegy();
		}
	}
}
