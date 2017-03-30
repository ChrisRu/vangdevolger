using System.Windows.Forms;

namespace VangDeVolger.Elements.Birds
{
    public class Player : Bird
    {
        /// <summary>
        /// Initialize PlayerBird Class
        /// </summary>
        public Player()
        {
            ImageLeft = Properties.Resources.bird_blue_left;
            ImageRight = Properties.Resources.bird_blue_right;
            Pb.Image = ImageRight;
        }

        /// <summary>
        /// Move Player towards direction
        /// </summary>
        /// <param name="direction">Direction of movement</param>
        /// <returns>bool Can move</returns>
        public override bool CanMove(Direction direction)
        {
            ChangeDirection(direction);

            Spot nextSpot;
            if (Parent.Neighbors.TryGetValue(direction, out nextSpot))
            {
                if (nextSpot.Element == null || nextSpot.Element.CanMove(direction))
                {
                    Move(direction);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Execute on KeyDown event
        /// </summary>
        /// <param name="e"></param>
        public override void KeyDown(KeyEventArgs e)
        {
            Direction? direction = null;
            switch (e.KeyCode)
            {
                case Keys.Up:
                    direction = Direction.Up;
                    break;
                case Keys.Down:
                    direction = Direction.Down;
                    break;
                case Keys.Left:
                    direction = Direction.Left;
                    break;
                case Keys.Right:
                    direction = Direction.Right;
                    break;
            }
            if (direction != null)
            {
                CanMove((Direction)direction);
            }
        }
    }
}
