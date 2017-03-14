using System;
using System.Windows.Forms;
using VangDeVolger.Elements;
using VangDeVolger.Elements.Birds;
using VangDeVolger.Elements.Blocks;

namespace VangDeVolger
{
    public class Level
    {
        public static int Scaling;
        public Control.ControlCollection Controls;
        public Element[,] Grid;

        public Level(Control.ControlCollection controls, int width, int height, int scale)
        {
            Controls = controls;
            Scaling = scale;

            Grid = GetRandomGrid((width / scale), (height / scale));

            Grid[0, 0] = new Player(0, 0, Scaling);

            Render();
        }

        /// <summary>
        /// Returns a random list of blocks
        /// </summary>
        /// <param name="sizeX"></param>
        /// <param name="sizeY"></param>
        /// <returns></returns>
        public Element[,] GetRandomGrid(int sizeX, int sizeY)
        {
            var blocks = new Element[sizeX, sizeY];
            var random = new Random();

            for (var y = 0; y < sizeY; y++)
            {
                for (var x = 0; x < sizeX; x++)
                {
                    var chance = random.Next(100);

                    if (chance <= 5)
                    {
                        Block block = new BlockSolid(x, y, Scaling);
                        blocks[x, y] = block;
                    }
                    else if (chance <= 25)
                    {
                        Block block = new BlockMovable(x, y, Scaling);
                        blocks[x, y] = block;
                    }
                }
            }

            return blocks;
        }

        public Tuple<int, int> GetRandomOpenPosition()
        {
            var random = new Random();
            while (true)
            {
                var randomX = random.Next(0, Grid.GetLength(0));
                var randomY = random.Next(0, Grid.GetLength(1));

                if (Grid[randomX, randomY] == null)
                {
                    return new Tuple<int, int>(randomX, randomY);
                }
            }
        }

        public void KeyDown(KeyEventArgs e)
        {
            
        }

        /// <summary>
        /// Add all elements to the Controls
        /// </summary>
        public void Render()
        {
            foreach (var block in Grid)
            {
                if (block != null)
                {
                    Controls.Add(block.Pb);
                }
            }
        }
    }
}