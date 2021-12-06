namespace Client.Strategy
{
    public class MoveRight : Algorithm
    {
        public int x;
        public int y;
        public int height;
        public int width;
        public MoveRight(int x, int y, int height, int width)
        {
            this.x = x;
            this.y = y;
            this.height = height;
            this.width = width;
        }
        public int[] Behave(int x, int y, int height, int width)
        {
            int[] coords = { x, y };

            coords[0] = x + width;
            coords[1] = y;
            return coords;
        }
    }
}
