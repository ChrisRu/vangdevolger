using System.Drawing;

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
        protected Bird(int x, int y) : base (x, y)
        {
            
        }

        /// <summary>
        /// Change facing direction of image
        /// </summary>
        public void ChangeDirection(Direction direction)
        {
            if (direction == Direction.Left)
            {
                GoingRight = false;
            }
            if (direction == Direction.Right)
            {
                GoingRight = true;
            }
            Pb.Image = GoingRight ? ImageRight : ImageLeft;
            Pb.Invalidate();
        }
    }
}