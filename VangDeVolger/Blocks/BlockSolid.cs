using System.Collections.Generic;
using System.Drawing;

namespace VangDeVolger.Blocks
{
    internal class BlockSolid : Block
    {
        private readonly Image _image = Properties.Resources.solid;

        public BlockSolid(Point position) : base(position)
        {
            Pb.Image = _image;
        }

        public override bool Touch(Game.Directions direction)
        {
            return false;
        }
    }
}
