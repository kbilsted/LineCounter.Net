namespace KbgSoft.LineCounter.Strategies
{
    internal class SlnStrategy : IStrategy
    {
        public string StatisticsKey => "c# sln";

        public Statistics Count(string path)
        {
            return new Statistics() {Files = 1};
        }
    }
}