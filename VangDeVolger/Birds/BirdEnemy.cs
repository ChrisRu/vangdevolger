using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VangDeVolger.Blocks;

namespace VangDeVolger.Birds
{
    internal class EnemyBird : Bird
    {
        private readonly Image _imageLeft = Properties.Resources.bird_red_left;
        private readonly Image _imageRight = Properties.Resources.bird_red_right;

        /// <summary>
        /// Initialize EnemyBird Class
        /// </summary>
        /// <param name="position"></param>
        public EnemyBird(Point position) : base(position)
        {
            Pb.Image = _imageLeft;
        }

        /// <summary>
        /// Move Bird with controls
        /// </summary>
        /// <param name="e"></param>
        public override void Move(KeyEventArgs e)
        {
            Size direction = new Size(0, 0);

            if (!Game._player.Pb.Bounds.IntersectsWith(Pb.Bounds))
            {
                foreach (var block in Game.Blocks.ToList())
                {
                    if (!Pb.Bounds.IntersectsWith(block.Pb.Bounds))
                    {

                        if (Game._player.Pb.Location.X < Pb.Location.X)
                        {
                            direction = new Size(-Game.EnemySpeed, 0);
                            GoingRight = false;
                        }
                        if (Game._player.Pb.Location.X > Pb.Location.X)
                        {
                            direction = new Size(+Game.EnemySpeed, 0);
                            GoingRight = true;
                        }
                        if (Game._player.Pb.Location.Y < Pb.Location.Y)
                        {
                            direction = new Size(0, -Game.EnemySpeed);
                        }
                        if (Game._player.Pb.Location.Y > Pb.Location.Y)
                        {
                            direction = new Size(0, +Game.EnemySpeed);
                        }
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                // game over
                MessageBox.Show("You suck dick!");
            }

            var tempPb = new Rectangle
            {
                Location = Point.Add(Pb.Location, direction),
                Size = Pb.Size
            };

            // Collision checking
            foreach (var block in Game.Blocks.ToList())
            {
                if (tempPb.IntersectsWith(block.Pb.Bounds))
                {
                    tempPb.Location = Pb.Location;
                }

            }

            if (tempPb.Location.X < 0 || tempPb.Location.X > (Game.WindowWidth - (Pb.Width * 1.4))) return;
            if (tempPb.Location.Y < 0 || tempPb.Location.Y > Game.WindowHeight - (Pb.Height * 2.3)) return;

            Pb.Location = tempPb.Location;

            ChangeDirection(_imageLeft, _imageRight);
        }
    }
}
