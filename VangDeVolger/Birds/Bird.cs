using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger.Birds
{
    public abstract class Bird
    {
        protected Image ImageLeft;
        protected Image ImageRight;
        protected bool GoingRight = true;

        public PictureBox Pb;

        /// <summary>
        /// Initialize Bird Class
        /// </summary>
        /// <param name="position"></param>
        protected Bird(Point position)
        {
            Pb = new PictureBox
            {
                Location = position,
                Size = new Size(Level.Scaling, Level.Scaling),
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = ImageRight
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
        public void ChangeDirection()
        {
            Pb.Image = Pb.Image == ImageLeft ? ImageRight : ImageLeft;
            Pb.Invalidate();
        }
    }
}