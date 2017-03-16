using System;

namespace VangDeVolger.Elements.Birds
{
    internal class Player : Bird
    {
        /// <summary>
        /// Initialize PlayerBird Class
        /// </summary>
        /// <param name="x">Position X on grid</param>
        /// <param name="y">Position Y on grid</param>
        public Player(int x, int y) : base(x, y)
        {
            ImageLeft = Properties.Resources.bird_green_left;
            ImageRight = Properties.Resources.bird_green_right;
            Pb.Image = ImageRight;
        }

        /// <summary>
        /// Move Player towards direction
        /// </summary>
        /// <param name="direction">Direction of movement</param>
        /// <returns>bool Can move</returns>
        public override bool Move(Direction direction)
        {
            ChangeDirection(direction);

            Coordinates newLocation = DirectionToLocation(X, Y, direction);
            Element nextBlock = Level.Grid[newLocation.X, newLocation.Y];

            if (newLocation.X == X && newLocation.Y == Y)
            {
                return false;
            }

            if (nextBlock == null)
            {
                Level.MoveBlock(X, Y, newLocation.X, newLocation.Y);
                return true;
            }

            if (nextBlock.Move(direction))
            {
                Level.MoveBlock(X, Y, newLocation.X, newLocation.Y);
                return true;
            }

            return false;
        }
    }
}
