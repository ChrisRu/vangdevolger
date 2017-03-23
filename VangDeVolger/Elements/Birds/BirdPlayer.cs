using System;
using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger.Elements.Birds
{
    internal class Player : Bird
    {
        /// <summary>
        /// Initialize PlayerBird Class
        /// </summary>
        public Player(Spot parent) : base(parent)
        {
            this.ImageLeft = Properties.Resources.bird_green_left;
            this.ImageRight = Properties.Resources.bird_green_right;
            this.Pb.Image = ImageRight;
        }

        /// <summary>
        /// Move Player towards direction
        /// </summary>
        /// <param name="direction">Direction of movement</param>
        /// <returns>bool Can move</returns>
        public override bool Move(Direction direction)
        {
            ChangeDirection(direction);

            if (!Parent.Neighbors.TryGetValue(direction, out Spot nextSpot))
            {
                return false;
            }
            else if (nextSpot.Element == null)
            {
                this.Parent.MoveElement(direction);
                return true;
            }
            else if (nextSpot.Element.Move(direction))
            {
                this.Parent.MoveElement(direction);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
