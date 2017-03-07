using System.Collections.Generic;
using System.Drawing;

namespace VangDeVolger.Blocks
{
    internal class BlockMoveable : Block
    {
        private Point _previousLocation;
        private readonly Image _image = VangDeVolger.Properties.Resources.rock;

        public BlockMoveable(Point position) : base(position)
        {
            this.Pb.Image = _image;
        }

        internal override void Move(ref List<Block> blocks, Size size)
        {
            _previousLocation = this.Pb.Location;
            this.Pb.Location = Point.Add(Pb.Location, size);
            foreach (var block in blocks)
            {
                if (Pb.Bounds.IntersectsWith(block.Pb.Bounds))
                {
                    this.Pb.Location = _previousLocation;
                }
            }
        }
    }
}
