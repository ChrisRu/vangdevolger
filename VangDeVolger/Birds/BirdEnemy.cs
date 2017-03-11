using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
            Size direction;

            var randomNumber = new Random().Next(1, 5);

            switch (randomNumber)
            {
                case 1:
                    direction = new Size(0, -Speed);
                    break;
                case 2:
                    direction = new Size(0, Speed);
                    break;
                case 3:
                    direction = new Size(-Speed, 0);
                    GoingRight = false;
                    break;
                case 4:
                    direction = new Size(Speed, 0);
                    GoingRight = true;
                    break;
                default:
                    direction = new Size(0, 0);
                    break;
            }

            var tempPb = new Rectangle
            {
                Location = Point.Add(Pb.Location, direction),
                Size = Pb.Size
            };

            // Collision checking
            foreach (var block in Game.Blocks)
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
