﻿
namespace VangDeVolger.Elements.Blocks
{
    /// <summary>
    /// Solid Block to sit inside Spot, blocks the Player's movement
    /// </summary>
    public class BlockGrass : Block
    {
        /// <summary>
        /// Initialize new BlockSolid Class
        /// </summary>
        public BlockGrass()
        {
            this.Image = Properties.Resources.grass;
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
