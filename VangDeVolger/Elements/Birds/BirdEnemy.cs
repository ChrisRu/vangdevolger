﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VangDeVolger.PathFinding;

namespace VangDeVolger.Elements.Birds
{
    internal class Enemy : Bird
    {
        public PathFinder PathFinder;
        public List<Coordinates> Path;

        /// <summary>
        /// Initialize EnemyBird Class
        /// </summary>
        /// <param name="x">Position X on grid</param>
        /// <param name="y">Position Y on grid</param>
        public Enemy(int x, int y) : base(x, y)
        {
            ImageLeft = Properties.Resources.bird_red_left;
            ImageRight = Properties.Resources.bird_red_right;
            Pb.Image = ImageLeft;
            GoingRight = false;

            PathFinder = new PathFinder(Level.Grid, new Coordinates(this.X, this.Y), new Coordinates(0, 0));
            Path = PathFinder.GetOptimalPath();

            var timer = new Timer
            {
                Interval = 500
            };
            timer.Tick += _moveAlongPath;
            timer.Start();
        }

        /// <summary>
        /// Move Bird with controls
        /// </summary>
        /// <param name="direction">Direction of movement</param>
        public override bool Move(Direction direction)
        {
            ChangeDirection(direction);

            return false;
        }

        public Coordinates GetPlayerLocation()
        {
            foreach (Element element in Level.Grid)
            {
                if (element is Player)
                {
                    return new Coordinates(element.X, element.Y);
                }
            }
            return new Coordinates(0, 0);
        }

        private void _moveAlongPath(object sender, EventArgs e)
        {
            if (Path.Count > 0)
            {
                if (X > Path[Path.Count - 1].X)
                {
                    ChangeDirection(Direction.Left);
                }
                else if (X < Path[Path.Count - 1].X)
                {
                    ChangeDirection(Direction.Right);
                }
                PathFinder = new PathFinder(Level.Grid, new Coordinates(X, Y), GetPlayerLocation());
                Path = PathFinder.GetOptimalPath();
                Path.Remove(Path[Path.Count - 1]);
                Level.MoveBlock(X, Y, Path[Path.Count - 1].X, Path[Path.Count - 1].Y);
            }
        }
    }
}
