using System.IO;

namespace TeamBinary.LineCounter
{
    class MarkDownStrategy : IStrategy
    {
        const int lineWidth = 75;

        public Statistics Count(string path)
        {
            var res = new Statistics();
            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                var l = line.Trim();
                if (string.IsNullOrWhiteSpace(l))
                    continue;

                res.DocumentationLines += l.Length / lineWidth;
                res.DocumentationLines += l.Length % lineWidth == 0 ? 0 : 1;
            }

            return res;
        }
    }
}