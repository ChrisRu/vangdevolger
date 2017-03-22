namespace VangDeVolger.Elements.Blocks
{
    internal class BlockMovable : Block
    {
        /// <summary>
        /// Initialize BlockMovable Class
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
        public override bool Move(Direction direction)
        {
            /*
            if (newLocation.X == X && newLocation.Y == Y)
            {
                return false;
            }

            if (nextBlock == null)
            {
                Level.MoveBlock(X, Y, newLocation.X, newLocation.Y);
                return true;
            }

            if (nextBlock.Move(direction))
            {
                Level.MoveBlock(X, Y, newLocation.X, newLocation.Y);
                return true;
            }
            */

            return false;
        }
    }
}
