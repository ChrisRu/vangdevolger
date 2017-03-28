namespace VangDeVolger.Elements.Blocks
{
    public class BlockMovable : Block
    {
        /// <summary>
        /// Initialize BlockMovable class
        /// </summary>
        public BlockMovable()
        {
            Image = Properties.Resources.tree;
            Pb.Image = Image;
        }

        /// <summary>
        /// Execute on collision with other object
        /// </summary>
        /// <param name="direction">Direction of movement</param>
        /// <returns>bool Can move</returns>
        public override bool CanMove(Direction direction)
        {
            Spot nextSpot;
            if (Parent.Neighbors.TryGetValue(direction, out nextSpot))
            {
                if (nextSpot.Element == null || nextSpot.Element.CanMove(direction))
                {
                    Move(direction);
                    return true;
                }
            }
            return false;
        }
    }
}
