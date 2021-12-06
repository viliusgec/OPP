namespace Client.Strategy
{
    public class MineRight : Algorithm
    {
        // not working for now
        public int x;
        public int y;
        public int height;
        public int width;
        public MineRight(int x, int y, int height, int width)
        {
            this.x = x;
            this.y = y;
            this.height = height;
            this.width = width;
        }
        public int[] Behave(int x, int y, int height, int width)
        {
            int[] coords = { x, y };
            return coords;
        }
    }
}
