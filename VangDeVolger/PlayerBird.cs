using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VangDeVolger
{
    internal class PlayerBird : Bird
    {
        private readonly Image _image = VangDeVolger.Properties.Resources.vogel_groen;
        private readonly Image _imageActive = VangDeVolger.Properties.Resources.vogel_groen_active;

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
        /// <param name="Blocks"></param>
        /// <param name="e"></param>
        internal override void Move(List<Block> Blocks, KeyEventArgs e)
        {
            Size direction;

            if (Blocks.Any(block => this.Pb.Bounds.IntersectsWith(block.Pb.Bounds)))
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
                direction = new Size(-PlayerSpeed, 0);
            }
            else if (e.KeyCode.Equals(Keys.Right))
            {
                direction = new Size(PlayerSpeed, 0);
            }
            else
            {
                direction = new Size(0, 0);
            }

            _previousPosition = this.Pb.Location;
            this.Pb.Location = Point.Add(this.Pb.Location, direction);
        }
    }
}
