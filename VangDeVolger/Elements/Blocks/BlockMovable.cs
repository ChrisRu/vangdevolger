namespace VangDeVolger.Elements.Blocks
{
    public class BlockMovable : Block
    {
        /// <summary>
        /// Initialize BlockMovable Class
        /// </summary>
        public BlockMovable(Spot parent) : base(parent)
        {
            Image = Properties.Resources.tree;
            Pb.Image = Image;
        }

        /// <summary>
        /// Execute on collision with other object
        /// </summary>
        /// <param name="direction">Direction of movement</param>
        /// <returns>bool Can move</returns>
        public override bool Move(Direction direction)
        {
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
