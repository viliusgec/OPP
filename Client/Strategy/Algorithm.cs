namespace Client.Strategy
{
    public interface IAlgorithm
    {
        int[] Behave(int x, int y, int height, int width);
    }
}
