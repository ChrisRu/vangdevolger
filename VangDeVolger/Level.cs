using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VangDeVolger.Elements;
using VangDeVolger.Elements.Birds;
using VangDeVolger.Elements.Blocks;

namespace VangDeVolger
{
    public class Level
    {
        public const int Scale = 32;
        public Control.ControlCollection Controls;
        public static Element[,] Grid;

        public Element Player
        {
            get
            {
                Coordinates playerCoordinates = new Coordinates();

                foreach (Element element in Grid)
                {
                    if (element is Player)
                    {
                        playerCoordinates = new Coordinates(element.X, element.Y);
                    }
                }

                return Grid[playerCoordinates.X, playerCoordinates.Y];
            }
        }

        public Element Enemy
        {
            get
            {
                Coordinates enemyCoordinates = new Coordinates();

                foreach (Element element in Grid)
                {
                    if (element is Enemy)
                    {
                        enemyCoordinates = new Coordinates(element.X, element.Y);
                    }
                }

                return Grid[enemyCoordinates.X, enemyCoordinates.Y];
            }
        }

        /// <summary>
        /// Initialize Level Class
        /// </summary>
        /// <param name="controls">Controls of the Form</param>
        /// <param name="width">Width of the Form</param>
        /// <param name="height">Height of the Form</param>
        public Level(Control.ControlCollection controls, int width, int height)
        {
            Controls = controls;
            width = (width - width % Scale) / Scale;
            height = (height - height % Scale) / Scale - 1;

            Grid = GetRandomGrid(width, height);

            // Create Player
            Grid[0, 0] = new Player(0, 0);

            // Create Enemy
            Grid[width - 1, height - 1] = new Enemy(width - 1, height - 1);

            Render();
        }

        /// <summary>
        /// Returns a random list of blocks
        /// </summary>
        /// <param name="sizeX">Grid size horizontally</param>
        /// <param name="sizeY">Grid size vertically</param>
        /// <returns>Jagged Array of Elements</returns>
        public Element[,] GetRandomGrid(int sizeX, int sizeY)
        {
            Element[,] blocks = new Element[sizeX, sizeY];
            Random random = new Random();

            for (var y = 0; y < sizeY; y++)
            {
                for (var x = 0; x < sizeX; x++)
                {
                    int chance = random.Next(100);

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

        /// <summary>
        /// Get Random Open Position in the Grid
        /// </summary>
        /// <returns>Random Open Coordinates</returns>
        public Coordinates GetRandomOpenPosition()
        {
            Random random = new Random();
            while (true)
            {
                int randomX = random.Next(0, Grid.GetLength(0));
                int randomY = random.Next(0, Grid.GetLength(1));

                if (Grid[randomX, randomY] == null)
                {
                    return new Coordinates(randomX, randomY);
                }
            }
        }

        /// <summary>
        /// Execute on KeyDown
        /// </summary>
        /// <param name="e">KeyData</param>
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
                Player.Move((Direction) direction);
            }
        }

        /// <summary>
        /// Move Block from position to new position
        /// </summary>
        /// <param name="x">Initial X Position</param>
        /// <param name="y">Initial Y Position</param>
        /// <param name="newX">New X Position</param>
        /// <param name="newY">New Y Position</param>
        public static void MoveBlock(int x, int y, int newX, int newY)
        {
            Grid[newX, newY] = Grid[x, y];
            Grid[x, y] = null;
            Grid[newX, newY].Pb.Location = new Point(newX * Scale, newY * Scale);
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