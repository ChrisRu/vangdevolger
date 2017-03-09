using System.Drawing;
using System.Linq;

namespace VangDeVolger.Blocks
{
    internal class BlockMovable : Block
    {
        private readonly Image _image = Properties.Resources.tree;

        /// <summary>
        /// Initialize BlockMovable Class
        /// </summary>
        /// <param name="position"></param>
        public BlockMovable(Point position) : base(position)
        {
            Pb.Image = _image;
        }

        /// <summary>
        /// Execute on collision with other object
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public override bool Touch(Game.Directions direction)
        {
            var newLocation = Point.Add(Pb.Location, Game.EnumToSize(direction));

            if (newLocation.X < 0 || newLocation.X > (Game.WindowWidth - Pb.Width)) return false;
            if (newLocation.Y < 0 || newLocation.Y > Game.WindowHeight - (Pb.Width * 2)) return false;

            var canMove = true;

            foreach (var block in Game.Blocks.ToList())
            {
                if (newLocation != block.Pb.Location) continue;

                canMove = block.Touch(direction);
            }

            foreach (var enemy in Game.Enemies)
            {
                var tempRect = new Rectangle
                {
                    Location = newLocation,
                    Size = new Size(Game.BlockSize, Game.BlockSize)
                };

                if (tempRect.IntersectsWith(enemy.Pb.Bounds)) canMove = false;
            }

            if (!canMove) return false;

            Pb.Location = Point.Add(Pb.Location, Game.EnumToSize(direction));
            return true;
        }
    }
}
