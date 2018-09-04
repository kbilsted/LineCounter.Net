using System;

namespace KbgSoft.LineCounter.Strategies
{
    internal class CsprojStrategy : IStrategy
    {
        public string StatisticsKey => "c# proj";

        public Statistics Count(string path)
        {
            return new Statistics() {};
        }
    }
}