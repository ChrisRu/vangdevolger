using System.Drawing;

namespace VangDeVolger.Elements.Birds
{
    public abstract class Bird : Element
    {
        protected Image ImageLeft { get; set; }
        protected Image ImageRight { get; set; }
        protected bool GoingRight { get; set; }

        /// <summary>
        /// Initialize Bird Class
        /// </summary>
        protected Bird()
        {
            GoingRight = true;
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

            if (GoingRight && Pb.Image == ImageLeft)
            {
                Pb.Image = ImageRight;
                Pb.Invalidate();
            }
            else if (!GoingRight && Pb.Image == ImageRight)
            {
                Pb.Image = ImageLeft;
                Pb.Invalidate();
            }
        }
    }
}