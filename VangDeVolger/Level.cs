using System;
using System.Drawing;
using System.Windows.Forms;
using VangDeVolger.Elements;
using VangDeVolger.Elements.Birds;
using VangDeVolger.Elements.Blocks;

namespace VangDeVolger
{
    public class Level
    {
        public static int Width;
        public static int Height;
        public static int Scaling;
        public Control.ControlCollection Controls;
        public static Element[,] Grid;
        public static Tuple<int, int> BirdLocation;
        public static Tuple<int, int> EnemyLocation;

        public Level(Control.ControlCollection controls, int width, int height, int scale)
        {
            Controls = controls;
            Width = (width - width % scale) / scale;
            Height = (height - height % scale) / scale - 1;
            Scaling = scale;

            Grid = GetRandomGrid(Width, Height);

            // Create new Player
            BirdLocation = new Tuple<int, int>(0, 0);
            Grid[0, 0] = new Player(0, 0);

            EnemyLocation = new Tuple<int, int>(Width - 1, Height - 1);
            Grid[Width - 1, Height - 1] = new Enemy(Width - 1, Height - 1);

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
                        Block block = new BlockSolid(x, y);
                        blocks[x, y] = block;
                    }
                    else if (chance <= 25)
                    {
                        Block block = new BlockMovable(x, y);
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
            if (direction != null)
            {
                Grid[BirdLocation.Item1, BirdLocation.Item2].Move((Direction) direction);
            }
        }

        public static Tuple<int, int> DirectionToLocation(int x, int y, Direction? direction)
        {
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
                    if (y < Height - 1)
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
                    if (x < Width - 1)
                    {
                        newX++;
                    }
                    break;
            }
            return new Tuple<int, int>(newX, newY);
        }

        public static void MoveBlock(int x, int y, int newX, int newY)
        {
            Grid[newX, newY] = Grid[x, y];
            Grid[x, y] = null;
            Grid[newX, newY].Pb.Location = new Point(newX * Scaling, newY * Scaling);
            Grid[newX, newY].X = newX;
            Grid[newX, newY].Y = newY;
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