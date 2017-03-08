using System.Collections.Generic;
using System.Drawing;

namespace VangDeVolger.Blocks
{
    internal class BlockSolid : Block
    {
        private readonly Image _image = VangDeVolger.Properties.Resources.solid;

        public BlockSolid(Point position, ref List<Block> blocks) : base(position, ref blocks)
        {
            this.Pb.Image = _image;
        }

        internal override void Move(ref List<Block> blocks, Size size)
        {
            throw new System.NotImplementedException();
        }
    }
}
