using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace KbgSoft.LineCounter
{
    public class WebFormatter
    {

        public string CreateGithubShieldsForAllNonZeroStats(Stats stats)
        {
            var tagsList = new List<Tag>();
            
            foreach (var item in stats.FiletypeStat)
            {
                if (item.Value.TestCodeLines > 0)
                    tagsList.Add(new Tag(item.Key + " test code", FormatWholeK(item.Value.TestCodeLines)));
                if (item.Value.CodeLines > 0)
                    tagsList.Add(new Tag(item.Key + " code", FormatWholeK(item.Value.CodeLines)));
                if (item.Value.DocumentationLines > 0)
                    tagsList.Add(new Tag(item.Key + " doc", FormatWholeK(item.Value.DocumentationLines)));
            }

            return string.Join("\r\n", tagsList.Select(x=>x.DisplayText));
        }


        public string CreateGithubShields(Statistics stats)
        {
            var str = @"[![Stats](https://img.shields.io/badge/Code_lines-{0}-ff69b4.svg)]()
[![Stats](https://img.shields.io/badge/Test_lines-{1}-69ffb4.svg)]()
[![Stats](https://img.shields.io/badge/Doc_lines-{2}-ffb469.svg)]()";
            var res = string.Format(str, Format(stats.CodeLines), Format(stats.TestCodeLines), Format(stats.DocumentationLines));

            return res;
        }

        private string Format(int count)
        {
            if (count < 1000)
                return "" + count;
            return string.Format("{0:0.0}_K", count / 1000M);
        }

        private string FormatWholeK(int count)
        {
            return count < 1000 ? $"{count}" : $"{count / 1000M:0}K";
        }

        public string PrintOnlyCode(Statistics stats)
        {
            return $@"{(stats.CodeLines.ToString("N0")),7}";
        }

        public string ToString(Statistics stats)
        {
            return $@"Code: {FormatWholeK(stats.CodeLines),5}  Test: {FormatWholeK(stats.TestCodeLines),5} Doc: {FormatWholeK(stats.DocumentationLines),5} Files: {FormatWholeK(stats.Files),5}";
        }
    }
}

public class Tag
{
    const int MaxValue_ffffff = 16777215;
    public readonly string DisplayText;

    public readonly string HexCodeForValue;

    public readonly string Value;

    public Tag(string value, string count)
    {
        Value = value;
        var hashCode = Math.Abs(GetDeterministicHashCode(value));
        DisplayText = $@"[![Stats](https://img.shields.io/badge/{value.Replace(' ','_')}-{count}-{hashCode}.svg)]()";
        var color = hashCode % MaxValue_ffffff;
        HexCodeForValue = color.ToString("x").PadRight(6, '0');
    }

    public override string ToString()
    {
        return Value;
    }

    int GetDeterministicHashCode(string str)
    {
        unchecked
        {
            int hash1 = (5381 << 16) + 5381;
            int hash2 = hash1;

            for (int i = 0; i < str.Length; i += 2)
            {
                hash1 = ((hash1 << 5) + hash1) ^ str[i];
                if (i == str.Length - 1)
                    break;
                hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
            }

            return hash1 + (hash2 * 1566083941);
        }
    }
}