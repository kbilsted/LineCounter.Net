using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KbgSoft.LineCounter {
	public class FileGetter {
		public IEnumerable<string> GetFile(string path, Func<string, bool> filter) {
			var d = new DirectoryInfo(path);

			var files = d.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly).Select(x => x.FullName);
			foreach (var file in files)
				if (filter(file))
					yield return file;

			var dirs = d.EnumerateDirectories("*.*", SearchOption.TopDirectoryOnly).Select(x => x.FullName).ToArray();
			foreach (var dir in dirs)
				if (filter(dir))
					foreach (var xx in GetFile(dir, filter))
						yield return xx;
		}
	}
}