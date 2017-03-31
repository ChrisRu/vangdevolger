
namespace VangDeVolger.Elements.Birds
{
    using System.Drawing;

    /// <summary>
    /// Bird (Abstract) Class to sit inside Spot
    /// </summary>
    public abstract class Bird : Element
    {
        protected Image ImageLeft { get; set; }

        protected Image ImageRight { get; set; }

        protected bool GoingRight { get; set; }

        /// <summary>
        /// Initialize new Bird Class
        /// </summary>
        protected Bird()
        {
            this.GoingRight = true;
        }

        /// <summary>
        /// Change facing direction of image
        /// </summary>
        /// <param name="direction">Facing Direction</param>
        public void ChangeDirection(Direction direction)
        {
            if (direction == Direction.Left && this.Pb.Image == this.ImageRight)
            {
                this.Pb.Image = this.ImageLeft;
                this.Pb.Invalidate();
            }
            else if (direction == Direction.Right && this.Pb.Image == this.ImageLeft)
            {
                this.Pb.Image = ImageRight;
                this.Pb.Invalidate();
            }
        }
    }
}