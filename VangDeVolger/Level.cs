﻿using System;
using System.Drawing;
using System.Windows.Forms;
using VangDeVolger.Elements;
using VangDeVolger.Elements.Birds;
using VangDeVolger.Elements.Blocks;

namespace VangDeVolger
{
    public class Level
    {
        public static bool Paused;
        public const int Scale = 32;
        public Control.ControlCollection Controls;
        public Spot[,] Grid { get; set; }

        public Spot Player
        {
            get
            {
                foreach (Spot spot in Grid)
                {
                    if (spot.Element is Player)
                    {
                        return spot;
                    }
                }
                return null;
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
            //Grid = new ImageReader.ImageReader(Properties.Resources.maze).GetGrid();

            // Create Player
            Grid[0, 0] = new Spot(ElementType.Player);

            // Create Enemy
            Grid[width - 1, height - 1] = new Spot(ElementType.Enemy);

            Render();
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
                        spots[x, y] = new Spot(ElementType.Solid);
                    }
                    else if (randomNumber < 25)
                    {
                        spots[x, y] = new Spot(ElementType.Movable);
                    }
                    else
                    {
                        spots[x, y] = new Spot(ElementType.None);
                    }
                }
            }

            return spots;
        }

        /// <summary>
        /// Execute on KeyDown
        /// </summary>
        /// <param name="e">KeyData</param>
        public void KeyDown(KeyEventArgs e)
        {
            if (Paused) return;

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
                Player.Element.Move((Direction) direction);
            }
        }

        /// <summary>
        /// Add all elements to the Controls
        /// </summary>
        public void Render()
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
                        spot.Element.Pb.Location = new Point(x * Scale, y * Scale);
                        Controls.Add(spot.Element.Pb);
                    }
                }
            }
        }
    }
}