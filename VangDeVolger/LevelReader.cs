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
                    Color pixelColor = ImageBitmap.GetPixel(x, y);

                    switch (pixelColor.Name)
                    {
                        // White
                        case "000000":
                            grid[x, y] = null;
                            break;
                        // Black
                        case "ff000000":
                            grid[x, y] = new Spot(new BlockSolid(), 32);
                            break;
                    }
                }
            }
            return grid;
        }
    }
}
