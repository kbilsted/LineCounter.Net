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
            var stat = new Statistics();

            // TODO replace with recursive visitor to avoid load on file system since we then can skip visiting deep subfolders
            var allFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            var files = allFiles.Where(x => !(
                x.Contains(@"\.hg\")
                || x.Contains(@"\.git\")
                || x.Contains(@"\obj\Debug\")
                || x.Contains(@"\obj\ReSharper\")));

            foreach (var file in files)
            {
                var strategy = GetStrategy(file);
                var res = strategy.Count(file);
                stat.CodeLines += res.CodeLines;
                stat.DocumentationLines += res.DocumentationLines;
            }

            return stat;
        }

        IStrategy GetStrategy(string path)
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
