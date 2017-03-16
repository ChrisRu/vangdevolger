using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VangDeVolger.Elements;
using VangDeVolger.Elements.Blocks;

namespace VangDeVolger.ImageReader
{
    internal class ImageReader
    {
        public Bitmap ImageBitmap;

        public ImageReader(Bitmap imageBitmap)
        {
            ImageBitmap = imageBitmap;
        }

        public Element[,] GetGrid()
        {
            Element[,] grid = new Element[ImageBitmap.Width, ImageBitmap.Height];

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
                        // Donker Wit
                        case "ff000000":
                            grid[x, y] = new BlockSolid(x, y);
                            break;
                    }
                }
            }
            return grid;
        }
    }
}
