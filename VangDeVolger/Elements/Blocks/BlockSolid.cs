namespace VangDeVolger.Elements.Blocks
{
    internal class BlockSolid : Block
    {
        /// <summary>
        /// Initialize BlockSolid Class
        /// </summary>
        public BlockSolid(Spot parent) : base(parent)
        {
            Image = Properties.Resources.solid;
            Pb.Image = Image;
        }

        /// <summary>
        /// Execute on collision with other object
        /// </summary>
        /// <param name="direction">Direction of movement</param>
        /// <returns>bool Can Move</returns>
        public override bool Move(Direction direction)
        {
            return false;
        }
    }
}
