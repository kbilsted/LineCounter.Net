using System.IO;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
			var files = (Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly))
				.Select(x => new {Filename = Path.GetFileName(x), x})
				.Where(x => !x.Filename.StartsWith("jquery", StringComparison.OrdinalIgnoreCase))
				.Where(x => !x.Filename.StartsWith("bootstrap", StringComparison.OrdinalIgnoreCase))
				.Where(x => !x.Filename.StartsWith("angular", StringComparison.OrdinalIgnoreCase))
				.Where(x => !x.Filename.StartsWith("knockout", StringComparison.OrdinalIgnoreCase))
				.Where(x => !x.Filename.StartsWith("modernizr", StringComparison.OrdinalIgnoreCase))

				.Select(x => x.x);

			var dirs = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly)
				.Where(x => !(x.EndsWith(@"\.hg", StringComparison.Ordinal)
							  || x.EndsWith(@"\.git", StringComparison.Ordinal)
							  || x.EndsWith(@"\node_modules", StringComparison.Ordinal)
							  || x.EndsWith(@"\jspm_packages", StringComparison.Ordinal)
							  || x.EndsWith(@"\packages", StringComparison.Ordinal)


							  || x.EndsWith(@"\obj\Debug", StringComparison.Ordinal)
							  || x.EndsWith(@"\obj\ReSharper", StringComparison.Ordinal)));

			return files.Concat(dirs.SelectMany(GetFiles));
		}

		readonly CStrategy c = new CStrategy();
		readonly CSharpStrategy cs = new CSharpStrategy();
		readonly SqlStrategy sql = new SqlStrategy();
		readonly UnknownFileTypeStragegy unknown = new UnknownFileTypeStragegy();
		readonly JSStrategy js = new JSStrategy();

		public virtual IStrategy GetStrategy(string path)
		{
			//Console.WriteLine("path: " + path);
			var ext = Path.GetExtension(path).ToLowerInvariant();
			if (ext == ".cs")
				return cs;
			if (ext == ".cshtml")
				return new CsHtmlStrategy();
			if (ext == ".csproj")
				return new CsprojStrategy();
			if (ext == ".sln")
				return new SlnStrategy();
			if (ext == ".fs")
				return new FSharpStrategy();
			if (ext == ".md")
				return new MarkDownStrategy();
			if (ext == ".c" || ext == ".h" || ext == ".cpp")
				return c;
			if (ext == ".js")
				return js;
			if (ext == ".ts")
				return new TSStrategy();
			if (ext == ".prc" || ext == ".pkb" || ext == ".fnc")
			{
				//Console.WriteLine(path);
				return sql;
			}

			//Console.WriteLine(path);
			//Console.ReadKey();
			return unknown;
		}

		/// <summary>
		/// Looks for magic marker <!--start-->....<!--end--> and replace the body of that with the shield content
		/// </summary>
		public void ReplaceWebshieldsInFile(string codeFolderPath, string pathOfFileToMutate)
		{
			var stats = new LineCounting().CountFolder(codeFolderPath);

			var shieldsRegEx = new Regex("<!--start-->.*<!--end-->", RegexOptions.Singleline);
			var githubShields = new WebFormatter().CreateGithubShields(stats.Total);

			var oldReadme = File.ReadAllText(pathOfFileToMutate);
			var newReadMe = shieldsRegEx.Replace(oldReadme, $"<!--start-->\r\n{githubShields}\r\n<!--end-->");

			if (oldReadme != newReadMe)
				File.WriteAllText(pathOfFileToMutate, newReadMe);
		}
	}
}