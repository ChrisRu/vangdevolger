using System.Collections.Generic;
using System.Drawing;

namespace VangDeVolger.Blocks
{
    internal class BlockSolid : Block
    {
        private readonly Image _image = Properties.Resources.solid;

        public BlockSolid(Point position, ref List<Block> blocks) : base(position, ref blocks)
        {
            Pb.Image = _image;
        }

        internal override void Move(ref List<Block> blocks, Game.Directions direction)
        {
            throw new System.NotImplementedException();
        }
    }
}
