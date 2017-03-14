using System;
using System.Drawing;
using System.Linq.Expressions;
using System.Windows.Forms;
using VangDeVolger.Elements;
using VangDeVolger.Elements.Birds;
using VangDeVolger.Elements.Blocks;

namespace VangDeVolger
{
    public class Level
    {
        private readonly int _width;
        private readonly int _height;
        public static int Scaling;
        public Control.ControlCollection Controls;
        public Element[,] Grid;
        public Tuple<int, int> BirdLocation;

        public Level(Control.ControlCollection controls, int width, int height, int scale)
        {
            Controls = controls;
            _width = width;
            _height = height;
            Scaling = scale;

            Grid = GetRandomGrid((width / scale), (height / scale));

            // Create new Player
            BirdLocation = new Tuple<int, int>(0, 0);
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
            Direction? direction = null;
            switch (e.KeyCode)
            {
                case Keys.Up:
                    direction = Direction.Up;
                    break;
                case Keys.Down:
                    direction = Direction.Down;
                    break;
                case Keys.Left:
                    direction = Direction.Left;
                    break;
                case Keys.Right:
                    direction = Direction.Right;
                    break;
            }
            MoveBird(direction);

        }

        public void MoveBird(Direction? direction)
        {
            var x = BirdLocation.Item1;
            var y = BirdLocation.Item2;
            var newX = x;
            var newY = y;

            switch (direction)
            {
                case Direction.Up:
                    if (y > 0)
                    {
                        newY--;
                    }
                    break;
                case Direction.Down:
                    if (y < _height)
                    {
                        newY++;
                    }
                    break;
                case Direction.Left:
                    if (x > 0)
                    {
                        newX--;
                    }
                    break;
                case Direction.Right:
                    if (x < _width)
                    {
                        newX++;
                    }
                    break;
            }

            if (Grid[newX, newY] == null || newX != x && newY != y)
            {
                BirdLocation = new Tuple<int, int>(newX, newY);
                Grid[newX, newY] = Grid[x, y];
                Grid[x, y] = null;
                Grid[newX, newY].Pb.Location = new Point(newX * Scaling, newY * Scaling);
                Grid[newX, newY].X = newX;
                Grid[newX, newY].Y = newY;
            }
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