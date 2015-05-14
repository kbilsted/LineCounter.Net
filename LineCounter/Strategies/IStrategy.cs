namespace TeamBinary.LineCounter
{
    interface IStrategy
    {
        Statistics Count(string path);
    }
}