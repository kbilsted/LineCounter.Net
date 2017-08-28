using System.IO;

namespace TeamBinary.LineCounter
{
    class CsHtmlStrategy : IStrategy
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

                if (l.Length == 1 && (string.Compare(l, "{", StringComparison.Ordinal) == 0 || string.Compare(l, "}", StringComparison.Ordinal)==0 || string.Compare(l, ";", StringComparison.Ordinal)==0))
                    continue;

                if (l.StartsWith("//"))
                    continue;

                res.CodeLines++;
            }

            return res;
        }
    }
}