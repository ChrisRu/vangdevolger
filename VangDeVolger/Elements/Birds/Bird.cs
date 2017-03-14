using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger.Elements.Birds
{
    public abstract class Bird : Element
    {
        protected Image ImageLeft;
        protected Image ImageRight;
        protected bool GoingRight = true;

        /// <summary>
        /// Initialize Bird Class
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="scale"></param>
        protected Bird(int x, int y, int scale) : base (x, y, scale)
        {
            
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