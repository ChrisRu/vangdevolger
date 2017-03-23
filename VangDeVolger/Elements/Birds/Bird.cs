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
        protected Bird(Spot parent) : base(parent)
        {
            this.GoingRight = true;
        }

        /// <summary>
        /// Change facing direction of image
        /// </summary>
        /// <param name="direction">Facing Direction</param>
        public void ChangeDirection(Direction direction)
        {
            if (direction == Direction.Left)
            {
                this.GoingRight = false;
            }
            if (direction == Direction.Right)
            {
                this.GoingRight = true;
            }
            this.Pb.Image = GoingRight ? ImageRight : ImageLeft;
            this.Pb.Invalidate();
        }
    }
}