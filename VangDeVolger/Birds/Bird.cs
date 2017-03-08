using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VangDeVolger.Blocks;

namespace VangDeVolger.Birds
{
    public abstract class Bird
    {
        public PictureBox Pb;

        public bool FacingRight = true;
        public bool GoingRight = true;

        protected int Speed;

        /// <summary>
        /// Initialize Bird Class
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        protected Bird(Point position, int speed)
        {
            this.Pb = new PictureBox
            {
                Location = position,
                Size = new Size(Game.BirdSize, Game.BirdSize),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            Speed = speed;
        }

        public abstract void Move(ref List<Block> blocks, KeyEventArgs e);

        public void ChangeDirection(Image imageLeft, Image imageRight)
        {
            if (!GoingRight && FacingRight)
            {
                Pb.Image = imageLeft;
                FacingRight = false;
                Pb.Invalidate();
            }
            else if (GoingRight && !FacingRight)
            {
                Pb.Image = imageRight;
                FacingRight = true;
                Pb.Invalidate();
            }
        }
    }
}