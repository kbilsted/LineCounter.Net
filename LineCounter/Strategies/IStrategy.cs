namespace TeamBinary.LineCounter
{
    public interface IStrategy
    {
        Statistics Count(string path);
    }
}