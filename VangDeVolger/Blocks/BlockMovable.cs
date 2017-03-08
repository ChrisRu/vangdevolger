using System.Collections.Generic;
using System.Drawing;

namespace VangDeVolger.Blocks
{
    internal class BlockMovable : Block
    {
        private Point _previousLocation;
        private readonly Image _image = VangDeVolger.Properties.Resources.rock;

        public BlockMovable(Point position, ref List<Block> blocks) : base(position, ref blocks)
        {
            this.Pb.Image = _image;
        }

        internal override void Move(ref List<Block> blocks, Size size)
        {
            _previousLocation = this.Pb.Location;

            var newLocation = Point.Add(Pb.Location, size);

            if (newLocation.X < 0 || newLocation.X > Game.WindowWidth) return;
            if (newLocation.Y < 0 || newLocation.Y > Game.WindowHeight) return;

            this.Pb.Location = Point.Add(Pb.Location, size);
        }
    }
}
