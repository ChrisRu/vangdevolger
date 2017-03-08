﻿using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VangDeVolger.Blocks;

namespace VangDeVolger.Birds
{
    internal class PlayerBird : Bird
    {
        private readonly Image _imageLeft = Properties.Resources.bird_green_left;
        private readonly Image _imageRight = Properties.Resources.bird_green_right;

        private string _facingDirection = "left";
        private bool _goingRight;

        /// <summary>
        /// Initialize PlayerBird Class
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        public PlayerBird(Point position, int speed) : base(position, speed)
        {
            Pb.Image = _imageLeft;
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
                    _goingRight = true;
                    break;
                default:
                    direction = new Size(0, 0);
                    break;

            }

            if (!_goingRight && !_facingDirection.Equals("left"))
            {
                Pb.Image = _imageLeft;
                _facingDirection = "left";
                Pb.Invalidate();
            }
            if (_goingRight && !_facingDirection.Equals("right"))
            {
                Pb.Image = _imageRight;
                _facingDirection = "right";
                Pb.Invalidate();
            }

            var tempPb = new Rectangle
            {
                Location = Point.Add(Pb.Location, direction),
                Size = Pb.Size
            };

            var canMove = true;

            // Collision checking
            foreach (var block in blocks)
            {
                if (!tempPb.IntersectsWith(block.Pb.Bounds)) continue;

                if (block.GetType().Name == "BlockMovable")
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Down:
                            canMove = block.Touch(Game.Directions.Down);
                            break;
                        case Keys.Up:
                            canMove = block.Touch(Game.Directions.Up);
                            break;
                        case Keys.Right:
                            canMove = block.Touch(Game.Directions.Right);
                            break;
                        case Keys.Left:
                            canMove = block.Touch(Game.Directions.Left);
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

            if (canMove)
            {
                Pb.Location = tempPb.Location;
            }
            
        }
    }
}
