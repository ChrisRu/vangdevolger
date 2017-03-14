using System.Drawing;

namespace VangDeVolger.Blocks
{
    internal class BlockSolid : Block
    {
        /// <summary>
        /// Initialize BlockSolid Class
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="scale"></param>
        protected BlockSolid(int x, int y, int scale) : base(x, y, scale)
        {
            Image = Properties.Resources.solid;
        }

        /// <summary>
        /// Execute on collision with other object
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public override bool Touch(Direction direction)
        {
            return false;
        }
    }
}
