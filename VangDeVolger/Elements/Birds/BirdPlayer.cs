


namespace VangDeVolger.Elements.Birds
{
    using Blocks;
    using System.Windows.Forms;

    public delegate void FreezeEnemy();

    /// <summary>
    /// Player Class to trap Enemy in Grid
    /// </summary>
    public class Player : Bird
    {
        public event FreezeEnemy FreezeEnemy; 

        /// <summary>
        /// Initialize new Player Class
        /// </summary>
        public Player()
        {
            this.ImageLeft = Properties.Resources.bird_blue_left;
            this.ImageRight = Properties.Resources.bird_blue_right;
            this.Pb.Image = this.ImageRight;
        }

        /// <summary>
        /// Move Player towards direction
        /// </summary>
        /// <param name="direction">Direction of movement</param>
        /// <returns>Element can move</returns>
        public override bool CanMove(Direction direction)
        {
            this.ChangeDirection(direction);

            Spot nextSpot;
            if (this.Parent.Neighbours.TryGetValue(direction, out nextSpot))
            {
                if (nextSpot.Element == null || nextSpot.Element.CanMove(direction))
                {
                    this.Move(direction);
                    return true;
                }

                if (nextSpot.Element.GetType() == typeof(BlockSnowFlake))
                {
                    nextSpot.Element.Pb.Dispose();
                    nextSpot.Element = null;
                    this.FreezeEnemy();
                    this.Move(direction);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Execute on KeyDown event
        /// </summary>
        /// <param name="e">KeyEvent Arguments</param>
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
                this.CanMove((Direction)direction);
            }
        }
    }
}
