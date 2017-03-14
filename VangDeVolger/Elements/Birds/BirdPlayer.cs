using System;
using System.Windows.Forms;

namespace VangDeVolger.Elements.Birds
{
    internal class Player : Bird
    {
        /// <summary>
        /// Initialize PlayerBird Class
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="scale"></param>
        public Player(int x, int y, int scale) : base(x, y, scale)
        {
            ImageLeft = Properties.Resources.bird_green_left;
            ImageRight = Properties.Resources.bird_green_right;
            Pb.Image = ImageRight;
        }

        public override bool Move(Direction direction)
        {
            var newLocation = Level.DirectionToLocation(X, Y, direction);
            var nextBlock = Level.Grid[newLocation.Item1, newLocation.Item2];

            if (newLocation.Item1 == X && newLocation.Item2 == Y)
            {
                return false;
            }

            if (nextBlock == null)
            {
                Level.BirdLocation = new Tuple<int, int>(newLocation.Item1, newLocation.Item2);
                Level.MoveBlock(X, Y, newLocation.Item1, newLocation.Item2);
                return true;
            }

            if (nextBlock.Move(direction))
            {
                Level.BirdLocation = new Tuple<int, int>(newLocation.Item1, newLocation.Item2);
                Level.MoveBlock(X, Y, newLocation.Item1, newLocation.Item2);
                return true;
            }

            return false;
        }
    }
}
