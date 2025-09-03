namespace KbgSoft.LineCounter.Strategies;

public class ConfigStrategy : IStrategy {
    private static readonly TrimStringLens l = new TrimStringLens();

    public string StatisticsKey => "config";

    public Statistics Count(string path) {
        using (TextReader reader = File.OpenText(path))
        {
            return Count(new XmlMultiLineCommentFilterStream().ReadLines(reader));
        }
    }

    public Statistics Count(IEnumerable<string> lines) {
        var res = new Statistics()
        {
            Files = 1,
        };

        foreach (var line in lines) {
            l.SetValue(line);

            res.CodeLines++;
        }

        return res;
    }
}