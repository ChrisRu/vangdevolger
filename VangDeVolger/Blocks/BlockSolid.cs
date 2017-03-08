using System.Drawing;

namespace VangDeVolger.Blocks
{
    internal class BlockSolid : Block
    {
        private readonly Image _image = Properties.Resources.solid;

        /// <summary>
        /// Initialize BlockSolid Class
        /// </summary>
        /// <param name="position"></param>
        public BlockSolid(Point position) : base(position)
        {
            Pb.Image = _image;
        }

        /// <summary>
        /// Execute on collision with other object
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public override bool Touch(Game.Directions direction)
        {
            return false;
        }
    }
}
