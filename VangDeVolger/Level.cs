using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using VangDeVolger.Elements;
using VangDeVolger.Elements.Birds;
using VangDeVolger.Elements.Blocks;

namespace VangDeVolger
{
    public class Level
    {
        public int Scale { get; set; }
        public int Size { get; set; }

        private bool _paused;
        public bool Paused
        {
            get { return _paused; }
            set {
                _paused = value;
                if (value)
                {
                    ((Enemy) Enemy).MoveTimer.Stop();
                }
                else
                {
                    ((Enemy) Enemy).MoveTimer.Start();
                }
            }
        }

        private Control.ControlCollection Controls { get; }
        public Spot[,] Grid { get; set; }

        public Dictionary<int, Type> RandomElements { get; }

        public Element Player => (from Spot spot in Grid where spot.Element is Player select spot.Element).FirstOrDefault();
        public Element Enemy => (from Spot spot in Grid where spot.Element is Enemy select spot.Element).FirstOrDefault();

        /// <summary>
        /// Initialize Level Class
        /// </summary>
        /// <param name="controls">Controls of the Form</param>
        /// <param name="size">Width of the Form</param>
        /// <param name="scale">Height of the Form</param>
        /// <param name="offsetTop">Top Offset for Level</param>
        public Level(Control.ControlCollection controls, int size, int scale, int offsetTop)
        {
            Controls = controls;
            Size = size;
            Scale = scale;
            RandomElements = new Dictionary<int, Type>
            {
                {5, typeof(BlockSolid)},
                {20, typeof(BlockMovable)},
            };

            Grid = GetRandomGrid(size, size);
            //Grid = new LevelReader(Properties.Resources.maze).GetGrid();

            // Add Birds and allow them to move
            Grid[0, 0] = new Spot(new Player(), Scale);
            Grid[1, 0] = new Spot(null, Scale);
            Grid[0, 1] = new Spot(null, Scale);

            Grid[size - 1, size - 1] = new Spot(new Enemy(), Scale);
            Grid[size - 1, size - 2] = new Spot(null, Scale);
            Grid[size - 2, size - 1] = new Spot(null, Scale);

            Render(offsetTop);
        }

        /// <summary>
        /// Returns a random list of blocks
        /// </summary>
        /// <param name="sizeX">Grid size horizontally</param>
        /// <param name="sizeY">Grid size vertically</param>
        /// <returns>Jagged Array of Elements</returns>
        public Spot[,] GetRandomGrid(int sizeX, int sizeY)
        {
            Spot[,] spots = new Spot[sizeX, sizeY];
            Random random = new Random();

            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    spots[x, y] = new Spot(GetRandomElement(random.Next(0, 100)), Scale);
                }
            }

            return spots;
        }

        /// <summary>
        /// Get Random Element
        /// </summary>
        /// <param name="randomPercentage">Random number as percentage</param>
        /// <returns></returns>
        public Element GetRandomElement(int randomPercentage)
        {
            int percentage = 0;
            foreach (KeyValuePair<int, Type> type in RandomElements)
            {
                percentage += type.Key;
                if (randomPercentage < percentage)
                {
                    return (Element)Activator.CreateInstance(type.Value);
                }
            }
            return null;
        }

        /// <summary>
        /// Add all Elements to Controls
        /// </summary>
        /// <param name="offsetTop">Offset on Top of View</param>
        public void Render(int offsetTop)
        {
            for (int y = 0; y < Grid.GetLength(0); y++)
            {
                for (int x = 0; x < Grid.GetLength(1); x++)
                {
                    Spot spot = Grid[x, y];

                    // Set neighbors
                    if (y > 0)
                    {
                        spot.Neighbors.Add(Direction.Up, Grid[x, y - 1]);
                    }
                    if (y < Grid.GetLength(0) - 1)
                    {
                        spot.Neighbors.Add(Direction.Down, Grid[x, y + 1]);
                    }
                    if (x > 0)
                    {
                        spot.Neighbors.Add(Direction.Left, Grid[x - 1, y]);
                    }
                    if (x < Grid.GetLength(1) - 1)
                    {
                        spot.Neighbors.Add(Direction.Right, Grid[x + 1, y]);
                    }

                    // Add elements to Controls
                    if (spot.Element != null)
                    {
                        spot.Element.Pb.Location = new Point(x * Scale, y * Scale + offsetTop);
                        Controls.Add(spot.Element.Pb);
                    }
                }
            }
        }
    }
}