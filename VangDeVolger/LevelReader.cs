
namespace VangDeVolger
{
    using System;
    using System.Drawing;

    using VangDeVolger.Elements;
    using VangDeVolger.Elements.Blocks;

    public class LevelReader
    {
        public Bitmap ImageBitmap;

        private readonly Random _random;
        
        /// <summary>
        /// Initialize new LevelReader Class
        /// </summary>
        /// <param name="image">Image to be parsed as grid</param>
        public LevelReader(Image image)
        {
            this.ImageBitmap = new Bitmap(image);
            this._random = new Random();
        }

        /// <summary>
        /// Get Grid From image
        /// </summary>
        /// <returns>Grid with Spots from image</returns>
        public Spot[,] GetGrid()
        {
            Spot[,] grid = new Spot[this.ImageBitmap.Width, this.ImageBitmap.Height];

            for (int y = 0; y < this.ImageBitmap.Height; y++)
            {
                for (int x = 0; x < this.ImageBitmap.Width; x++)
                {
                    grid[x, y] = this._colorToSpot(this.ImageBitmap.GetPixel(x, y));
                }
            }

            return grid;
        }

        /// <summary>
        /// Convert color to Spot
        /// </summary>
        /// <param name="color">Color to be parsed</param>
        /// <returns>Spot with empty or random element</returns>
        private Spot _colorToSpot(Color color)
        {
            switch (color.Name)
            {
                // White
                case "ffffffff":
                    return new Spot(null, 32);
                // Other colors
                default:
                    return new Spot(this._getRandomElement(), 32);
            }
        }

        /// <summary>
        /// Get Random Movable or Solid Block
        /// </summary>
        /// <returns>Random Element</returns>
        private Element _getRandomElement()
        {
            int randomNumber = this._random.Next(2);
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
