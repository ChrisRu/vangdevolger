using System.Collections.Generic;
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

        internal override void Move(ref List<Block> blocks, Size size)
        {
            throw new System.NotImplementedException();
        }
    }
}
