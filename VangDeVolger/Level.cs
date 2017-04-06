
namespace VangDeVolger
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    using Elements;
    using Elements.Birds;
    using Elements.Blocks;

    /// <summary>
    /// Level Class to create the Game Level containing all elements
    /// </summary>
    public class Level
    {
        public int Scale { get; set; }

        public int Size { get; set; }

        private bool _paused;
        public bool Paused
        {
            get
            {
                return this._paused;
            }

            set
            {
                this._paused = value;
                this.Enemy.Paused = value;
            }
        }
        
        public Spot[,] Grid { get; set; }

        public Dictionary<Type, int> RandomElements { get; }

        public Enemy Enemy => (Enemy)(from Spot spot in Grid where spot.Element is Enemy select spot.Element).FirstOrDefault();

        private Control.ControlCollection Controls { get; }

        /// <summary>
        /// Initialize new Level Class
        /// </summary>
        /// <param name="controls">Controls of the Form</param>
        /// <param name="size">Width of the Form</param>
        /// <param name="scale">Height of the Form</param>
        /// <param name="offsetTop">Top Offset for Level</param>
        public Level(Control.ControlCollection controls, int size, int scale, int offsetTop)
        {
            this.Size = size;
            this.Scale = scale;
            this.RandomElements = new Dictionary<Type, int>
            {
                { typeof(BlockSnowFlake), 1 },
                { typeof(BlockGrass), 1 },
                { typeof(BlockSolid), 5 },
                { typeof(BlockMovable), 20 }
            };

            this.Grid = this.GetRandomGrid(size, size);

            ////this.Grid = new LevelReader(Properties.Resources.maze).GetGrid();

            // Add Birds and allow them to move
            this.Grid[0, 0] = new Spot(new Player(), this.Scale);
            this.Grid[1, 0] = new Spot(null, this.Scale);
            this.Grid[0, 1] = new Spot(null, this.Scale);

            this.Grid[size - 1, size - 1] = new Spot(new Enemy(), this.Scale);
            this.Grid[size - 1, size - 2] = new Spot(null, this.Scale);
            this.Grid[size - 2, size - 1] = new Spot(null, this.Scale);

            if (controls != null)
            {
                this.Controls = controls;
                this.Render(offsetTop);
            }
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
                    spots[x, y] = new Spot(this.GetRandomElement(random.Next(0, 100)), this.Scale);
                }
            }

            return spots;
        }

        /// <summary>
        /// Get Random Element
        /// </summary>
        /// <param name="randomPercentage">Random number as percentage</param>
        /// <returns>Random Element</returns>
        public Element GetRandomElement(int randomPercentage)
        {
            int percentage = 0;
            foreach (KeyValuePair<Type, int> type in this.RandomElements)
            {
                percentage += type.Value;
                if (randomPercentage < percentage)
                {
                    return (Element)Activator.CreateInstance(type.Key);
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
            for (int y = 0; y < this.Grid.GetLength(0); y++)
            {
                for (int x = 0; x < this.Grid.GetLength(1); x++)
                {
                    Spot spot = this.Grid[x, y];

                    // Set neighbors
                    if (y > 0)
                    {
                        spot.Neighbours.Add(Direction.Up, this.Grid[x, y - 1]);
                    }
                    if (y < this.Grid.GetLength(0) - 1)
                    {
                        spot.Neighbours.Add(Direction.Down, this.Grid[x, y + 1]);
                    }
                    if (x > 0)
                    {
                        spot.Neighbours.Add(Direction.Left, this.Grid[x - 1, y]);
                    }
                    if (x < this.Grid.GetLength(1) - 1)
                    {
                        spot.Neighbours.Add(Direction.Right, this.Grid[x + 1, y]);
                    }

                    // Add elements to Controls
                    if (spot.Element != null)
                    {
                        spot.Element.Pb.Location = new Point(x * this.Scale, (y * this.Scale) + offsetTop);
                        this.Controls?.Add(spot.Element.Pb);
                    }
                }
            }
        }
    }
}