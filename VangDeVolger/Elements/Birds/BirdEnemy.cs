
namespace VangDeVolger.Elements.Birds
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Execute on Game End to push an event to the Game Class
    /// </summary>
    /// <param name="victory">Has won</param>
    public delegate void GameEnd(bool victory);

    /// <summary>
    /// Enemy Class to follow Player in Grid
    /// </summary>
    public class Enemy : Bird
    {
        public Timer MoveTimer { get; set; }

        public int PrevTime { get; set; }

        public event GameEnd GameEnd;

        private PathFinder _pathFinder { get; set; }

        /// <summary>
        /// Initialize new Enemy Class
        /// </summary>
        public Enemy()
        {
            this.ImageLeft = Properties.Resources.bird_red_left;
            this.ImageRight = Properties.Resources.bird_red_right;
            this.Pb.Image = this.ImageLeft;
            this.GoingRight = false;
            this._initMovement(500);
        }

        /// <summary>
        /// Move Bird with controls
        /// </summary>
        /// <param name="direction">Direction of movement</param>
        /// <returns>Element can move</returns>
        public override bool CanMove(Direction direction)
        {
            this.ChangeDirection(direction);
            return false;
        }

        /// <summary>
        /// Initialize PathFinder Movement
        /// </summary>
        /// <param name="time">Interval of enemy move</param>
        private void _initMovement(int time)
        {
            this._pathFinder = new PathFinder();
            this.MoveTimer = new Timer
            {
                Interval = time
            };
            this.MoveTimer.Tick += this._moveAlongPath;
            this.MoveTimer.Start();
        }

        /// <summary>
        /// Move Enemy Along 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _moveAlongPath(object sender, EventArgs e)
        {
            Direction? direction = this._pathFinder.GetNextDirection(this.Parent, typeof(Player), true);
            if (direction == null)
            {
                this.MoveTimer.Stop();
                this.GameEnd(true);
            }
            else
            {
                if (this.Parent.Neighbors[(Direction) direction].Element is Player)
                {
                    this.MoveTimer.Stop();
                    this.GameEnd(false);
                }
                this.ChangeDirection((Direction) direction);
                this.Move((Direction) direction);
            }
        }
    }
}
