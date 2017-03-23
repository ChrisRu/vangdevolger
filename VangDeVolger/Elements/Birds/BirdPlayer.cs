namespace VangDeVolger.Elements.Birds
{
    public class Player : Bird
    {
        /// <summary>
        /// Initialize PlayerBird Class
        /// </summary>
        public Player(Spot parent) : base(parent)
        {
            ImageLeft = Properties.Resources.bird_green_left;
            ImageRight = Properties.Resources.bird_green_right;
            Pb.Image = ImageRight;
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
            if (nextSpot.Element == null)
            {
                Parent.MoveElement(direction);
                return true;
            }
            if (nextSpot.Element.Move(direction))
            {
                Parent.MoveElement(direction);
                return true;
            }
            return false;
        }
    }
}
