﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VangDeVolger.Blocks;

namespace VangDeVolger.Birds
{
    internal class PlayerBird : Bird
    {
        private readonly Image _image = Properties.Resources.bird_green;

        private string _facingDirection = "left";
        private bool _goingRight;

        /// <summary>
        /// Initialize PlayerBird Class
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        public PlayerBird(Point position, int speed) : base(position, speed)
        {
            Pb.Image = _image;
        }

        /// <summary>
        /// Move Bird
        /// </summary>
        /// <param name="blocks"></param>
        /// <param name="e"></param>
        internal override void Move(ref List<Block> blocks, KeyEventArgs e)
        {
            Size direction;

            switch (e.KeyCode)
            {
                case Keys.Down:
                    direction = new Size(0, Speed);
                    break;
                case Keys.Up:
                    direction = new Size(0, -Speed);
                    break;
                case Keys.Left:
                    direction = new Size(-Speed, 0);
                    _goingRight = false;
                    break;
                case Keys.Right:
                    direction = new Size(Speed, 0);
                    break;
                default:
                    direction = new Size(0, 0);
                    break;

            }

            if (!_goingRight && !_facingDirection.Equals("left"))
            {
                Pb.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                _facingDirection = "left";
                Pb.Invalidate();
            }
            if (_goingRight && !_facingDirection.Equals("right"))
            {
                Pb.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                _facingDirection = "right";
                Pb.Invalidate();
            }

            var tempPb = new Rectangle
            {
                Location = Point.Add(Pb.Location, direction),
                Size = Pb.Size
            };

            // Collision checking
            foreach (var block in blocks)
            {
                if (!tempPb.IntersectsWith(block.Pb.Bounds)) continue;

                if (block.GetType().Name == "BlockMovable")
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Down:
                            block.Move(ref blocks, Game.Directions.Down);
                            break;
                        case Keys.Up:
                            block.Move(ref blocks, Game.Directions.Up);
                            break;
                        case Keys.Right:
                            block.Move(ref blocks, Game.Directions.Right);
                            break;
                        case Keys.Left:
                            block.Move(ref blocks, Game.Directions.Left);
                            break;
                    }
                }
                else
                {
                    tempPb.Location = Pb.Location;
                }
            }

            if (tempPb.Location.X < 0 || tempPb.Location.X > Game.WindowWidth) return;
            if (tempPb.Location.Y < 0 || tempPb.Location.Y > Game.WindowHeight) return;

            Pb.Location = tempPb.Location;
        }
    }
}
