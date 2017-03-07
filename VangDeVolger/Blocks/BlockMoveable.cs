using System.Drawing;

namespace VangDeVolger.Blocks
{
    internal class BlockMoveable : Block
    {
        private readonly Image _image = VangDeVolger.Properties.Resources.rock;

        public BlockMoveable(Point position) : base(position)
        {
            this.Pb.Image = _image;
        }
    }
}
