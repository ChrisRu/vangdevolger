using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VangDeVolger.Blocks;

namespace VangDeVolger.Birds
{
    internal class PlayerBird : Bird
    {
        private readonly Image _image = VangDeVolger.Properties.Resources.vogel_groen;
        private readonly Image _imageActive = VangDeVolger.Properties.Resources.vogel_groen_active;

        private string facing_direction = "left";
        private bool going_right = false;
        private bool going_left = true;

        private Point _previousPosition;

        /// <summary>
        /// Initialize PlayerBird Class
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        public PlayerBird(Point position, int speed) : base(position, speed)
        {
            this.Pb.Image = _image;
        }

        /// <summary>
        /// Move Bird
        /// </summary>
        /// <param name="blocks"></param>
        /// <param name="e"></param>
        internal override void Move(ref List<Block> blocks, KeyEventArgs e)
        {
            Size direction;

            // Collision checking
            if (blocks.Any(block => this.Pb.Bounds.IntersectsWith(block.Pb.Bounds)))
            {
                this.Pb.Location = _previousPosition;
                return;
            }

            if (e.KeyCode.Equals(Keys.Down))
            {
                direction = new Size(0, PlayerSpeed);
            }
            else if (e.KeyCode.Equals(Keys.Up))
            {
                direction = new Size(0, -PlayerSpeed);
            }
            else if (e.KeyCode.Equals(Keys.Left))
            {
                going_left = true;
                going_right = false;
                direction = new Size(-PlayerSpeed, 0);
            }
            else if (e.KeyCode.Equals(Keys.Right))
            {
                going_left = false;
                going_right = true;
                direction = new Size(PlayerSpeed, 0);
            }
            else
            {
                direction = new Size(0, 0);
            }

            if(going_left && !facing_direction.Equals("left"))
            {
                this.Pb.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                facing_direction = "left";
                this.Pb.Invalidate();
            }
            if (going_right && !facing_direction.Equals("right"))
            {
                this.Pb.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                facing_direction = "right";
                this.Pb.Invalidate();
            }

            _previousPosition = this.Pb.Location;
            this.Pb.Location = Point.Add(this.Pb.Location, direction);
        }
    }
}
