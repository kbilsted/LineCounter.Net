using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamBinary.LineCounter
{
    class UnknownFileTypeStragegy : IStrategy
    {
        public Statistics Count(string path)
        {
            return new Statistics();
        }
    }
}
