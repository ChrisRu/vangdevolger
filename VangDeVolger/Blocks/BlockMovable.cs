using System.Collections.Generic;
using System.Drawing;

namespace VangDeVolger.Blocks
{
    internal class BlockMovable : Block
    {
        private readonly Image _image = Properties.Resources.rock;

        public BlockMovable(Point position) : base(position)
        {
            Pb.Image = _image;
        }

        internal override void Move(ref List<Block> blocks, Game.Directions direction)
        {
            var newLocation = Point.Add(Pb.Location, Game.EnumToSize(direction));

            if (newLocation.X < 0 || newLocation.X > Game.WindowWidth) return;
            if (newLocation.Y < 0 || newLocation.Y > Game.WindowHeight) return;

            this.Pb.Location = Point.Add(Pb.Location, Game.EnumToSize(direction));
        }
    }
}
