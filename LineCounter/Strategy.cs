using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TeamBinary.LineCounter
{

    interface IStrategy
    {
        Statistics Count(string path);
    }

    class UnknownFileTypeStragegy : IStrategy
    {
        public Statistics Count(string path)
        {
            return new Statistics();
        }
    }

    class CSharpStrategy : IStrategy
    {
        public Statistics Count(string path)
        {
            var res = new Statistics();
            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                var l = line.Trim();
                if (string.IsNullOrWhiteSpace(l))
                    continue;

                if (l == "{" || l == "}" || l == ";")
                    continue;

                if (l == "/// <summary>" || l == "/// </summary>")
                    continue;

                if (l.StartsWith("/// "))
                    res.DocumentationLines++;

                if (l.StartsWith("//") || l.StartsWith("////"))
                    continue;

                res.CodeLines++;
            }

            return res;
        }
    }

    class MarkDownStrategy : IStrategy
    {
        public Statistics Count(string path)
        {
            var res = new Statistics();
            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                var l = line.Trim();
                if (string.IsNullOrWhiteSpace(l))
                    continue;

                res.DocumentationLines++;
            }

            return res;
        }
    }
}
