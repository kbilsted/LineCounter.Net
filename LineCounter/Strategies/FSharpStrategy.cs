﻿using System.IO;

namespace TeamBinary.LineCounter
{
    class FSharpStrategy : IStrategy
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

                if ((l.Length == 13 && l == "/// <summary>") || (l.Length == 14 && l == "/// </summary>"))
                    continue;

                if (l.StartsWith("/// "))
                    res.DocumentationLines++;

                if (l.StartsWith("//"))
                    continue;

                res.CodeLines++;
            }

            return res;
        }
    }
}