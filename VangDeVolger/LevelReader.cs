using System.Drawing;
using VangDeVolger.Elements.Blocks;

namespace VangDeVolger
{
    internal class LevelReader
    {
        public Bitmap ImageBitmap;

        public LevelReader(Image image)
        {
            ImageBitmap = new Bitmap(image);
        }

        public Spot[,] GetGrid()
        {
            Spot[,] grid = new Spot[ImageBitmap.Width, ImageBitmap.Height];

            for (int y = 0; y < ImageBitmap.Height; y++)
            {
                for (int x = 0; x < ImageBitmap.Width; x++)
                {
                    grid[x, y] = _colorToSpot(ImageBitmap.GetPixel(x, y));
                }
            }

            return grid;
        }

        private Spot _colorToSpot(Color color)
        {
            switch (color.Name)
            {
                // White
                case "000000":
                    return new Spot(null, 32);
                // Black
                case "ff000000":
                    return new Spot(new BlockSolid(), 32);
                // Other colors
                default:
                    return new Spot(null, 32);
            }
        }
    }
}
