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
        /// <param name="x">Position X on grid</param>
        /// <param name="y">Position Y on grid</param>
        protected Bird(int x, int y) : base (x, y)
        {
            
        }

        /// <summary>
        /// Change facing direction of image
        /// </summary>
        /// <param name="direction">Facing Direction</param>
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