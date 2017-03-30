
namespace VangDeVolger.Elements.Blocks
{
    public class BlockMovable : Block
    {
        /// <summary>
        /// Initialize new BlockMovable class
        /// </summary>
        public BlockMovable()
        {
            this.Image = Properties.Resources.movable;
            this.Pb.Image = this.Image;
        }

        /// <summary>
        /// Execute on collision with other object
        /// </summary>
        /// <param name="direction">Direction of movement</param>
        /// <returns>Element can move</returns>
        public override bool CanMove(Direction direction)
        {
            Spot nextSpot;
            if (this.Parent.Neighbors.TryGetValue(direction, out nextSpot))
            {
                if (nextSpot.Element == null || nextSpot.Element.CanMove(direction))
                {
                    this.Move(direction);
                    return true;
                }
            }
            return false;
        }
    }
}
