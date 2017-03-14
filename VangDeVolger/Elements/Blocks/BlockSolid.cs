namespace VangDeVolger.Elements.Blocks
{
    internal class BlockSolid : Block
    {
        /// <summary>
        /// Initialize BlockSolid Class
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="scale"></param>
        public BlockSolid(int x, int y) : base(x, y)
        {
            Image = Properties.Resources.solid;
            Pb.Image = Image;
        }

        /// <summary>
        /// Execute on collision with other object
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public override bool Move(Direction direction)
        {
            return false;
        }
    }
}
