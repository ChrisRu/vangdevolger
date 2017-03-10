using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VangDeVolger.Blocks;

namespace VangDeVolger.Birds
{
    public abstract class Bird
    {
        public PictureBox Pb;
        public const int BirdSize = 28;
        public int Speed;

        public bool FacingRight = true;
        public bool GoingRight = true;

        /// <summary>
        /// Initialize Bird Class
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        protected Bird(Point position)
        {
            Pb = new PictureBox
            {
                Location = position,
                Size = new Size(BirdSize, BirdSize),
                SizeMode = PictureBoxSizeMode.Zoom
            };
        }

        /// <summary>
        /// Move Bird with controls
        /// </summary>
        /// <param name="e"></param>
        public abstract void Move(KeyEventArgs e);

        /// <summary>
        /// Change facing direction of image
        /// </summary>
        /// <param name="imageLeft"></param>
        /// <param name="imageRight"></param>
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