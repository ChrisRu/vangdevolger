using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VangDeVolger.Elements;
using VangDeVolger.Elements.Birds;
using VangDeVolger.PathFinding;

namespace VangDeVolger
{
    public class Level
    {
        public int Scale { get; set; }
        public int Size { get; set; }
        public bool Paused { get; set; }
        public Control.ControlCollection Controls { get; set; }
        public Spot[,] Grid { get; set; }

        public Element Player =>
            (from Spot spot in Grid where spot.Element is Player select spot.Element).FirstOrDefault();
        public Element Enemy =>
            (from Spot spot in Grid where spot.Element is Enemy select spot.Element).FirstOrDefault();

        /// <summary>
        /// Initialize Level Class
        /// </summary>
        /// <param name="controls">Controls of the Form</param>
        /// <param name="size">Width of the Form</param>
        /// <param name="scale">Height of the Form</param>
        /// <param name="offsetTop"></param>
        public Level(Control.ControlCollection controls, int size, int scale, int offsetTop)
        {
            Controls = controls;
            Size = size;
            Scale = scale;
            Grid = GetRandomGrid(size, size);

            Grid[0, 0] = new Spot(ElementType.Player, Scale);
            Grid[size - 1, size - 1] = new Spot(ElementType.Enemy, Scale);

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
                    int randomNumber = random.Next(0, 100);

                    if (randomNumber < 5)
                    {
                        spots[x, y] = new Spot(ElementType.Solid, Scale);
                    }
                    else if (randomNumber < 25)
                    {
                        spots[x, y] = new Spot(ElementType.Movable, Scale);
                    }
                    else
                    {
                        spots[x, y] = new Spot(ElementType.None, Scale);
                    }
                }
            }

            return spots;
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