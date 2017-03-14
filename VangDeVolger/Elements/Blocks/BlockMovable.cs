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
        public BlockMovable(int x, int y, int scale) : base(x, y, scale)
        {
            Image = Properties.Resources.tree;
            Pb.Image = Image;
        }

        /// <summary>
        /// Execute on collision with other object
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public override bool Touch(Direction direction)
        {
            return true;
        }
    }
}
