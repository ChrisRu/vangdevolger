
namespace VangDeVolger.Elements.Blocks
{
    internal class BlockSolid : Block
    {
        /// <summary>
        /// Initialize new BlockSolid Class
        /// </summary>
        public BlockSolid()
        {
            this.Image = Properties.Resources.solid;
            this.Pb.Image = this.Image;
        }

        /// <summary>
        /// Execute on collision with other object
        /// </summary>
        /// <param name="direction">Direction of movement</param>
        /// <returns>Element can move</returns>
        public override bool CanMove(Direction direction) => false;
    }
}
