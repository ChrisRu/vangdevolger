namespace VangDeVolger.Elements.Birds
{
    internal class Enemy : Bird
    {
        /// <summary>
        /// Initialize EnemyBird Class
        /// </summary>
        /// <param name="x">Position X on grid</param>
        /// <param name="y">Position Y on grid</param>
        public Enemy(int x, int y) : base(x, y)
        {
            ImageLeft = Properties.Resources.bird_red_left;
            ImageRight = Properties.Resources.bird_red_right;
            Pb.Image = ImageLeft;
            GoingRight = false;
        }

        /// <summary>
        /// Move Bird with controls
        /// </summary>
        /// <param name="direction">Direction of movement</param>
        public override bool Move(Direction direction)
        {
            ChangeDirection(direction);

            return false;
        }
    }
}
