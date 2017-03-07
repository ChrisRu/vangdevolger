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
        private readonly Image _birdImage = VangDeVolger.Properties.Resources.vogel_groen;
        private readonly Image _birdImageActive = VangDeVolger.Properties.Resources.vogel_groen_active;

        public PlayerBird(Point position, int speed) : base(position, speed)
        {
            this.Pb.Image = _birdImage;
        }

        /// <summary>
        /// Move Bird
        /// </summary>
        /// <param name="e"></param>
        internal override void Move(KeyEventArgs e)
        {
            Size direction;

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

            this.Pb.Location = Point.Add(this.Pb.Location, direction);
        }
    }
}
