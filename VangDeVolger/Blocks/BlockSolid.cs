using System.Drawing;

namespace VangDeVolger.Blocks
{
    internal class BlockSolid : Block
    {
        private readonly Image _image = VangDeVolger.Properties.Resources.solid;

        public BlockSolid(Point position) : base(position)
        {
            this.Pb.Image = _image;
        }
    }
}
