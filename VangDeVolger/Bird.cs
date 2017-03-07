using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger
{
    public abstract class Bird
    {
        private const int PlayerSpeed = 3;
        public PictureBox Pb;

        protected Bird(Point position)
        {
            this.Pb = new PictureBox
            {
                Location = position,
                Size = new Size(30, 30),
                SizeMode = PictureBoxSizeMode.Zoom
            };
        }

        /// <summary>
        /// Move Bird
        /// </summary>
        /// <param name="e"></param>
        public void Move(KeyEventArgs e)
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