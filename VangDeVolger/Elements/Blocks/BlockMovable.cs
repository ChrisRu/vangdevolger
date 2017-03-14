namespace VangDeVolger.Elements.Blocks
{
    internal class BlockMovable : Block
    {
        /// <summary>
        /// Initialize BlockMovable Class
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="scale"></param>
        public BlockMovable(int x, int y) : base(x, y)
        {
            Image = Properties.Resources.tree;
            Pb.Image = Image;
        }

        /// <summary>
        /// Execute on collision with other object
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public override bool Move(Direction direction)
        {
            var newLocation = Level.DirectionToLocation(X, Y, direction);
            var nextBlock = Level.Grid[newLocation.Item1, newLocation.Item2];

            if (newLocation.Item1 == X && newLocation.Item2 == Y)
            {
                return false;
            }

            if (nextBlock == null)
            {
                Level.MoveBlock(X, Y, newLocation.Item1, newLocation.Item2);
                return true;
            }

            if (nextBlock.Move(direction))
            {
                Level.MoveBlock(X, Y, newLocation.Item1, newLocation.Item2);
                return true;
            }
            return false;
        }
    }
}
