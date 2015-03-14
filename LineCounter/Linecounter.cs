using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TeamBinary.LineCounter
{
    public class DirWalker
    {
        public Statistics DoWork(string path)
        {
            var stat = new Statistics();

            // TODO replace with recursive visitor to avoid load on file system
            var allFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            var files = allFiles.Where(x => !(
                x.Contains(@"\.hg\") 
                || x.Contains(@"\.git\") 
                || x.Contains(@"\obj\Debug\")
                || x.Contains(@"\obj\ReSharper\")));
            
            foreach(var file in files)
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
            if (ext == ".md")
                return new MarkDownStrategy();

            return new UnknownFileTypeStragegy();
        }
    }

    public class Statistics
    {
        public int CodeLines, DocumentationLines;
    }

    public class WebFormatter
    {
        public string CreateGithubShields(Statistics stats)
        {
            var str = @"[![Stats](https://img.shields.io/badge/Code_lines-{0}-ff69b4.svg)]()
[![Stats](https://img.shields.io/badge/Doc_lines-{1}-ff69b4.svg)]()";
            var res = string.Format(str, Format(stats.CodeLines), Format(stats.DocumentationLines));

            return res;
        }

        string Format(int count)
        {
            if (count < 1000)
                return "" + count;
            return string.Format("{0:0.0}_K", count/1000M);
        }
    }

}
