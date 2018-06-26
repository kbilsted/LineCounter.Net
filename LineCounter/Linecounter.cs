using System.IO;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using KbgSoft.LineCounter.Strategies;

namespace KbgSoft.LineCounter {
	public class LineCounting {
		public Stats CountFiles(IEnumerable<string> paths) {
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

		public IEnumerable<string> GetFiles(string path)
		{
			var files = (Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly));
			var dirs = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly)
				.Where(x => !(x.EndsWith(@"\.hg", StringComparison.Ordinal)
				              || x.EndsWith(@"\.git", StringComparison.Ordinal)
				              || x.EndsWith(@"\node_modules", StringComparison.Ordinal)
				              || x.EndsWith(@"\packages", StringComparison.Ordinal)
				              || x.EndsWith(@"\obj\Debug", StringComparison.Ordinal)
				              || x.EndsWith(@"\obj\ReSharper", StringComparison.Ordinal)));

			return files.Concat(dirs.SelectMany(GetFiles));
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