namespace AStar
{
    public class Tools
    {
        public static byte[,] CreateMatrix(int size)
        {
            var mMatrix = new byte[size, size];

            for (var y = 0; y < mMatrix.GetUpperBound(1); y++)
            {

                for (var x = 0; x < mMatrix.GetUpperBound(0); x++)
                {
                    mMatrix[x, y] = 1;
                }
            }

            return mMatrix;
        }
    }
}

