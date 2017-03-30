using System;
using System.Drawing;
using VangDeVolger.Elements;
using VangDeVolger.Elements.Blocks;

namespace VangDeVolger
{
    internal class LevelReader
    {
        public Bitmap ImageBitmap;
        private readonly Random _random;

        public LevelReader(Image image)
        {
            ImageBitmap = new Bitmap(image);
            _random = new Random();
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
                case "ffffffff":
                    return new Spot(null, 32);
                // Other colors
                default:
                    return new Spot(_getRandomElement(), 32);
            }
        }

        private Element _getRandomElement()
        {
            int randomNumber = _random.Next(2);
            switch (randomNumber)
            {
                case 0:
                    return new BlockMovable();
                default:
                    return new BlockSolid();
            }
        }
    }
}
