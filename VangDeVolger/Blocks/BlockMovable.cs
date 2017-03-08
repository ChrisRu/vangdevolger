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

        public override bool Touch(Game.Directions direction)
        {
            var newLocation = Point.Add(Pb.Location, Game.EnumToSize(direction));

            if (newLocation.X < 0 || newLocation.X > Game.WindowWidth) return false;
            if (newLocation.Y < 0 || newLocation.Y > Game.WindowHeight) return false;

            var canMove = true;

            foreach (var block in Game.Blocks)
            {
                var nextBlockLocation = Point.Add(Pb.Location, Game.EnumToSize(direction));

                if (nextBlockLocation != block.Pb.Location) continue;

                canMove = block.Touch(direction);
                
            }

            if (!canMove) return false;

            Pb.Location = Point.Add(Pb.Location, Game.EnumToSize(direction));
            return true;
        }
    }
}
