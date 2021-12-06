namespace Client.Strategy
{
    public interface Algorithm
    {
        int[] Behave(int x, int y, int height, int width);
    }
}
