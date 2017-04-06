
namespace VangDeVolger.Elements.Blocks
{
    /// <summary>
    /// SnowFlake Block to sit inside Spot, temporarily freezes the enemy
    /// </summary>
    public class BlockSnowFlake : Block
    {
        /// <summary>
        /// Initialize new BlockSnowFlake Class
        /// </summary>
        public BlockSnowFlake()
        {
            this.Image = Properties.Resources.snowflake;
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
